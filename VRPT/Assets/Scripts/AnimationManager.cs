using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public GameObject feedMan;
    private FeedbackManager feed;
    public GameObject recordMan;
    private RecordingManager record;

    public bool isMale;
    private Animator anim;
    private float attention = 1f;

	// Use this for initialization
	void Start () {

        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isMale", isMale);

        
        feed = feedMan.GetComponent<FeedbackManager>();
        record = recordMan.GetComponent<RecordingManager>();

        //system.timers.timer timer;

        feed.FeedbackArrivedEvent += Feed_FeedbackArrivedEvent;
    }

    private void Feed_FeedbackArrivedEvent(object sender, string message)
    {
        // reset all the triggers
        anim.ResetTrigger("wasGood");
        anim.ResetTrigger("speakLouder");
        anim.ResetTrigger("askQuestion");

        if (record.isRecording)
        {           
            // 10% chance to trigger the animation
            float chance = Random.Range(0f, 100f);
            //print("#####");
            float attentionFactor = 1f;
            if (message.Contains("Good"))
            {
                attentionFactor = Random.Range(1f, 1.4f);
                if (chance < 0.1f)
                {
                    anim.SetTrigger("wasGood");
                    print(this.name);
                    print(chance);
                    print("was good");
                } 
            }
            else if (message.Contains("Reset Posture"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
            }
            else if (message.Contains("Stand Still"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
            }
            else if (message.Contains("Move Hands"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
                if (chance < 0.1f)
                {
                    anim.SetTrigger("askQuestion");
                    print(this.name);
                    print(chance);
                    print("ask question");
                }
            }
            else if (message.Contains("Smile"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
            }
            else if (message.Contains("Speak Louder"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
                if (chance < 0.1f)
                {
                    anim.SetTrigger("speakLouder");
                    print(this.name);
                    print(chance);
                    print("speak louder");
                }
            }
            else if (message.Contains("Speak Softer"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
            }
            else if (message.Contains("Stop Speaking"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
            }
            else if (message.Contains("Stop Hmmmm"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
            }
            else if (message.Contains("Start Speaking"))
            {
                attentionFactor = Random.Range(.8f, 1.2f);
            }
            else
            {
                // wait for some seconds before resetting the icon
                print("Unknown feedback message.");
                attentionFactor = 1;
            }

            // decrease the attention constantly
            //  and clip the attention factor
            attentionFactor = Mathf.Max(Mathf.Min(attentionFactor, 1.2f), .8f);
            attention = Mathf.Max(0, Mathf.Min(1,attentionCurve(record.minutesPast / 60) * attentionFactor));
            attention = 10f;
            //anim.SetFloat("attention", attention);
            
            //print(record.minutesPast / 60);
            //print(attentionFactor);
            //print(attention);
            //print("#####");
        }
    }

    // Update is called once per frame
    void Update () {

    }

    private float normal(float x, float mu, float sig)
    {
        return 1 / Mathf.Sqrt(2 * Mathf.PI * Mathf.Pow(sig, 2f)) * Mathf.Exp(-Mathf.Pow(x - mu, 2f) / (2 * Mathf.Pow(sig, 2f)));
    }

    private float attentionCurve(float x)
    {
        // curve for 50 minutes presentation
        float result = 0f;
        float muL = 0f;
        float muR = 50f;
        float  sigma = 5f;
        x = Mathf.Max(Mathf.Min(x, 50f), 0f);

        result = 11f * (normal(x, muL, sigma) + 0.3f * normal(x, muR, sigma)) + 0.1f;
        result = Mathf.Max(Mathf.Min(result, 1f), 0f);
        return result;
    }
}
