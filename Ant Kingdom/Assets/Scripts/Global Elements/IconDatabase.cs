using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IconDatabase
{
    private static Dictionary<string, Sprite> iconDictionary;

    public static void Init() {
        iconDictionary = new Dictionary<string, Sprite>();
        string iconPath = @"UI Icons\";
        Sprite[] all = Resources.LoadAll<Sprite>(iconPath + "UI Icons");
        foreach (var icon in all) {
            if (icon.name == "Heart") {
                iconDictionary.Add("Heart", icon);
            }
        }
    }

    public static Sprite GetIcon(string s) {
        return iconDictionary[s];
    }
}
