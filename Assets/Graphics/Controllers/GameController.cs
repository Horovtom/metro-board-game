using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {
	public static GameController Instance { get; protected set; }

	private TileRepository tileRepository;
	private Game game;

	public GameController() {
		GameController.Instance = this;
	}

	// Use this for initialization
	void Awake() {
		tileRepository = new TileRepository();
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	public void CreateGame(int numPlayers, Action<Tile[,]> tileChangedCb, Action<Dictionary<PlayerColor, int>, PlayerColor> scoreChangedCb, Action<PlayerColor[]> stationsChangedCb) {
		Debug.Log("Creating new game for " + numPlayers + " players.");
		game = new Game(numPlayers, tileRepository);
		game.RegisterCallbacks(tileChangedCb, scoreChangedCb, stationsChangedCb);
	}

	public void ClickedOnTile(int x, int y) {
		game.PlaceTileIfPossible(x, y);		
	}
}
