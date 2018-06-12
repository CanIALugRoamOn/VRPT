using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConnectorHubUW;

public class FeedbackManager : MonoBehaviour {



#if NETFX_CORE
    public FeedbackHub myFeedbackHub;
#endif
    public Sprite[] IconCollection;
    public GameObject PTIconHolder;
    //public int listeningTCPPort;
    private UnityEngine.UI.Image IconImage;
    private UnityEngine.UI.Text IconText;
    // Only for testing
    public string[] FeedbackSequence;
    private int iterator = 1;
    private bool coroutine = true;

    private string feedback = "Good";
    // Use this for initialization
    void Start() {

#if NETFX_CORE

        myFeedbackHub = new FeedbackHub();
        myFeedbackHub.init(listeningTCPPort, listeningUDPPort);
        myFeedbackHub.feedbackReceivedEvent += MyFeedbackHub_feedbackReceivedEvent();
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

    // Update is called once per frame
    void Update() {
        // TO DO if feedback == "good" then ...     
        if (coroutine){
            IconImage.color = new Color32(255, 255, 255, 255);

            if (feedback.Contains("Good"))
            {
                IconImage.sprite = IconCollection[0];
                IconText.text = "Good !";
            }
            else if (feedback.Contains("Reset Posture"))
            {
                IconImage.sprite = IconCollection[1];
                IconText.text = "Reset Posture";
            }
            else if (feedback.Contains("Stand Still"))
            {
                IconImage.sprite = IconCollection[1];
                IconText.text = "Stand Still";
            }
            else if (feedback.Contains("Move Hands"))
            {
                IconImage.sprite = IconCollection[1];
                IconText.text = "Move Hands";
            }
            else if (feedback.Contains("Smile"))
            {
                IconImage.sprite = IconCollection[2];
                IconText.text = "Smile";
            }
            else if (feedback.Contains("Speak Louder"))
            {
                IconImage.sprite = IconCollection[3];
                IconText.text = "Speak Louder";
            }
            else if (feedback.Contains("Speak Softer"))
            {
                IconImage.sprite = IconCollection[4];
                IconText.text = "Speak Softer";
            }
            else if (feedback.Contains("Stop Speaking"))
            {
                IconImage.sprite = IconCollection[6];
                IconText.text = "Stop Speaking";
            }
            else if (feedback.Contains("Stop Hmmmm"))
            {
                IconImage.sprite = IconCollection[6];
                IconText.text = "Stop Hmm";
            }
            else if (feedback.Contains("Start Speaking"))
            {
                IconImage.sprite = IconCollection[5];
                IconText.text = "Start Speaking";
            }
            else
            {
                // wait for some seconds before resetting the icon
                IconImage.sprite = null;
                IconImage.color = new Color32(255, 255, 255, 0);
                IconText.text = "";
            }
            feedback = FeedbackSequence[iterator].ToString();
            if (iterator >= FeedbackSequence.Length - 1)
            {
                iterator = 0;
            }
            else
            {
                iterator++;
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

#if NETFX_CORE
    public myFeedbackHub myFeedbackHub_feedbackReceivedEvent(object sender,string feedback)
    {
        this.feedback = feedback;
    }
#endif
}
