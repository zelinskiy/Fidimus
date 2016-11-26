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
        PointLightsSwitcher.Color = Color.black;
        TopSpotLightsSwitcher.Color = Color.black;
        BottomSpotLightsSwitcher.Color = Color.black;

        PointLightsSwitcher.OnPressed += () =>
        {
            PointLights.SendMessage("ToggleAll");
            if (PointLightsSwitcher.Color == Color.black)
            {
                PointLightsSwitcher.Color = Color.red;
            }
            else
            {
                PointLightsSwitcher.Color = Color.black;
            }
        };
        TopSpotLightsSwitcher.OnPressed += () =>
        {
            SpotLights.SendMessage("ToggleTop");
            if(TopSpotLightsSwitcher.Color == Color.black)
            {
                TopSpotLightsSwitcher.Color = Color.red;
            }
            else
            {
                TopSpotLightsSwitcher.Color = Color.black;
            }
           
        };
        BottomSpotLightsSwitcher.OnPressed += () =>
        {
            SpotLights.SendMessage("ToggleBottom");
            if (BottomSpotLightsSwitcher.Color == Color.black)
            {
                BottomSpotLightsSwitcher.Color = Color.red;
            }
            else
            {
                BottomSpotLightsSwitcher.Color = Color.black;
            }
        };

    }
	
	// Update is called once per frame
	void Update () {
    }
}
