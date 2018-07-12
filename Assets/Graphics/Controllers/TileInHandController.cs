using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInHandController : MonoBehaviour {
    private Image i;

	// Use this for initialization
	void Start () {
        i = (Image)this.GetComponent<Image>();
        if (i == null) Debug.LogError("Image object not found!");
	}
	
    public void ChangeTileInHandImage(Sprite s) {
        i.sprite = s;
    }
}
