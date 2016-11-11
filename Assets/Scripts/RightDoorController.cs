using UnityEngine;
using System.Collections;

public class RightDoorController : MonoBehaviour {

    private Animation OpenDoor;

	// Use this for initialization
	void Start () {
        OpenDoor = GetComponent<Animation>();
        GetComponent<Animation>().Play("OpenRightDoor");
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Animation>();
	}

    void Open()
    {
        OpenDoor.Play("OpenRightDoor");
    }


}
