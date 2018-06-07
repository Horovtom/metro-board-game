using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour {
	public static Driver Instance { get; protected set; }

	public int BoardSize;
	private InputController inputController;
	private GraphicsController graphicsController;
	private GameController gameController;

	public Driver() {
		Instance = this;
	}

	void Start() {
		inputController = Utilities.GetChildScript<InputController>(this.gameObject, "InputControllerGO");
		graphicsController = Utilities.GetChildScript<GraphicsController>(this.gameObject, "GraphicsControllerGO");
		gameController = Utilities.GetChildScript<GameController>(this.gameObject, "GameControllerGO");

		graphicsController.State = GameState.GAME;
		CreateGame(2);
	}
	
	// Update is called once per frame
	void Update() {
			
	}

	public void CreateGame(int numPlayers) {
		gameController.CreateGame(numPlayers, TilesChanged, ScoreChanged, StationsChanged);
	}

    public void RegisterClickOnTile(int x, int y) {
        gameController.ClickedOnTile(x, y);
    }

	public void TilesChanged(Tile[,] tiles) {
		Debug.Log("Displaying changed tiles.");
		for (int i = 0; i < BoardSize; i++) {
			for (int k = 0; k < BoardSize; k++) {
				if (tiles[i, k] != null)
					graphicsController.DisplayTile(tiles[i, k], i, k);
			}
		}
	}

	public void ScoreChanged(Dictionary<PlayerColor, int> obj, PlayerColor onTurn) {
		Debug.Log("Event changed score.");
        graphicsController.ScoreChanged(obj, onTurn);
	}

	void StationsChanged(PlayerColor[] obj) {
		Debug.Log("Event changed stations.");
		//throw new System.NotImplementedException();
	}
}
