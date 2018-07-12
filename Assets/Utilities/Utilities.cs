using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {
	/// <summary>
	/// This function will get instance of script running in gameObjects child
	/// </summary>
	/// <returns>The child script.</returns>
	/// <param name="go">Parent gameObject</param>
	public static T GetChildScript<T>(GameObject go, string nameOfChildGO) {
		Transform inputTransform = go.transform.Find(nameOfChildGO);
		if (inputTransform == null) {
			throw new MissingComponentException("Did not find child named: " + nameOfChildGO);
		} 

		GameObject inputGO = inputTransform.gameObject;
		if (inputGO == null) {
			throw new MissingComponentException("Transform of: " + nameOfChildGO + " did not have its game object!");
		}

		T controller = inputGO.GetComponent<T>();
		if (controller == null) {
			throw new MissingComponentException("Did not find script in " + nameOfChildGO);
		}

		return controller;
	}
}
