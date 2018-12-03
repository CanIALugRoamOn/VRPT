using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class RecordingManager : MonoBehaviour {

    public GameObject ButtonPlay;
    public GameObject ButtonPause;
    public GameObject ButtonStop;

    public bool isRecording { get; private set; } = false ;
    public float minutesPast { get; private set; } = 0;


    private InputField Input; 
    public GameObject Menu;
    public GameObject Init;
    // init ip adress of machine in network
    private string IPAddress = "192.168.70.53";
    private string port = "12345";



	// Use this for initialization
	void Start () {
        //Input = GameObject.Find("InputField").GetComponent<InputField>();
        IPAddress = "192.168.70.53";///just for now lets check how to add a keyboard to input the IP
        //IPAddress = Input.text;

        //Button StartApp = GameObject.Find("StartApp").GetComponent<Button>();
        //StartApp.onClick.AddListener(() => TaskOnClick(StartApp));

        //Button Play = ButtonPlay.GetComponent<Button>();
        //Play.onClick.AddListener(() => TaskOnClick(Play));

        //Button Pause = ButtonPause.GetComponent<Button>();
        //Pause.onClick.AddListener(() => TaskOnClick(Pause));

        //Button Stop = ButtonStop.GetComponent<Button>();
        //Stop.onClick.AddListener(() => TaskOnClick(Stop));

    }
	
	// Update is called once per frame
	void Update () {

        if (isRecording)
        {
            minutesPast += Time.deltaTime;
        }
        //print(minutesPast.ToString());
        // get the IP Adress from the input
        // IPAddress = Input.GetComponent<InputField>().text; //TODO uncomment this 
    }

    public float GetCaptureTime()
    {
        return minutesPast;
    }

   public void TaskOnClick(Button Btn)
    {

        switch (Btn.name)
        {
            case "StartApp":
                if (IPAddress != "")
                {
                    // change panels
                    Menu.SetActive(true);
                    Init.SetActive(false);
                    sendMessage("<START APPLICATIONS>");               
                }
                else
                {
                    Input.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "IP Adress?";
                    // just for testing
                    IPAddress = "192.168.70.53";
                    // change panels
                    Menu.SetActive(true);
                    Init.SetActive(false);
                    sendMessage("<START APPLICATIONS>");
                    //until here
                }

                break;
            case "Play":
                sendMessage("<START RECORDING>");
                isRecording = true;
                break;
            case "Pause":
                sendMessage("<STOP RECORDING>");
                isRecording = false;
                break;
            case "Stop":
                sendMessage("<STOP RECORDING>");
                minutesPast = 0;
                isRecording = false;
                break;
            case "Finish":
                sendMessage("<FINISCH>");
                break;
        }

    }


    // the messages can be: "<START APPLICATIONS>" "<START RECORDING>" "<STOP RECORDING>" "<FINISH>"
    public async void sendMessage(string message)
    {

#if !UNITY_EDITOR
        try
        {
            // Create the StreamSocket 
            using (var streamSocket = new Windows.Networking.Sockets.StreamSocket())
            {
                // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
                var hostName = new Windows.Networking.HostName(IPAddress);

                await streamSocket.ConnectAsync(hostName, port);

                using (Stream outputStream = streamSocket.OutputStream.AsStreamForWrite())
                {
                    using (var streamWriter = new StreamWriter(outputStream))
                    {
                        await streamWriter.WriteLineAsync(message);
                        await streamWriter.FlushAsync();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);

        }
#endif
        Debug.Log("sending message to learning hub");
    }

}
