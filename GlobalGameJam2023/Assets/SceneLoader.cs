using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAfterDelay(string sceneName, float delay = 0.1f)
    {
        StartCoroutine(DelayedLoad());

        IEnumerator DelayedLoad()
        {
            yield return new WaitForSeconds(delay);
            LoadScene(sceneName);
        }
    }
}
