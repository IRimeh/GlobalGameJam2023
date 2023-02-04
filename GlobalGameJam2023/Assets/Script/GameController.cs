using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Setup")]
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;
<<<<<<< HEAD
    public EnemySpawnController EnemyController;
=======
    public Vector3 PlayerSpawnPos = new Vector3(-2, 1.5f, 0);
>>>>>>> b5bb58de2fa713e0bcc818f19e4749284c893602

    private GameObject player;
    private GameObject playerCamera;
    private PlayerController playerController;
    private CameraController cameraController;
    
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        playerCamera = Instantiate(CameraPrefab);
        playerController = player.GetComponent<PlayerController>();
        cameraController = playerCamera.GetComponent<CameraController>();

        playerController.AssignCamera(playerCamera.GetComponent<Camera>());
        cameraController.StartFollowing(player);

        EnemyController.StartGame();
    }
}
