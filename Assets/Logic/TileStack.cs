using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileStack {
	private static Stack<string> tileStack;

	public static string PopStack() {
		return tileStack.Pop();
	}

	public static void CreateStack(Dictionary<string, int> tilesCounts) {
		tileStack = new Stack<string>();
		string[] arr = new string[0];
		tilesCounts.Keys.CopyTo(arr, 0);
		for (int i = 0; i < arr.Length; i++) {
			int count = tilesCounts[arr[i]];
			for (int j = 0; j < count; j++) {
				tileStack.Push(arr[i]);
			}
		}

		ShuffleStack();
	}

	private static void ShuffleStack() {
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
