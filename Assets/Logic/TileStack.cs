using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileStack {
	private Stack<string> tileStack;

	public int Length { get { return tileStack.Count; } }

	public TileStack(List<string> names, Dictionary<string, int> tilesCounts) {
		CreateStack(names, tilesCounts);
	}

	public string PopStack() {
		return tileStack.Pop();
	}

	public void CreateStack(List<string> names, Dictionary<string, int> tilesCounts) {
		tileStack = new Stack<string>();

		for (int i = 0; i < names.Count; i++) {
			int count = tilesCounts[names[i]];
			for (int j = 0; j < count; j++) {
				tileStack.Push(names[i]);
			}
		}

		ShuffleStack();
	}

	private void ShuffleStack() {
		List<string> shuffle = new List<string>(tileStack);

		System.Random _random = new System.Random();

		string myTile;

		int n = shuffle.Count;
		for (int i = 0; i < n; i++) {
			// NextDouble returns a random number between 0 and 1.
			// ... It is equivalent to Math.random() in Java.
			int r = i + (int)(_random.NextDouble() * (n - i));
			myTile = shuffle[r];
			shuffle[r] = shuffle[i];
			shuffle[i] = myTile;
		}

		tileStack = new Stack<string>(shuffle);
	}


}
