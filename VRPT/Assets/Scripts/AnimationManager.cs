using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public GameObject feedMan;
    private FeedbackManager feed;
    public bool isMale;
    private Animator anim;
    private double boredom = 0;

	// Use this for initialization
	void Start () {

        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isMale", isMale);

        feed = feedMan.GetComponent<FeedbackManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
        

	}
}
