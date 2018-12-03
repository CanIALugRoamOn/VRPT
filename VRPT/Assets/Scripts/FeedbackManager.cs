using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FeedbackManager : MonoBehaviour {



#if NETFX_CORE
    public ConnectorHubUW.FeedbackHub myFeedbackHub;
#endif
    public Sprite[] IconCollection;
    public GameObject PTIconHolder;
    public int listeningTCPPort= 15002;
    public int listeningUDPPort=16002;
    private UnityEngine.UI.Image IconImage;
    private UnityEngine.UI.Text IconText;
    // Only for testing
    public string[] FeedbackSequence;
    private float iterator = 1;
    private bool coroutine = true;
    public event EventHandler<string> FeedbackArrivedEvent;

    private string feedback = "Good";
    //readonly public string[] feedbackMessages;

    // Use this for initialization
    void Start() {

#if NETFX_CORE

        myFeedbackHub = new ConnectorHubUW.FeedbackHub();
        myFeedbackHub.init(listeningTCPPort, listeningUDPPort);
        myFeedbackHub.feedbackReceivedEvent += MyFeedbackHub_feedbackReceivedEvent;
#endif
        // Get Image component of the Icon
        IconImage = PTIconHolder.GetComponent<UnityEngine.UI.Image>();
        // make sure to be transparent
        IconImage.color = new Color32(255, 255, 255, 0);

        // Get Text Component of the Icon
        IconText = PTIconHolder.GetComponentInChildren<UnityEngine.UI.Text>();

        feedback = "";
        feedback = FeedbackSequence[0].ToString();

    }

    public string getFeedback()
    {
        return feedback;
    }

    // Update is called once per frame
    void Update() {
        // TO DO if feedback == "good" then ...     
        if (coroutine){
            IconImage.color = new Color32(255, 255, 255, 255);

            if (feedback.Contains("Good"))
            {
                //IconImage.sprite = IconCollection[0];
                //IconText.text = "Good !";
                IconImage.sprite = null;
                IconImage.color = new Color(255, 255, 255, 0);
                IconText.text = "";
                FeedbackArrivedEvent?.Invoke(this, "Good");
            }
            else if (feedback.Contains("Reset Posture"))
            {
                IconImage.sprite = IconCollection[1];
                IconText.text = "Reset Posture";
                FeedbackArrivedEvent?.Invoke(this, "Reset Posture");
            }
            else if (feedback.Contains("Stand Still"))
            {
                IconImage.sprite = IconCollection[1];
                IconText.text = "Stand Still";
                FeedbackArrivedEvent?.Invoke(this, "Stand Still");
            }
            else if (feedback.Contains("Move Hands"))
            {
                IconImage.sprite = IconCollection[1];
                IconText.text = "Move Hands";
                FeedbackArrivedEvent?.Invoke(this, "Move Hands");
            }
            else if (feedback.Contains("Smile"))
            {
                IconImage.sprite = IconCollection[2];
                IconText.text = "Smile";
                FeedbackArrivedEvent?.Invoke(this, "Smile");
            }
            else if (feedback.Contains("Speak Louder"))
            {
                IconImage.sprite = IconCollection[3];
                IconText.text = "Speak Louder";
                FeedbackArrivedEvent?.Invoke(this, "Speak Louder");
            }
            else if (feedback.Contains("Speak Softer"))
            {
                IconImage.sprite = IconCollection[4];
                IconText.text = "Speak Softer";
                FeedbackArrivedEvent?.Invoke(this, "Speak Softer");
            }
            else if (feedback.Contains("Stop Speaking"))
            {
                IconImage.sprite = IconCollection[6];
                IconText.text = "Stop Speaking";
                FeedbackArrivedEvent?.Invoke(this, "Stop Speaking");
                // decrease attention value if it is for tooo long
                // start mumbling
            }
            else if (feedback.Contains("Stop Hmmmm"))
            {
                IconImage.sprite = IconCollection[6];
                IconText.text = "Stop Hmm";
                FeedbackArrivedEvent?.Invoke(this, "Stop Hmmmm");
            }
            else if (feedback.Contains("Start Speaking"))
            {
                IconImage.sprite = IconCollection[5];
                IconText.text = "Start Speaking";
                FeedbackArrivedEvent?.Invoke(this, "Start Speaking");
                // surprise animation
            }
            else
            {
                // wait for some seconds before resetting the icon
                IconImage.sprite = null;
                IconImage.color = new Color32(255, 255, 255, 0);
                IconText.text = "";
            }

            // simulate feedback messages
            feedback = FeedbackSequence[(int)iterator%10].ToString();
            if (iterator >= 100 - 1)
            {
                iterator = 0;
            }
            else
            {
                iterator += .01f;
                //just for testing
            }
        }
        else
        {   
            //StartCoroutine(WaitBeforeReset());
        }
        //coroutine = false;
        
        

    }

    public IEnumerator WaitBeforeReset()
    {
        yield return new WaitForSeconds(.5f);
        coroutine = true;
        
    }

    //#if NETFX_CORE
    private void MyFeedbackHub_feedbackReceivedEvent(object sender, string feedback)
    {
        this.feedback = feedback;
    }
//#endif
}
