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

	// Use this for initialization
	void Start ()
	{
	    Audio = GetComponent<AudioSource>();
        Anim = GetComponent<Animation>();
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
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
}
