using UnityEngine;
using System.Collections;

public class LightsController : MonoBehaviour
{

    public GameObject PointLights;
    public GameObject SpotLights;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PointLights.SendMessage("NextMode");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpotLights.SendMessage("ToggleTop");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpotLights.SendMessage("ToggleBottom");
        }
    }
}
