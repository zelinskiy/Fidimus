using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.VR;

public class GalleryModuleController : MonoBehaviour
{

    public GameObject Painting;
    public TextMesh EntranceLabel;
    public Transform PaintingNameLabels;

    public ButtonController PlayAttachedClipButton;

    private AudioSource _audioSource;
    private const string testClip = "file:///C:/_Projects/Fidimus/Assets/MyPictures/GrandBudapestHotel/test1.ogg";
    public string ClipPath = "";

    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        PlayAttachedClipButton.OnPressed += () =>
        {
            if (ClipPath == "")
            {
                return;
            }
            if (_audioSource.clip == null)
            {
                StartCoroutine(LoadAndPlayAudio(ClipPath));
                return;
            }
            if (_audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
            else
            {
                _audioSource.Play();
            }
            print("bang! " + _audioSource.clip.length);

        };
    }

    // Update is called once per frame
    void Update()
    {
        
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
