using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
        
    public GameObject LeftDoor;
    public GameObject RightDoor;

    public ButtonController OuterButton;
    public ButtonController InnerButton;

    private Animation leftAnim;
    private Animation rightAnim;

    private bool IsClosed = true;

    // Use this for initialization
    void Start ()
    {
        leftAnim = LeftDoor.GetComponent<Animation>();
        rightAnim = RightDoor.GetComponent<Animation>();

        InnerButton.OnPressed += ToggleDoors;
        OuterButton.OnPressed += ToggleDoors;
    }

    void ToggleDoors()
    {
        if (!leftAnim.isPlaying && !rightAnim.isPlaying)
        {
            if (IsClosed)
            {
                print("opening");
                leftAnim.Play("OpenLeftDoor");
                rightAnim.Play("OpenRightDoor");
                LeftDoor.GetComponent<AudioSource>().Play();
                RightDoor.GetComponent<AudioSource>().Play();
                OuterButton.Color = Color.black;
                InnerButton.Color = Color.black;
            }
            else
            {
                print("closing");
                leftAnim.Play("CloseLeftDoor");
                rightAnim.Play("CloseRightDoor");
                LeftDoor.GetComponent<AudioSource>().Play();
                RightDoor.GetComponent<AudioSource>().Play();
                OuterButton.Color = Color.red;
                InnerButton.Color = Color.red;
            }            
            IsClosed = !IsClosed;   
        }
    }
    
}
