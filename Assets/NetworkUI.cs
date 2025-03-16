using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;
    [SerializeField] TextMeshProUGUI playerCount;
    [SerializeField] GameObject Buttons;

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            CameraFollow.instance.target = GameObject.FindGameObjectWithTag("Player").transform;
            GameManager.Instance.gameHosted = true;
            Buttons.SetActive(false);
            
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            CameraFollow.instance.target = GameObject.FindGameObjectWithTag("Player").transform;
            GameManager.Instance.gameHosted = true;
            Buttons.SetActive(false);
        });
    }

    private void Update()
    {
        playerCount.text = "Players: " + playersNum.Value.ToString();
        if (!IsServer) return;
        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
        
    }
}
