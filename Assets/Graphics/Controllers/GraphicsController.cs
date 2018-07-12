using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GraphicsController : MonoBehaviour {
	private SpriteController spriteController;
    private ScoreDisplayController scoreDisplayController;
    private TileInHandController tileInHandController;

    private GameState state = GameState.GAME;

	public GameState State {
		get { return state; }
		set {
			if (value == GameState.GAME) {
				switchToGameState();
				state = GameState.GAME;
			} else if (value == GameState.MENU) {
				switchToMenuState();
				state = GameState.MENU;
			} else {
				throw new ArgumentException("Unhandled State!");
			}
		}
	}



	// Use this for initialization
	void Awake() {
		spriteController = Utilities.GetChildScript<SpriteController>(this.gameObject, "SpriteControllerGO");
        scoreDisplayController = Utilities.GetChildScript<ScoreDisplayController>(this.transform.Find("Canvas").gameObject, "ScoreDisplayControllerGO");
        tileInHandController = Utilities.GetChildScript<TileInHandController>(this.transform.Find("Canvas").gameObject, "TileInHandControllerGO");

        if (spriteController == null || scoreDisplayController == null || tileInHandController == null) {
            Debug.LogError("Controller not found!");
        }
    }
	
	// Update is called once per frame
	void Update() {
		
	}

	/// <summary>
	/// This will switch to game view
	/// </summary>
	private void switchToGameState() {
		spriteController.displayBoard();

	}

	/// <summary>
	/// This will switch to menu view
	/// </summary>
	private void switchToMenuState() {
		throw new NotImplementedException();
	}

	public void DisplayTile(Tile t, int x, int y) {
		spriteController.DisplayTile(t, x, y);
	}

	public void CleanBoard() {
		spriteController.CleanBoard();
	}

    public void ScoreChanged(Dictionary<PlayerColor, int> obj, PlayerColor onTurn) {
        scoreDisplayController.UpdateScores(obj, onTurn);
    }

    public void TileInHandChanged(string type) {
        tileInHandController.ChangeTileInHandImage(spriteController.GetTileSprite(type));

    }
}
