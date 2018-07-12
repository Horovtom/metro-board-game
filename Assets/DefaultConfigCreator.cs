using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefaultConfigCreator {
	public static void InitializeDefaults() {
		Debug.Log("Creating default configurations!");
		SetDefaultTilesConfig();
		SetDefaultScheduleConfig();

		PlayerPrefs.SetInt("DefaultsInitialized", 1);
	}

	/// <summary>
	/// This function will find out if there is some configuration set up, it will set it up if it is not.
	/// </summary>
	public static void PresentConfig() {
		if (!PlayerPrefs.HasKey("DefaultsInitialized"))
			DefaultConfigCreator.InitializeDefaults();
	}

	public static void SetDefaultScheduleConfig() {
		string defaultConfig = 
			"2 Y 1 3 5 7 9 11 13 15 17 19 21 23 25 27 29 31" + Environment.NewLine +
			"3 Y 1 4 6 11 15 20 23 25 28 31" + Environment.NewLine +
			"4 Y 4 7 11 16 20 23 27 32" + Environment.NewLine +
			"5 Y 1 5 10 14 22 28" + Environment.NewLine +
			"6 Y 1 5 10 19 27" + Environment.NewLine +
			"2 B 2 4 6 8 10 12 14 16 18 20 22 24 26 28 30 32" + Environment.NewLine +
			"3 B 2 7 9 12 14 19 22 27 29 32" + Environment.NewLine +
			"4 B 3 8 12 15 19 24 28 31" + Environment.NewLine +
			"5 B 6 12 18 23 27 32" + Environment.NewLine +
			"6 B 2 11 18 25 29" + Environment.NewLine +
			"3 O 3 5 8 10 13 18 21 24 26 30" + Environment.NewLine +
			"4 O 1 6 10 13 18 21 25 30" + Environment.NewLine +
			"5 O 3 7 15 19 25 29" + Environment.NewLine +
			"6 O 4 8 14 21 26" + Environment.NewLine +
			"4 G 2 5 9 14 17 22 26 29" + Environment.NewLine +
			"5 G 2 9 13 21 26 30" + Environment.NewLine +
			"6 G 6 15 20 24 31" + Environment.NewLine +
			"5 R 4 8 11 20 24 31" + Environment.NewLine +
			"6 R 3 9 13 23 30" + Environment.NewLine +
			"6 V 7 12 22 28 32";

		PlayerPrefs.SetString("ScheduleConfig", defaultConfig);
	}

	public static void SetDefaultTilesConfig() {
		string defaultConfig = 
			"2 N N E E S S W W" + Environment.NewLine +
			"2 N N S S E W W E" + Environment.NewLine +
			"2 N S S N E E W W" + Environment.NewLine +
			"2 N E E S S W W N" + Environment.NewLine +

			"2 W W S S E N N E" + Environment.NewLine +
			"2 W N N W S S E E" + Environment.NewLine +
			"2 S W W S E E N N" + Environment.NewLine +
			"2 N N E S S E W W" + Environment.NewLine +

			"2 N N E W W S S E" + Environment.NewLine +
			"2 N S S E E N W W" + Environment.NewLine +
			"2 N W W E S S E N" + Environment.NewLine +
			"2 W S S N N W E E" + Environment.NewLine +

			"2 N N S W W E E S" + Environment.NewLine +
			"2 W W S N N E E S" + Environment.NewLine +
			"2 S S N E E W W N" + Environment.NewLine +
			"2 E E N S S W W N" + Environment.NewLine +

			"4 W S S N N E E W" + Environment.NewLine +
			"4 S E E W W N N S" + Environment.NewLine +
			"4 W E E N N S S W" + Environment.NewLine +
			"4 W E E S S N N W" + Environment.NewLine +

			"3 W S S W N E E N" + Environment.NewLine +
			"3 W N N W S E E S" + Environment.NewLine +
			"4 W E E W S N N S" + Environment.NewLine +
			"2 W N N E E S S W" + Environment.NewLine;

		PlayerPrefs.SetString("TilesConfig", defaultConfig);
	}



	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}
}
