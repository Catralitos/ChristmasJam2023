using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;

    //public GameObject playerPrefab;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {   
        playerInputManager.onPlayerJoined += AddPlayer;
        //var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
        //var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

public void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        Transform playerTransform = player.transform.parent;
        playerTransform.position = startingPoints[players.Count -1].position;


        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        playerTransform.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        playerTransform.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;

        CarLevelManager.Instance.numCarsSpawned++;
        //playerTransform.GetComponentInChildren
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        //playerInput.gameObject.GetComponent<>
    }
}
