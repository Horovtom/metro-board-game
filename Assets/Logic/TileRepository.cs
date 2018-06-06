using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileRepository {
	private Dictionary<string, int> tilesCounts;
	private Dictionary<string, Dictionary<Direction, Direction>> tileConnections;
	private TileStack tileStack;
	private List<string> tilesNames;

	public TileRepository() {
		CreateRepository();
		CreateStack();
	}

	public void CreateStack() {
		tileStack = new TileStack(tilesNames, tilesCounts);
	}

	public Tile PopStack() {
		if (tileStack == null) {
			Debug.LogError("tileStack was not initialized! Fixing...");
			CreateStack();
		}	
		return GetTile(tileStack.PopStack());
	}

	public int LeftInStack() {
		return tileStack.Length;
	}

	public void CreateRepository() {
		LoadTileConfiguration();
	}

	private void LoadTileConfiguration() {
		tileConnections = new Dictionary<string, Dictionary<Direction, Direction>>();
		tilesCounts = new Dictionary<string, int>();
		tilesNames = new List<string>();

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

			string tileName = curr++.ToString();
			tilesNames.Add(tileName);
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

	public Tile GetTile(int type) {
		return GetTile(type.ToString());
	}


	public Tile GetTile(string type) {
		if (tilesCounts == null) {
			Debug.LogError("TileRepository was not instantiated! Fixing...");
			CreateRepository();
		}

		if (!tilesCounts.ContainsKey(type))
			throw new ArgumentException("Tile type: " + type + " does not exist!");
		Tile t = new Tile(type, tileConnections[type]);
		t.Rotation = 2;
		return t;
	}

	public Dictionary<Direction, Direction> GetConnections(string type) {
		if (!tileConnections.ContainsKey(type)) {
			Debug.LogError("There is no tile that would be of type: " + type);
			return null;
		}

		Dictionary<Direction, Direction> conn = tileConnections[type];

		//Dictionary<Direction, Direction> connCop = new Dictionary<Direction, Direction>(conn);
		return conn;
	}


}
