using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpritesRepository {
	private Dictionary<string, Sprite> tileSprites;
	private List<string> tileNames;

	public TileSpritesRepository() {
		CreateRepository();
	}

	public void CreateRepository() {
		LoadSprites();
	}

	private void LoadSprites() {
		tileSprites = new Dictionary<string, Sprite>();
		tileNames = new List<string>();
		Sprite[] sprites1 = Resources.LoadAll<Sprite>("Sprites/tiles1");
		Sprite[] sprites2 = Resources.LoadAll<Sprite>("Sprites/tiles2");
		Sprite[] sprites3 = Resources.LoadAll<Sprite>("Sprites/tiles3");

		foreach (Sprite s in sprites1) {
			tileSprites[s.name] = s;
			tileNames.Add(s.name);
		}
		foreach (Sprite s in sprites2) {
			tileSprites[s.name] = s;
			tileNames.Add(s.name);
		}
		foreach (Sprite s in sprites3) {
			tileSprites[s.name] = s;
			tileNames.Add(s.name);
		}
	}

	public Sprite GetSprite(string type) {
		if (tileNames == null) {
			Debug.LogError("TileSpritesRepository was not instantiated! Fixing...");
			CreateRepository();
		}
		if (!tileNames.Contains(type))
			throw new KeyNotFoundException("There is no tile sprite with name: " + type);
		return tileSprites[type];
	}

	public Sprite GetSprite(int type) {
		return GetSprite("tiles" + type);
	}
}
