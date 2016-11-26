using UnityEngine;
using System.Collections;

public class PointLightsController : MonoBehaviour
{
    private int currentState = 0;

	// Use this for initialization
	void Start ()
	{
        DisableAll();


    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void DisableAll()
    {
        foreach (Transform c in transform)
        {
            c.gameObject.SetActive(false);
        }
    }

    void NextMode()
    {
        currentState++;
        if (currentState == transform.childCount / 2)
        {
            currentState = 0;
        }
        if (currentState == 0)
        {
            DisableAll();
            return;
        }
        LightEvery(currentState);

    }

    void LightEvery(int n)
    {
        DisableAll();
        for (int i = 0 ; i < transform.childCount; i+=n)
        {
            var c = transform.GetChild(i);
            c.gameObject.SetActive(!c.gameObject.activeSelf);
        }
    }
}
