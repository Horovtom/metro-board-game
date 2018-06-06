using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileRepository {


	private static Dictionary<string, Sprite> tileSprites;
	private static Dictionary<string, int> tilesCounts;
	private static Dictionary<string, Dictionary<Direction, Direction>> tileConnections;

	public static void CreateRepository() {
		LoadSprites();
		LoadTileConfiguration();
	}

	/// <summary>
	/// This function will initialize tile stack with tile counts according to TileRepository.
	/// </summary>
	public void InitializeTileStack() {
		TileStack.CreateStack(tilesCounts);
	}

	private static void LoadTileConfiguration() {
		tileConnections = new Dictionary<string, Dictionary<Direction, Direction>>();
		tilesCounts = new Dictionary<string, int>();

		DefaultConfigCreator.PresentConfig();
		string configFile = PlayerPrefs.GetString("TilesConfig");
		int curr = 0;
		char[] delimiter = { ' ' };
		foreach (string line in configFile.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)) {
			string[] words = line.Split(delimiter);
			if (words.Length != 9) {
				Debug.LogError("Config file has wrong number of tokens!");
				throw new FormatException();
			}

			string tileName = "tiles" + (curr++);
			tilesCounts[tileName] = int.Parse(words[0]);
			//tilesCounts.Add(int.Parse(words[0]));
			Dictionary<Direction, Direction> directions = new Dictionary<Direction, Direction>();
			for (int i = 0; i < 4; i++) {
				Direction from = (Direction)Enum.Parse(typeof(Direction), words[i * 2 + 1]);
				Direction where = (Direction)Enum.Parse(typeof(Direction), words[i * 2 + 2]);

				directions[from] = where;
			}
			tileConnections[tileName] = directions;
		}
	}

	private static void LoadSprites() {
		tileSprites = new Dictionary<string, Sprite>();
		Sprite[] sprites1 = Resources.LoadAll<Sprite>("Sprites/tiles1");
		Sprite[] sprites2 = Resources.LoadAll<Sprite>("Sprites/tiles2");
		Sprite[] sprites3 = Resources.LoadAll<Sprite>("Sprites/tiles3");

		foreach (Sprite s in sprites1) {
			tileSprites[s.name] = s;
		}
		foreach (Sprite s in sprites2) {
			tileSprites[s.name] = s;
		}
		foreach (Sprite s in sprites3) {
			tileSprites[s.name] = s;
		}
	}

	public static Dictionary<Direction, Direction> GetConnections(string type) {
		if (!tileConnections.ContainsKey(type)) {
			Debug.LogError("There is no tile that would be of type: " + type);
			return null;
		}

		Dictionary<Direction, Direction> conn = tileConnections[type];

		//Dictionary<Direction, Direction> connCop = new Dictionary<Direction, Direction>(conn);
		return conn;
	}

	public static Sprite GetSprite(string type) {
		return tileSprites[type];
	}
}
