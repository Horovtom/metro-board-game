using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRepository {

    private static Dictionary<string, Sprite> tileSprites;

    public static void LoadSprites() {
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

    public static Sprite getSprite(string type) {
        return tileSprites[type];
            }
}
