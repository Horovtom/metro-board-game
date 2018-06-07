using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game {
    private Action<Tile[,]> cbTileChanged;
    private Action<Dictionary<PlayerColor, int>, PlayerColor> cbScoreChanged;
    private Action<PlayerColor[]> cbStationsChanged;
    private int numPlayers;
    private Tile[,] grid;
    private Tile[] playersHands;
    private TileRepository tileRepository;
    int playerOnTurn = 0;
    PlayerColor[] stations;
    int[] points;
    private int boardSize;

    public Game(int numPlayers, TileRepository tileRepository) {

        this.numPlayers = numPlayers;
        playersHands = new Tile[numPlayers];
        stations = LoadStations();
        points = new int[numPlayers];

        this.tileRepository = tileRepository;
        InitialHandDraw();

        grid = new Tile[Driver.Instance.BoardSize, Driver.Instance.BoardSize];
        boardSize = Driver.Instance.BoardSize;
    }

    private Dictionary<PlayerColor, int> GetPoints() {
        Dictionary<PlayerColor, int> returning = new Dictionary<PlayerColor, int>();
        for (int i = 0; i < numPlayers; i++) {
            returning[(PlayerColor)i] = points[i];
        }
        return returning;
    }

    public void RegisterCallbacks(Action<Tile[,]> tileChangedCb, Action<Dictionary<PlayerColor, int>, PlayerColor> scoreChangedCb, Action<PlayerColor[]> stationsChangedCb) {
        cbTileChanged += tileChangedCb;
        cbScoreChanged += scoreChangedCb;
        cbStationsChanged += stationsChangedCb;
        //Call them right away

        if (cbTileChanged != null) cbTileChanged(grid);
        if (cbScoreChanged != null) cbScoreChanged(GetPoints(), (PlayerColor)playerOnTurn);
        if (cbStationsChanged != null) cbStationsChanged(stations);
    }

    private void NextPlayerOnTurn() {
        playerOnTurn = (playerOnTurn + 1) % numPlayers;
        Debug.Log("Player on turn: " + playerOnTurn);
        if (cbScoreChanged != null)  cbScoreChanged(GetPoints(), (PlayerColor)playerOnTurn);
    }

    private void InitialHandDraw() {
        tileRepository.CreateStack();

        for (int i = 0; i < numPlayers; i++) {
            DrawForPlayer(i);
        }
    }

    private void DrawForPlayer(int i) {
        if (tileRepository.LeftInStack() != 0) {
            playersHands[i] = tileRepository.PopStack();
            Debug.Log("Player " + i + " has drawn tile: " + playersHands[i]);
        } else {
            Debug.LogError("Cannot draw into hand of the last player, because the stack is empty!");
            playersHands[i] = null;
        }
    }

    public bool GameRunning() {
        return playerOnTurn != -1;
    }

    private void GameEnd() {
        playerOnTurn = -1;
        throw new NotImplementedException();
    }

    private bool IsValidPlace(int x, int y) {
        if (x < 0 || y < 0 || x >= boardSize || y >= boardSize) return false;
        if ((x == 3 && (y == 3 || y == 4)) || (x == 4) && (y == 3 || y == 4)) return false;
        return grid[x, y] == null;
    }

    private bool WouldBeValidMap(Tile t, int x, int y) {
        //TODO: IMPLEMENT
        //Bottom line:
        if (y == 0) {
            if (t.IsConnected(Direction.S, Direction.S)) return false;
        }

        //Right line:
        if (x == boardSize - 1) {
            if (t.IsConnected(Direction.E, Direction.E)) return false;
        }

        //Left line:
        if (x == 0) {
            if (t.IsConnected(Direction.W, Direction.W)) return false;
        }

        //Upper line:
        if (y == boardSize - 1) {
            if (t.IsConnected(Direction.N, Direction.N)) return false;
        }

        //Corners
        if (x == 0) {
            if (y == 0) {
                //Lower left corner
                if (t.IsConnected(Direction.W, Direction.S) || t.IsConnected(Direction.S, Direction.W)) return false;
            } else if (y == boardSize - 1) {
                //Upper left corner
                if (t.IsConnected(Direction.N, Direction.W) || t.IsConnected(Direction.W, Direction.N)) return false;
            }
        } else if (x == boardSize - 1) {
            if (y == 0) {
                //Lower right corner
                if (t.IsConnected(Direction.S, Direction.E) || t.IsConnected(Direction.E, Direction.S)) return false;
            } else if (y == boardSize - 1) {
                //Upper right corner
                if (t.IsConnected(Direction.N, Direction.E) || t.IsConnected(Direction.E, Direction.N)) return false;
            }
        }

        return true;
    }

    public bool PlaceTileIfPossible(int x, int y) {
        if (!IsValidPlace(x, y)) {
            Debug.LogError("Cannot place tile on position: " + x + "," + y);
            return false;
        } else {
            if (!WouldBeValidMap(playersHands[playerOnTurn], x, y)) {
                Debug.LogError("Map would not be valid!");
                return false;
            }

            Debug.Log("Placing tile " + playersHands[playerOnTurn] + " at: " + x + "," + y);
            grid[x, y] = playersHands[playerOnTurn];
            if (grid[x, y] == null) {
                //The game has ended!
                GameEnd();
                return false;
            }
            DrawForPlayer(playerOnTurn);
            NextPlayerOnTurn();
            if (cbTileChanged != null)
                cbTileChanged(grid);
            if (ClosedAnyStation(x, y)) {
                if (cbStationsChanged != null)
                    cbStationsChanged(stations);
                if (cbScoreChanged != null)
                    cbScoreChanged(GetPoints(), (PlayerColor) playerOnTurn);
            }
            return true;
        }
    }

    PlayerColor[] LoadStations() {
        PlayerColor[] returning = new PlayerColor[32];
        for (int i = 0; i < returning.Length; i++) {
            returning[i] = PlayerColor.None;
        }

        string config = PlayerPrefs.GetString("ScheduleConfig");

        char[] delimiter = { ' ' };
        foreach (string line in config.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)) {
            string[] words = line.Split(delimiter);
            if (words.Length < 2) {
                Debug.LogError("Config file has wrong number of tokens!");
                throw new FormatException();
            }

            int numOfPlayers = int.Parse(words[0]);
            if (numOfPlayers != numPlayers)
                continue;

            PlayerColor color = (PlayerColor)Enum.Parse(typeof(PlayerColor), words[1]);

            for (int i = 2; i < words.Length; i++) {
                int cell = int.Parse(words[i]);
                returning[cell - 1] = color;
            }
        }

        return returning;
    }

    bool ClosedAnyStation(int x, int y) {
        return false;
        //throw new NotImplementedException();
    }
}
