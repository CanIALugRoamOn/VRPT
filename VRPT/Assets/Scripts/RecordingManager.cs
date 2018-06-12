using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordingManager : MonoBehaviour {

    public GameObject ButtonPlay;
    public GameObject ButtonPause;
    public GameObject ButtonStop;

    private InputField Input; 
    public GameObject Menu;
    public GameObject Init;
    // init ip adress of machine in network
    private string IPAdress;
    private string Port = "12345";



	// Use this for initialization
	void Start () {
        IPAdress = "";

        Input = GameObject.Find("InputField").GetComponent<InputField>();

        Button StartApp = GameObject.Find("StartApp").GetComponent<Button>();
        StartApp.onClick.AddListener(() => TaskOnClick(StartApp));

        Button Play = ButtonPlay.GetComponent<Button>();
        Play.onClick.AddListener(() => TaskOnClick(Play));

        Button Pause = ButtonPause.GetComponent<Button>();
        Pause.onClick.AddListener(() => TaskOnClick(Pause));

        Button Stop = ButtonStop.GetComponent<Button>();
        Stop.onClick.AddListener(() => TaskOnClick(Stop));

    }
	
	// Update is called once per frame
	void Update () {

        // get the IP Adress from the input
        IPAdress = Input.GetComponent<InputField>().text;
    }

   public void TaskOnClick(Button Btn)
    {
        switch (Btn.name)
        {
            case "StartApp":
                if (IPAdress != "")
                {
                    // change panels
                    Menu.SetActive(true);
                    Init.SetActive(false);
                    sendMessage("<START APPLICATIONS>", IPAdress, Port);               
                }
                else
                {
                    Input.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "IP Adress?";
                    
                }

                break;
            case "Play":
                sendMessage("<START RECORDING>",IPAdress,Port);
                break;
            case "Pause":
                sendMessage("<STOP RECORDING>", IPAdress, Port);
                break;
            case "Stop":
                sendMessage("<STOP RECORDING>", IPAdress, Port);
                break;
            case "Finish":
                sendMessage("<FINISCH>", IPAdress, Port);
                break;
        }
    }

    // the messages can be: "<START APPLICATIONS>" "<START RECORDING>" "<STOP RECORDING>" "<FINISH>"
    private void sendMessage(string message, string IPAddress, string port)
    {
#if NETFX_CORE
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
    }

}
