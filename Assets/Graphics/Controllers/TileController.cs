using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController {
	private static TileController instance;
	public GameObject myGameObject;
	private TileSpritesRepository tileSpritesRepository;

	public static Vector3 BOARD_TILE_OFFSET = new Vector3(-3.546f, -3.527f, 0);
	public static Vector3 TILE_SIZE = new Vector3(1.014f, 1.008f, 0);
	public GameObject[,] tilesGO = new GameObject[Driver.Instance.BoardSize, Driver.Instance.BoardSize];
	public SpriteRenderer[,] tilesSprites = new SpriteRenderer[Driver.Instance.BoardSize, Driver.Instance.BoardSize];

	/// <summary>
	/// This function will display tile with a given type at a given cell coordinate
	/// </summary>
	/// <param name="xCell">Number in range 0-8</param>
	/// <param name="yCell">Number in range 0-8</param>
	/// <param name="type">String representing the type of the cell</param>
	/// <param name="rotation">Integer representing number of 90 degree rotations to the right</param> 
	public void DisplayTile(int xCell, int yCell, string type, int rotation) {
		Sprite s = tileSpritesRepository.GetSprite(type);
		if (s == null) {
			Debug.LogError("Failed to load sprite: " + type);
			return;
		}

		if (xCell < 0 || yCell < 0 || xCell >= Driver.Instance.BoardSize || yCell >= Driver.Instance.BoardSize) {
			Debug.LogError("Cannot place cell at: " + xCell + "," + yCell + " because it is out of effective range!");
			return;
		}


		if (tilesGO[xCell, yCell] == null) {
			GameObject go = new GameObject();
			go.name = "Tile - " + xCell + "," + yCell;
			go.transform.SetParent(myGameObject.transform, true);
           
			Vector3 pos = new Vector3(BOARD_TILE_OFFSET.x, BOARD_TILE_OFFSET.y, BOARD_TILE_OFFSET.z);
			pos += myGameObject.transform.position;
			pos.x += TILE_SIZE.x * xCell;
			pos.y += TILE_SIZE.y * yCell;

			go.transform.localPosition = pos;
			SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
			renderer.sortingLayerName = "Tiles";

			tilesSprites[xCell, yCell] = renderer;
			tilesGO[xCell, yCell] = go;
		}

		//Debug.Log("Setting cell: " + xCell + "," + yCell + " to sprite: " + type);
		tilesSprites[xCell, yCell].sprite = s;
		tilesSprites[xCell, yCell].transform.rotation = Quaternion.identity;
		tilesSprites[xCell, yCell].transform.Rotate(Vector3.forward * (90 * rotation));
		tilesGO[xCell, yCell].SetActive(true);
	}

	/// <summary>
	/// This function will display tile on specified cell coordinate
	/// </summary>
	public void DisplayTile(Tile t, int x, int y) {
		DisplayTile(x, y, "tiles" + t.Type, t.Rotation);
	}

	/// <summary>
	/// This constructor sets the static instance and sets the gameObject this is tied to.
	/// It will instantiate all the tiles as children of specified gameObject and relevant to its transform.
	/// </summary>
	/// <param name="myGameObject"></param>
	public TileController(GameObject myGameObject) {
		instance = this;
		this.myGameObject = myGameObject;
		tileSpritesRepository = new TileSpritesRepository();
	}

	public static TileController getInstance() {
		return instance;
	}

	public void ResetBoard() {
		Debug.Log("Reseting board");
		for (int i = 0; i < Driver.Instance.BoardSize; i++) {
			for (int j = 0; j < Driver.Instance.BoardSize; j++) {
				if (tilesGO[i, j] != null)
					tilesGO[i, j].SetActive(false);
			}
		}
	}
}
