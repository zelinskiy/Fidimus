using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenuSettingsSaver : MonoBehaviour {

    public Toggle PaintRoomToggle;
    public Toggle PaintCorridorToggle;

    public Button RunBigButton;
    public Button RunSmallButton;

    public Button ExitButton;

    public Button OpenFolderButton;

    // Use this for initialization
    void Start () {
        RunBigButton.onClick.AddListener(OnRunBigButtonClick);
        RunSmallButton.onClick.AddListener(OnRunSmallButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
        OpenFolderButton.onClick.AddListener(OnOpenFolderButtonClick);
    }

    void OnRunBigButtonClick()
    {
        SaveSettings();
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        print("Big");
    }

    void OnRunSmallButtonClick()
    {
        SaveSettings();
        SceneManager.LoadScene("SingleRoom", LoadSceneMode.Single);
        print("Small");
    }

    void OnExitButtonClick()
    {
        SaveSettings();
        Application.Quit();
        print("Exit");
    }

    void OnOpenFolderButtonClick()
    {
        System.Diagnostics.Process.Start(Path.Combine(Application.dataPath, "MyPictures"));
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
