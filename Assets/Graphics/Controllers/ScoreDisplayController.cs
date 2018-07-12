using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreDisplayController : MonoBehaviour {
    private Dictionary<PlayerColor, Text> textBoxes;
    List<GameObject> textGO;

	// Use this for initialization
	void Awake () {
        textBoxes = null;
        textGO = null;
	}

    public void UpdateScores(Dictionary<PlayerColor, int> scores, PlayerColor onTurn) {
        if (textBoxes == null) {
            Debug.Log("TextBoxes were not Initialized yet! Fixing...");
            PlayerColor[] arr = scores.Keys.ToArray();
            createNewScoreDisplay(arr);
        }

        if (scores.Count != textGO.Count) {
            Debug.Log("Invalid length of updating array. Fixing...");
            PlayerColor[] arr = scores.Keys.ToArray();
            createNewScoreDisplay(arr);
        }

        foreach (KeyValuePair<PlayerColor, int> entry in scores) {
            if (!textBoxes.ContainsKey(entry.Key)) {
                Debug.LogError("Color passed to updating was not in score table!");
                continue;
            }
            textBoxes[entry.Key].text = createScoreString(entry.Key, scores[entry.Key], entry.Key == onTurn);
        }
    }

    private string createScoreString(PlayerColor color, int score, bool onTurn) {
        return color.GetColorName().PadRight(6) + " " + score.ToString().PadRight(4) + " " + (onTurn ? "<" : "");
    }

    public void createNewScoreDisplay(PlayerColor[] players) {
        if (textBoxes == null) {
            textBoxes = new Dictionary<PlayerColor, Text>();
            textGO = new List<GameObject>();
        }
        foreach (GameObject go in textGO) {
            Destroy(go);
        }
        textGO.Clear();
        textBoxes.Clear();

        int pos = 0;

        foreach (PlayerColor pc in players) {
            Debug.Log("Creating score for " + pc);
            GameObject go = new GameObject(pc.ToString() + " score");
            
            textGO.Add(go);
            go.transform.SetParent(this.transform, true);
            
            Text textField = go.AddComponent<Text>();
            textField.fontSize = 12;
            textField.color = pc.GetColor();
            textField.text = createScoreString(pc, 0, true);
            textField.font = Resources.Load<Font>("Fonts/DejaVuSansMono");

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(100, 14);
            //go.transform.localScale = new Vector3(1, 1, go.transform.localScale.z);
            go.transform.localPosition = new Vector3(0, -pos++ * 14, go.transform.localPosition.z);
            textBoxes[pc] = textField;
        }
    }
}
