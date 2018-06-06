using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {
	private TileController tileController;
    
	public Sprite boardBackground;

    

	// Use this for initialization
	void Start() {
		displayBoard();
		TileRepository.CreateRepository();
		GameObject tileControllerGo = null;
		int count = this.gameObject.transform.childCount;
		bool found = false;
		for (int i = 0; i < count; ++i) {
			tileControllerGo = this.gameObject.transform.GetChild(i).gameObject;
			if (tileControllerGo.name.Equals("TileController")) {
				found = true;
				break;
			}
		}

		if (!found) {
			throw new System.Exception("Did not find a child of SpriteController named TileController!");
		}

		tileController = new TileController(tileControllerGo);
	}

	/// <summary>
	/// This function will instantiate the board sprite.
	/// </summary>
	public void displayBoard() {
		GameObject go = new GameObject();
		go.name = "Board Sprite";
		go.transform.SetParent(this.transform, true);
		SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
		renderer.sortingLayerName = "Board";
		renderer.sprite = Resources.Load("Sprites/board", typeof(Sprite)) as Sprite;
	}

    


	bool a = false;
	// Update is called once per frame
	void Update() {
		if (!a) {
			tileController.displayTile(new Tile("tiles0"), 0, 0);
			//tileController.displayTile(0, 0, "tiles0");
			tileController.displayTile(0, 1, "tiles2", 0);
			tileController.displayTile(1, 0, "tiles0", 0);

			tileController.displayTile(7, 7, "tiles11", 0);
			Tile t = new Tile("tiles11");
			t.Rotation = 1;
				

			tileController.displayTile(t, 7, 6);
			tileController.displayTile(7, 0, "tiles22", 0);
			tileController.displayTile(0, 7, "tiles20", 0);
			a = true;
		}
	}
}
