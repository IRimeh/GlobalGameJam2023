using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDCanvas : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
