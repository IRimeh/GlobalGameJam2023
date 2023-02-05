using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Image fade;
    public void Play()
    {
        fade.DOColor(new Color(0,0,0, 1.0f), 0.1f);
        SceneLoader.Instance.LoadSceneAfterDelay("SampleScene", 0.15f);
    }
}
