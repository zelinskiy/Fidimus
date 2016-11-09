using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;


public class TipTextController : MonoBehaviour {

    private Text TipText;

    // Use this for initialization
    void Start ()
    {
        TipText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public IEnumerator ShowMessage(string msg, int duration)
    {
        TipText.text = msg;
        yield return new WaitForSeconds(duration);
        TipText.text = "";
    }

}
