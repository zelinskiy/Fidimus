using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Text TipText;

    private Camera camera;
    private RaycastHit hit;
    private Ray ray;

    // Use this for initialization
    void Start ()
	{
	    camera = GetComponentInChildren<Camera>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Button")
            {
                TipText.text = "press E";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit
                        .transform
                        .gameObject
                        .GetComponent<ButtonController>()
                        .SendMessage("Press");
                }
            }
            else
            {
                TipText.text = "";
            }
        }
    }



}
