using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Setup")]
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;

    private GameObject player;
    private GameObject playerCamera;
    private CameraController cameraController;
    
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        player = Instantiate(PlayerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
        playerCamera = Instantiate(CameraPrefab);
        cameraController = playerCamera.GetComponent<CameraController>();

        cameraController.StartFollowing(player);
    }
}
