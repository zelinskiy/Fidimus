using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

public class SettingsLoader : MonoBehaviour {

    static readonly string[] RequiredSettings = new string[]
    {
        "CustomFolderPath",
        "CustomFolderSet",
        "PaintCorridors",
        "PaintRooms"
    };

    public static readonly Dictionary<string, string> DefaultSettings = new Dictionary<string, string>()
    {
        { "CustomFolderPath", "NONE" },
        { "CustomFolderSet", "false" },
        { "PaintCorridors", "false" },
        { "PaintRooms", "false" },
    };

    public static void LoadSettingsToGallery(IHasSettings gallery)
    {
        Dictionary<string, string> settingsDict;
        var pathToSettings = Path.Combine(Application.dataPath, "settings.txt");
        if (TryLoadSettings(out settingsDict, pathToSettings))
        {
            gallery.CustomPicturesFolderSet = settingsDict["CustomFolderSet"] == "true";
            print("CustomFolderSet = " + gallery.CustomPicturesFolderSet.ToString());
            gallery.CustomPicturesFolderPath = settingsDict["CustomFolderPath"];
            print("CustomPicturesFolderPath = " + gallery.CustomPicturesFolderPath);
            gallery.PaintCorridors = settingsDict["PaintCorridors"] == "true";
            print("PaintCorridors = " + gallery.PaintCorridors.ToString());
            gallery.PaintRooms = settingsDict["PaintRooms"] == "true";
            print("PaintRooms = " + gallery.PaintRooms.ToString());
        }
    }


    public static bool TryLoadSettings(out Dictionary<string, string> res, string filePath)
    {
        res = new Dictionary<string, string>();
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            
            foreach (var l in lines)
            {                
                var key = l.Split('=')[0];
                var value = l.Split('=')[1];
                res.Add(key, value);
            }
            foreach (var k in RequiredSettings)
            {
                if (!res.Keys.Contains(k))
                {
                    print("Setting " + k + " not found!");
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            print(ex.Message);
            return false;
        }
    }

}
