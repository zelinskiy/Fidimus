using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;

public class GalleryController : MonoBehaviour {

    public string CustomPicturesFolderName = "MyPictures";
    public GameObject Corridor;
    public GameObject CorridorSegment;

    private const float corridorSegmentLength = -16f;

    // Use this for initialization
    void Start ()
    {
        GenerateGallery();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void GenerateGallery()
    {
        var folder = Path.Combine(
            Application.dataPath,
            CustomPicturesFolderName);

        var files = Directory
            .GetFiles(folder)
            .Where(f => f.EndsWith(".png"))
            .ToArray();

        if (files.Length == 0)
        {
            Debug.LogError("Files in " + folder + " not found!");
        }

        
        for(int i = 0; i < files.Length; i++)
        {
            var f = files[i];
            var rot = Corridor.transform.rotation;
            var pos = Corridor.transform.position;
            pos.z += corridorSegmentLength * (i + 1);
            var newSeg = (GameObject)Instantiate(CorridorSegment, pos, rot);
            newSeg.SendMessage("LoadPicture", f);
        }

        
    }

    
}
