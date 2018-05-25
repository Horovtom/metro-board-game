using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController {
    private static TileController instance;
    public GameObject myGameObject;

    private Vector3 BOARD_TILE_OFFSET = new Vector3(-3.546f, -3.527f, 0);
    private Vector3 TILE_SIZE = new Vector3(1.014f, 1.008f, 0);
    public GameObject[,] tiles = new GameObject[8, 8];
    public SpriteRenderer[,] tileSprites = new SpriteRenderer[8, 8];

    /// <summary>
    /// This function will display tile with a given type at a given cell coordinate
    /// </summary>
    /// <param name="xCell">Number in range 0-8</param>
    /// <param name="yCell">Number in range 0-8</param>
    /// <param name="type">String representing the type of the cell</param>
    public void displayTile(int xCell, int yCell, string type) {
        Sprite s = TileRepository.getSprite(type);
        if (s == null) {
            Debug.LogError("Failed to load sprite: " + type);
            return;
        }

        if (xCell < 0 || yCell < 0 || xCell >= 8 || yCell >= 8) {
            Debug.LogError("Cannot place cell at: " + xCell + "," + yCell + " because it is out of effective range!");
            return;
        }


        if (tiles[xCell, yCell] == null) {
            GameObject go = new GameObject();
            //TODO: COMPLETE
            go.name = "Tile - " + xCell + "," + yCell;
            go.transform.SetParent(myGameObject.transform, true);
           
            Vector3 pos = new Vector3(BOARD_TILE_OFFSET.x, BOARD_TILE_OFFSET.y, BOARD_TILE_OFFSET.z);
            pos += myGameObject.transform.position;
            pos.x += TILE_SIZE.x * xCell;
            pos.y += TILE_SIZE.y * yCell;

            go.transform.localPosition = pos;
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sortingLayerName = "Tiles";

            tileSprites[xCell, yCell] = renderer;
            tiles[xCell, yCell] = go;
        }

        Debug.Log("Setting cell: " + xCell + "," + yCell + " to sprite: " + type);
        tileSprites[xCell, yCell].sprite = s;
    }

    /// <summary>
    /// This constructor sets the static instance and sets the gameObject this is tied to.
    /// It will instantiate all the tiles as children of specified gameObject and relevant to its transform.
    /// </summary>
    /// <param name="myGameObject"></param>
    public TileController(GameObject myGameObject) {
        instance = this;
        this.myGameObject = myGameObject;
    }

    public static TileController getInstance() { return instance; } 
}
