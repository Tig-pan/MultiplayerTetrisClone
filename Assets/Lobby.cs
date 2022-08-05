using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Net;
using System.Net.Sockets;

public class Lobby : MonoBehaviour
{
    public GameController manager;
    [Space(10)]
    public Text modeOne;
    public Text modeTwo;
    public Toggle hardMode;
    public Button startButton;
    public Text connectionTextOne;
    public Text connectionTextTwo;
    public Text dominanceTextOne;
    public Text dominanceTextTwo;
    public GameObject battleTypeText;
    public GameObject modificationsText;
    public GameObject multiplierText;

    private Mode mode;

    public void StartLobby(Mode newMode, bool forceButton)
    {
        mode = newMode;
        startButton.gameObject.SetActive(false);
        dominanceTextOne.gameObject.SetActive(false);

        if (mode == Mode.Host)
        {
            battleTypeText.SetActive(true);
            modificationsText.SetActive(true);
            multiplierText.SetActive(true);

            hardMode.gameObject.SetActive(true);
            if (forceButton)
            {
                startButton.gameObject.SetActive(true);
            }
            modeOne.text = "Host";
            modeTwo.text = "Host";

            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    connectionTextOne.text = "Port: " + manager.PortUser + "  |  LAN IP: " + ip.ToString();
                    connectionTextTwo.text = "Port: " + manager.PortUser + "  |  LAN IP: " + ip.ToString();
                    break;
                }
            }
        }
        else
        {
            dominanceTextOne.gameObject.SetActive(false);

            battleTypeText.SetActive(false);
            modificationsText.SetActive(false);
            multiplierText.SetActive(false);

            hardMode.gameObject.SetActive(false);

            modeOne.text = "Client";
            modeTwo.text = "Client";

            connectionTextOne.text = "Waiting for Host to start.";
            connectionTextTwo.text = "Waiting for Host to start.";
        }
    }

    public void UpdateLobby(int connectionCount)
    {
        if (connectionCount == 1)
        {
            startButton.gameObject.SetActive(true);
        }
        if (connectionCount > 1)
        {
            dominanceTextOne.gameObject.SetActive(true);

            dominanceTextOne.text = "Dominance: " + (connectionCount + 1) + " Players";
            dominanceTextTwo.text = "Dominance: " + (connectionCount + 1) + " Players";

            battleTypeText.SetActive(false);
        }
        else
        {
            battleTypeText.SetActive(true);
        }
    }

    public void StartGame()
    {
        StartCoroutine(manager.SendGame(hardMode.isOn));
    }
}

