using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpotLightsController : MonoBehaviour {

    public GameObject TopLeft;
    public GameObject TopRight;
    public GameObject BottomLeft;
    public GameObject BottomMiddle;
    public GameObject BottomRight;

    private List<GameObject> All;
    private List<GameObject> Bottom;
    private List<GameObject> Top;


    // Use this for initialization
    void Start ()
    {
        Top = new List<GameObject>
        {
            TopLeft,
            TopRight
        };
        Bottom = new List<GameObject>
        {
            BottomLeft,
            BottomMiddle,
            BottomRight
        };

        All = new List<GameObject>
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomMiddle,
            BottomRight
        };

        DisableAll();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void DisableAll()
    {
        setLightsActive(false, All);
    }

    static void setLightsActive(bool active, IEnumerable<GameObject> lights)
    {
        foreach (var o in lights)
        {
            o.SetActive(active);
        }
    }

    static void toggleLightsActive(IEnumerable<GameObject> lights)
    {
        foreach (var o in lights)
        {
            o.SetActive(!o.activeSelf);
        }
    }

    void ToggleTop()
    {
        toggleLightsActive(Top);
    }

    void ToggleBottom()
    {
        toggleLightsActive(Bottom);
    }
    
}
