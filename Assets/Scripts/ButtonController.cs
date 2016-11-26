using System;
using UnityEngine;
using System.Collections;



public class ButtonController : MonoBehaviour
{

    public delegate void PressEventHandler();
    public event PressEventHandler OnPressed;

    public Color ActiveColor;

    private Animation Anim;
    private AudioSource Audio;

    public Color Color
    {
        get
        {
            return GetComponent<Renderer>().material.color;
        }
        set
        {
            GetComponent<Renderer>().material.color = value;
        }
    }

	// Use this for initialization
	void Start ()
	{
        OnPressed += () => {};
	    Audio = GetComponent<AudioSource>();
        Anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Press()
    {
        OnPressed.Invoke();
        Audio.Play();
        Anim.Play("ButtonPressing");
    }

    

    public void DisableButton()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void EnableButton()
    {
        transform.parent.gameObject.SetActive(true);
    }
}
