using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class HUDCanvas : MonoBehaviour
{
    public Image Fade;
    public GameObject DashIcon, ThornsIcon, GlaiveIcon;
    public GameObject DashOverlay, ThornsOverlay, GlaiveOverlay;
    public GameObject DashKeyText, ThornsKeyText, GlaiveKeyText;
    public TextMeshProUGUI DashChargesText;

    public static HUDCanvas Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void FadeIn(float timeToFadeIn = 0.2f)
    {
        Fade.DOColor(new Color(0,0,0, 0.0f), timeToFadeIn);
    }

    public void FadeOut(float timeToFadeOut = 0.4f)
    {
        Fade.DOColor(new Color(0,0,0, 1.0f), timeToFadeOut);
    }
}
