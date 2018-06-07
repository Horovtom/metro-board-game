using UnityEngine;

public enum PlayerColor {
    /// <summary>
    /// Yellow
    /// </summary>
    Y = 0,
    /// <summary>
    /// Blue
    /// </summary>
    B = 1,
    /// <summary>
    /// Orange
    /// </summary>
    O = 2,
    /// <summary>
    /// Green
    /// </summary>
    G = 3,
    /// <summary>
    /// Red
    /// </summary>
    R = 4,
    /// <summary>
    /// Violet
    /// </summary>
    V = 5,
    None = -1
}

static class PlayerColorMethods {
    public static Color GetColor(this PlayerColor pc) {
        switch(pc) {
            case PlayerColor.Y:
                return Color.yellow;
            case PlayerColor.B:
                return Color.blue;
            case PlayerColor.O:
                return new Color(1, 127/255f, 80/255f);
            case PlayerColor.G:
                return Color.green;
            case PlayerColor.R:
                return Color.red;
            case PlayerColor.V:
                return new Color(238f / 255f, 130f / 255f, 238 / 255f);
            default:
                return new Color(0, 0, 0);
        }
    }

    public static string GetColorName(this PlayerColor pc) {
        switch(pc) {
            case PlayerColor.Y:
                return "Yellow";
            case PlayerColor.B:
                return "Blue";
            case PlayerColor.O:
                return "Orange";
            case PlayerColor.G:
                return "Green";
            case PlayerColor.R:
                return "Red";
            case PlayerColor.V:
                return "Violet";
            default:
                return "None";
        }
    }
}