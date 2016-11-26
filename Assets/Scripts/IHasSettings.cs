using UnityEngine;
using System.Collections;

public interface IHasSettings
{
    string CustomPicturesFolderPath { get; set; }
    bool CustomPicturesFolderSet { get; set; }
    bool PaintRooms { get; set; }
    bool PaintCorridors { get; set; }
}
