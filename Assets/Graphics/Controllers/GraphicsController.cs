using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GraphicsController : MonoBehaviour {
	private SpriteController spriteController;


	private GameState state;

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
	void Start() {
		spriteController = Utilities.GetChildScript<SpriteController>(this.gameObject, "SpriteControllerGO");


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
}
