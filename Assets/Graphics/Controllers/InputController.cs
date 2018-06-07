using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float movedX = pos.x - TileController.BOARD_TILE_OFFSET.x + (TileController.TILE_SIZE.x / 2f);
            float movedY = pos.y - TileController.BOARD_TILE_OFFSET.y + (TileController.TILE_SIZE.y / 2f);

            int x = Mathf.FloorToInt(movedX / TileController.TILE_SIZE.x);
            int y = Mathf.FloorToInt(movedY / TileController.TILE_SIZE.y);
            Driver.Instance.RegisterClickOnTile(x, y);
        }
    }
}
