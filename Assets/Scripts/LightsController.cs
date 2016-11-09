using UnityEngine;
using System.Collections;

public class LightsController : MonoBehaviour
{

    public GameObject PointLights;
    public GameObject SpotLights;

    public ButtonController PointLightsSwitcher;
    public ButtonController TopSpotLightsSwitcher;
    public ButtonController BottomSpotLightsSwitcher;

    // Use this for initialization
    void Start ()
    {
        PointLightsSwitcher.OnPressed += () =>
        {
            PointLights.SendMessage("NextMode");
        };
        TopSpotLightsSwitcher.OnPressed += () =>
        {
            SpotLights.SendMessage("ToggleTop");
        };
        BottomSpotLightsSwitcher.OnPressed += () =>
        {
            SpotLights.SendMessage("ToggleBottom");
        };

    }
	
	// Update is called once per frame
	void Update () {
    }
}
