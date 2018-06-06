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
		gameController.ClickedOnTile(2, 1);
		gameController.ClickedOnTile(3, 1);
		gameController.ClickedOnTile(4, 1);

	}
	
	// Update is called once per frame
	void Update() {
			
	}

	public void CreateGame(int numPlayers) {
		gameController.CreateGame(numPlayers, TilesChanged, ActivePlayerChanged, StationsChanged, PointsChanged);
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

	public void ActivePlayerChanged(int obj) {
		Debug.Log("Displaying changed player.");
		//throw new System.NotImplementedException();
	}

	void StationsChanged(PlayerColor[] obj) {
		Debug.Log("Displaying changed stations.");
		//throw new System.NotImplementedException();
	}

	void PointsChanged(int[] obj) {
		Debug.Log("Displaying changed points.");
		//throw new System.NotImplementedException();
	}
}
