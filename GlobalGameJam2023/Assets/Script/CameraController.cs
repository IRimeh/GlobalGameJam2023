using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 offsetFromPlayer;
    [SerializeField]
    private float followSpeed = 10.0f;
    [SerializeField]
    private Vector3 minBounds = new Vector3(-30, -30, -30);
    [SerializeField]
    private Vector3 maxBounds = new Vector3(30, 30, 30);

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
    void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if(followingPlayer)
        {
            Vector3 playerPos = playerRef.transform.position;
            playerPos = Vector3.Min(playerPos, maxBounds);
            playerPos = Vector3.Max(playerPos, minBounds);

            transform.position = Vector3.Lerp(transform.position, playerPos + offsetFromPlayer, Time.deltaTime * followSpeed);
        }
    }
}
