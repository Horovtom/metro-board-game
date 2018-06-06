using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Tile {
	private Dictionary<Direction, Direction> connections;

	//Rotation to the right, what was north when rotation was 0, is now east, when rotation is 1
	private int rotation;

	public int Rotation { get { return rotation; } set { rotation = ((int)Math.Abs(value)) % 4; } }

	public string Type { get; protected set; }

	public Tile(string type) {
		Rotation = 0;
		Type = type;
		connections = TileRepository.GetConnections(type);
	}

	/// <summary>
	/// Returns whether two rotated directions are connected on this tile
	/// </summary>
	public Boolean IsConnected(Direction a, Direction b) {
		Direction rotatedA = (Direction)(((int)a + 4 - Rotation) % 4);
		Direction rotatedB = (Direction)(((int)b + 4 - Rotation) % 4);
		return connections[rotatedA] == rotatedB;
	}

	/// <summary>
	/// Returns rotated direction that specified rotated direction is connected to.
	/// </summary>
	public Direction GetConnection(Direction a) {
		Direction rotatedA = (Direction)(((int)a + 4 - Rotation) % 4);
		Direction rotatedB = connections[rotatedA];
		return (Direction)(((int)rotatedB + Rotation) % 4);
	}
}
