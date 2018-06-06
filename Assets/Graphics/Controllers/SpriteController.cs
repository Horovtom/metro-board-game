using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {
	private TileController tileController;
	private GameObject tileControllerGO;

    
	public Sprite boardBackground;

	private void InstantiateTileController() {
		tileControllerGO = new GameObject();
		tileControllerGO.name = "TileControllerGO";
		tileControllerGO.transform.SetParent(this.transform, true);
		tileController = new TileController(tileControllerGO);
		tileControllerGO.SetActive(false);
	}

	void Awake() {
		InstantiateTileController();
	}

	// Use this for initialization
	void Start() {

	}

	/// <summary>
	/// This function will instantiate the board sprite.
	/// </summary>
	public void displayBoard() {
		CreateBoardSprite();
		tileControllerGO.SetActive(true);
	}

	void CreateBoardSprite() {
		GameObject go = new GameObject();
		go.name = "Board Sprite";
		go.transform.SetParent(this.transform, true);
		SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
		renderer.sortingLayerName = "Board";
		renderer.sprite = Resources.Load("Sprites/board", typeof(Sprite)) as Sprite;
	}

	public void DisplayTile(Tile t, int x, int y) {
		tileController.DisplayTile(t, x, y);
	}

	public void CleanBoard() {
		tileController.ResetBoard();
	}

	// Update is called once per frame
	void Update() {
		
	}
}
