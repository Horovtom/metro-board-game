using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game {
	private Action<Tile[,]> cbTileChanged;
	private Action<int> cbActivePlayerChanged;
	private Action<PlayerColor[]> cbStationsChanged;
	private Action<int[]> cbPointsChanged;
	private int numPlayers;
	private Tile[,] grid;
	private Tile[] playersHands;
	private TileRepository tileRepository;
	int playerOnTurn = 0;
	PlayerColor[] stations;
	int[] points;

	public Game(int numPlayers, TileRepository tileRepository) {
		
		this.numPlayers = numPlayers;
		playersHands = new Tile[numPlayers];
		stations = LoadStations();
		points = new int[numPlayers];

		this.tileRepository = tileRepository;
		InitialHandDraw();

		grid = new Tile[Driver.Instance.BoardSize, Driver.Instance.BoardSize];

	}

	public void RegisterCallbacks(Action<Tile[,]> tileChangedCb, Action<int> activePlayerChangedCb, Action<PlayerColor[]> stationsChangedCb, Action<int[]> pointsChangedCb) {
		cbTileChanged += tileChangedCb;
		cbActivePlayerChanged += activePlayerChangedCb;
		cbStationsChanged += stationsChangedCb;
		cbPointsChanged += pointsChangedCb;
		//Call them right away
		cbTileChanged(grid);
		cbActivePlayerChanged(playerOnTurn);
		cbStationsChanged(stations);
		cbPointsChanged(points);
	}

	private void NextPlayerOnTurn() {
		playerOnTurn = (playerOnTurn + 1) % numPlayers;
		Debug.Log("Player on turn: " + playerOnTurn);
		cbActivePlayerChanged(playerOnTurn);
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

	public bool PlaceTileIfPossible(int x, int y) {
		if (grid[x, y] != null) {
			Debug.LogError("Cannot place tile on position: " + x + "," + y);
			return false;
		} else {
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
				if (cbPointsChanged != null)
					cbPointsChanged(points);
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
