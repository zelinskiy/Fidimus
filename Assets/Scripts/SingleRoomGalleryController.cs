using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Pic
{
    public string Path;
    public Color Color;
    public string Clip;
    public FileDescription Description;
}

internal class PicsFolder
{
    public string Path;
    public Pic[] Pics;
}

public class SingleRoomGalleryController : MonoBehaviour, IHasSettings {

    public string DefaultPicturesFolderName = "MyPictures";
    public string CustomPicturesFolderPath { get; set; }
    public bool CustomPicturesFolderSet { get; set; }
    public bool PaintCorridors { get; set; }
    public bool PaintRooms { get; set; }

    public ButtonController NextFolderButtonController;
    public ButtonController NextFileButtonController;
    public ButtonController PrevFolderButtonController;
    public ButtonController PrevFileButtonController;

    private GalleryModuleController Room;
    private PicsFolder[] Folders;

    private int FolderIndex;
    private int PicIndex;

    // Use this for initialization
    void Start ()
    {
        SettingsLoader.LoadSettingsToGallery(this);
        Room = GetComponent<GalleryModuleController>();
        GenerateGallery();
        NextFolderButtonController.OnPressed += NextFolder;
        NextFileButtonController.OnPressed += NextPicture;
        PrevFolderButtonController.OnPressed += PrevFolder;
        PrevFileButtonController.OnPressed += PrevPicture;
        NextPicture();
    }

    void NextFolder()
    {
        PicIndex = 0;
        FolderIndex++;
        if (FolderIndex >= Folders.Length)
        {
            FolderIndex = 0;
        }
        LoadFile(Folders[FolderIndex].Pics[PicIndex]);
    }

    void NextPicture()
    {
        PicIndex++;
        if (PicIndex >= Folders[FolderIndex].Pics.Length)
        {
            PicIndex = 0;
        }
        LoadFile(Folders[FolderIndex].Pics[PicIndex]);
    }

    void PrevFolder()
    {
        PicIndex = 0;
        FolderIndex--;
        if (FolderIndex < 0)
        {
            FolderIndex = Folders.Length - 1;
        }
        LoadFile(Folders[FolderIndex].Pics[PicIndex]);
    }

    void PrevPicture()
    {
        PicIndex--;
        if (PicIndex < 0)
        {
            PicIndex = Folders[FolderIndex].Pics.Length - 1;
        }
        LoadFile(Folders[FolderIndex].Pics[PicIndex]);
    }

    void LoadFile(Pic pic)
    {
        Room.ClipPath = pic.Clip;
        Room.RoomColor = pic.Color;
        Room.SendMessage("SetDescription", pic.Description);
        Room.SendMessage("LoadPicture", pic.Path);
        Room.SendMessage("UpdateColor");
    }


    void GenerateGallery()
    {
        var rootfolder = Path.Combine(
            Application.dataPath,
            DefaultPicturesFolderName);
        
        if (CustomPicturesFolderSet)
        {
            rootfolder = CustomPicturesFolderPath;
        }

        Folders = Directory
            .GetDirectories(rootfolder)
            .Select(f => new PicsFolder()
            {
                Path = f
            })
            .ToArray();

        if (Folders.Length == 0)
        {
            Debug.LogWarning("Folders in " + rootfolder + " not found!");
            return;
        }

        foreach(var folder in Folders)
        {
            folder.Pics = Directory
                .GetFiles(folder.Path)
                .Where(f =>
                    f.EndsWith(".png"))
                .Select(f => new Pic {Path = f} )
                .ToArray();

            if (folder.Pics.Length == 0)
            {
                Debug.LogWarning("Files in " + folder.Path + " not found!");
                break;
            }

            var corridorColor = GalleryModuleController.GetRandomColor(false);


            foreach(var pic in folder.Pics)
            {



                var filenameNoExtension = pic
                    .Path
                    .Split('/', '\\')
                    .Last()
                    .Split('.')
                    .First();

                pic.Description = new FileDescription
                {
                    CreatedAt = File
                        .GetCreationTime(pic.Path)
                        .ToString("dd.MM.yyyy HH:mm"),
                    LastAccess = File
                        .GetLastAccessTime(pic.Path)
                        .ToString("dd.MM.yyyy HH:mm"),
                    LastWrite = File
                        .GetLastWriteTime(pic.Path)
                        .ToString("dd.MM.yyyy HH:mm"),
                    FileName = pic.Path.Split('/', '\\').Last()
                };

                var attachedClip = Directory
                    .GetFiles(folder.Path)
                    .FirstOrDefault(f => f.EndsWith(filenameNoExtension + ".ogg"));

                if (attachedClip != null)
                {
                    attachedClip = @"file:///" + attachedClip.Replace("\\", "/");
                    attachedClip = attachedClip.Replace(" ", "%20");
                    pic.Description.AudioAttached = attachedClip.Split('/').Last();
                    pic.Clip = attachedClip;
                }

                if (PaintCorridors)
                {
                    pic.Color = corridorColor;
                }
                if (PaintRooms)
                {
                    pic.Color = GalleryModuleController.GetRandomColor(false);
                }

            }

        }




    }
}
