using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;

public class GalleryModuleController : MonoBehaviour
{

    public GameObject Painting;
    

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadPicture(string filePath)
    {
        var fileData = File.ReadAllBytes(filePath);
        var tex = new Texture2D(1, 1);
        tex.LoadImage(fileData); 
        Painting.GetComponent<MeshRenderer>().material.mainTexture = tex;
        
    }
}
