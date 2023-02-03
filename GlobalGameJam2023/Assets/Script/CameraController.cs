using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 offsetFromPlayer;
    [SerializeField]
    private float followSpeed = 10.0f;

    private GameObject playerRef;
    private bool followingPlayer = false;
    public bool FollowingPlayer
    {
        get { return followingPlayer && playerRef != null; }
    }

    public void StartFollowing(GameObject _playerRef)
    {
        playerRef = _playerRef;
        followingPlayer = true;
        transform.position = playerRef.transform.position + offsetFromPlayer;
    }

    public void StopFollowing()
    {
        playerRef = null;
        followingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if(followingPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, playerRef.transform.position + offsetFromPlayer, Time.deltaTime * followSpeed);
        }
    }
}
