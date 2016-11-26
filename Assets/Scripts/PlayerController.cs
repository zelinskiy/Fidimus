using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{

    public Text TipText;
    public Button ExitButton;

    private Camera camera;
    private RaycastHit hit;
    private Ray ray;

    private bool CanMove = true;

    // Use this for initialization
    void Start ()
	{
        ExitButton.onClick.AddListener(OnExitButtonClick);
	    camera = GetComponentInChildren<Camera>();          
    }
	
    void OnExitButtonClick()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void OnEscapePressed()
    {
        CanMove = !CanMove;
        if (CanMove)
        {
            GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            GetComponent<FirstPersonController>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapePressed();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            OnEscapePressed();
            OnExitButtonClick();
        }

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
