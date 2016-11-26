using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.VR;

public class FileDescription
{
    public string FileName { get; set; }
    public string AudioAttached { get; set; }
    public string LastAccess { get; set; }
    public string LastWrite { get; set; }
    public string CreatedAt { get; set; }
}

public class GalleryModuleController : MonoBehaviour
{

    public GameObject Painting;
    public TextMesh EntranceLabel;
    public TextMesh DescriptionLabel;
    public Transform PaintingNameLabels;
    public GameObject CorridorEnd;

    public ButtonController PlayAttachedClipButton;

    public Color? RoomColor = null;

    private AudioSource _audioSource;
    private const string testClip = "file:///C:/_Projects/Fidimus/Assets/MyPictures/GrandBudapestHotel/test1.ogg";
    public string ClipPath = "";

    private static readonly System.Random rand = new System.Random();

    private static readonly Color[] RandomColors = 
    {
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow
    };

    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        PlayAttachedClipButton.OnPressed += ToggleAudioPlaying;
        PlayAttachedClipButton.Color = Color.black;
        UpdateColor();
    }

    public static Color GetRandomColor(bool fromPreferred)
    {
        if (fromPreferred)
        {
            return RandomColors[rand.Next(0, RandomColors.Length - 1)];
        }
        else
        {
            return new Color(
                UnityEngine.Random.value, 
                UnityEngine.Random.value, 
                UnityEngine.Random.value);
        }        
    }

    void ToggleAudioPlaying()
    {
        if (ClipPath == "")
        {
            return;
        }
        if (_audioSource.clip == null)
        {
            PlayAttachedClipButton.Color = Color.red;
            StartCoroutine(LoadAndPlayAudio(ClipPath));
            return;
        }
        if (_audioSource.isPlaying)
        {
            PlayAttachedClipButton.Color = Color.black;
            _audioSource.Pause();
        }
        else
        {
            PlayAttachedClipButton.Color = Color.red;
            _audioSource.Play();
        }
        print("bang! " + _audioSource.clip.length);
    }

    
    void LoadPicture(string filePath)
    {
        var fileData = File.ReadAllBytes(filePath);
        var tex = new Texture2D(1, 1);
        tex.LoadImage(fileData);
        Painting.GetComponent<MeshRenderer>().material.mainTexture = tex;
        foreach (Transform c in PaintingNameLabels.transform)
        {
            var filename = filePath
                .Split('\\', '/')
                .Last()
                .insertEvery("\n", 16);
            filename = filename.Substring(0, filename.Length - 4);
            c.gameObject.GetComponent<TextMesh>().text = filename;
        }
        EntranceLabel.text = filePath
            .Split('\\', '/')
            .Reverse()
            .Take(2)
            .Last()
            .insertEvery("\n", 8);
    }

    void SetDescription(FileDescription desc)
    {
        if(desc.AudioAttached == null)
        {
            desc.AudioAttached = "None";
        }

        DescriptionLabel.text = "D E S C R I P T I O N\n"
            + "File name: \n" + desc.FileName + "\n"
            + "Audio attached: \n" + desc.AudioAttached + "\n"
            + "Created at: \n" + desc.CreatedAt + "\n"
            + "Last modified: \n" + desc.LastWrite + "\n"
            + "Last accessed: \n" + desc.LastAccess;
    }

    void UpdateColor()
    {
        print(GetComponent<Renderer>().material.color);
        if (RoomColor != null)
        {
            GetComponent<Renderer>().material.color = RoomColor.Value;
        }
    }

    IEnumerator LoadAndPlayAudio(string filePath)
    {
        var download = new WWW(filePath);
        yield return download;
        var clip = download.audioClip;
        if (clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        else
        {
            Debug.Log("Ogg vorbis download failed. (Incorrect link?)");
        }
        
    }


    



}

public static class Extensions
{
    public static string insertEvery(this string text, string sep, int n)
    {
        for (int i = n; i <= text.Length; i += n)
        {
            text = text.Insert(i, sep);
            i++;
        }
        return text;
    }
}
