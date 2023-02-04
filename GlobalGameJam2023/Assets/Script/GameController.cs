using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Setup")]
    public GameObject PlayerPrefab;
    public GameObject CameraPrefab;
    public EnemySpawnController EnemyController;
    public Vector3 PlayerSpawnPos = new Vector3(-2, 1.5f, 0);


    private GameObject player;
    private GameObject playerCamera;
    private PlayerController playerController;
    private CameraController cameraController;

    public static GameController Instance;
    private bool settingTimeScale = false;
    private float timeScaleToSetTo = 0.0f;
    private float timeInSeconds = 0.0f;
    private float timeScaleBefore = 0.0f;
    private float timeElapsed = 0.0f;

    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        player = Instantiate(PlayerPrefab, PlayerSpawnPos, Quaternion.identity);
        playerCamera = Instantiate(CameraPrefab);
        playerController = player.GetComponent<PlayerController>();
        cameraController = playerCamera.GetComponent<CameraController>();

        playerController.AssignCamera(playerCamera.GetComponent<Camera>());
        cameraController.StartFollowing(player);

        EnemyController.StartGame();
    }

    private void Update()
    {
        Shader.SetGlobalFloat("_GameTime", Time.time);
        SettingTime();
    }

    private void SettingTime()
    {
        if(settingTimeScale)
        {
            if(timeElapsed < timeInSeconds)
            {
                timeElapsed += Time.unscaledDeltaTime;
                float ratio = timeElapsed / timeInSeconds;
                Time.timeScale = Mathf.Lerp(timeScaleBefore, timeScaleToSetTo, ratio);
            }
            else
            {
                Time.timeScale = timeScaleToSetTo;
                timeElapsed = 0.0f;
                settingTimeScale = false;
            }
        }
    }


    public static void SetTimeScale(float _timeScaleToSetTo, float _timeInSeconds)
    {
        if(Instance.timeScaleToSetTo == _timeScaleToSetTo)
            return;

        Instance.settingTimeScale = true;
        Instance.timeScaleToSetTo = _timeScaleToSetTo;
        Instance.timeInSeconds = _timeInSeconds;
        Instance.timeElapsed = 0.0f;
        Instance.timeScaleBefore = Time.timeScale;
    }
}
