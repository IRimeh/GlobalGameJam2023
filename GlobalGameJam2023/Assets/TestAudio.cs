using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{

    private void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("Track1");
    }
}
