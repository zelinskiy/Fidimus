﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class MainMenuSettingsSaver : MonoBehaviour {

    public Toggle PaintRoomToggle;
    public Toggle PaintCorridorToggle;

    public Button RunBigButton;
    public Button RunSmallButton;

    public Button ExitButton;

    // Use this for initialization
    void Start () {
        RunBigButton.onClick.AddListener(OnRunBigButtonClick);
        RunSmallButton.onClick.AddListener(OnRunSmallButtonClick);
        RunSmallButton.onClick.AddListener(OnExitButtonClick);
    }

    void OnRunBigButtonClick()
    {
        SaveSettings();
        print("Big");
    }

    void OnRunSmallButtonClick()
    {
        SaveSettings();
        print("Small");
    }

    void OnExitButtonClick()
    {
        SaveSettings();
        print("Exit");
    }

    void SaveSettings()
    {
        var pathToSettings = Path.Combine(Application.dataPath, "settings.txt");
        var settings = new Dictionary<string, string>();
        if(!SettingsLoader.TryLoadSettings(out settings, pathToSettings))
        {
            settings = SettingsLoader.DefaultSettings;
        }
        

        var newSettingsLines = new List<string>
        {
            "PaintCorridors=" + PaintCorridorToggle.isOn.ToString().ToLower(),
            "PaintRooms=" + PaintRoomToggle.isOn.ToString().ToLower()
        };

        foreach(var k in settings.Keys)
        {
            if(k != "PaintCorridors" && k != "PaintRooms")
            {
                newSettingsLines.Add(k + "=" + settings[k]);
            }
        }

        if (File.Exists(pathToSettings))
        {
            File.WriteAllLines(pathToSettings, newSettingsLines.ToArray());
        }
        else
        {
            Debug.LogError("file " + pathToSettings + " not found");
        }
       
    }

}
