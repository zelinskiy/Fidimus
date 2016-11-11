using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;

public class GalleryController : MonoBehaviour {

    public string CustomPicturesFolderName = "MyPictures";
    public GameObject GalleryBegin;
    public GameObject CorridorSegment;

    public bool PaintRooms = true;
    public bool PaintCorridors = false;


    private float corridorSegmentLength = -16f;
    private float corridorSegmentWidth = -10f;

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
        var rootfolder = Path.Combine(
            Application.dataPath,
            CustomPicturesFolderName);

        var folders = Directory
            .GetDirectories(rootfolder)
            .ToArray();
            
        if (folders.Length == 0)
        {
            Debug.LogWarning("Folders in " + rootfolder + " not found!");
            return;
        }

        for (int i = 0; i < folders.Length; i++)
        {
            var folder = folders[i];

            var files = Directory
                .GetFiles(folder)
                .Where(f => 
                    f.EndsWith(".png"))
                .ToArray();

            var clips = Directory
                .GetFiles(folder)
                .Where(f =>
                    f.EndsWith(".ogg"))
                .ToArray();

            if (files.Length == 0)
            {
                Debug.LogWarning("Files in " + folder + " not found!");
                break;
            }

            if (clips.Length == 0)
            {
                Debug.LogWarning("Clips in " + folder + " not found!");
            }

            var corridorColor = GalleryModuleController.GetRandomColor(false);

            for (int j = 0; j < files.Length; j++)
            {
                var filename =  files[j];

                var description = new FileDescription()
                {
                    CreatedAt = File.GetCreationTime(filename).ToString("dd.MM.yyyy HH:mm"),
                    LastAccess = File.GetLastAccessTime(filename).ToString("dd.MM.yyyy HH:mm"),
                    LastWrite = File.GetLastWriteTime(filename).ToString("dd.MM.yyyy HH:mm"),
                    FileName = filename.Split('\\', '/').Last()
                };

                var rot = GalleryBegin.transform.rotation;
                var pos = GalleryBegin.transform.position;
                pos.z += corridorSegmentLength * (j + 1);
                pos.x = corridorSegmentWidth * (i + 1);
                var newSeg = (GameObject)Instantiate(CorridorSegment, pos, rot);
                newSeg.SendMessage("LoadPicture", filename);
                newSeg.transform.parent = GalleryBegin.transform;


                
                var controller = newSeg.GetComponent<GalleryModuleController>();

                var name = filename.Substring(0, filename.Length - 4);
                var attachedClip = clips.FirstOrDefault(c => c.EndsWith(name + ".ogg"));
                if (attachedClip != null)
                {
                    attachedClip = @"file:///" + attachedClip.Replace("\\", "/");
                    attachedClip = attachedClip.Replace(" ", "%20");
                    print("Found clip " + attachedClip);
                    
                    controller.ClipPath = attachedClip;
                    description.AudioAttached = attachedClip.Split('/', '\\').Last();
                }

                if (PaintCorridors)
                {
                    controller.RoomColor = corridorColor;
                }
                if (PaintRooms)
                {
                    controller.RoomColor = GalleryModuleController.GetRandomColor(false);
                }

                controller.SendMessage("SetDescription", description);

                if(j == files.Length - 1)
                {
                    controller.CorridorEnd.SetActive(true);
                }

            }

        }

        

        
    }

    
}
