using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Transform rotateToCameraTransform;
    [SerializeField]
    private float acceleration = 5.0f;
    [SerializeField]
    private float deceleration = 5.0f;
    [SerializeField]
    private LayerMask mouseColliderLayermask;

    [SerializeField]
    private Material idleMaterial, walkMaterial;
    [SerializeField]
    private Sprite idleSprite, walkSprite;
    
    private bool hasCamera = false;
    private Camera playerCamera;

    public static Vector3 Position = Vector3.zero;
    public static Vector3 AimDirection = Vector3.zero;
    private bool flipSprite = false;

    public void AssignCamera(Camera camera)
    {
        playerCamera = camera;
        hasCamera = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        RotateTowardsCursor();
        Position = transform.position;
        Animations();
    }

    private void Movement()
    {
        bool isPressingMovementInput = IsPressingMovementInput();
        float lerpSpeed = isPressingMovementInput ? acceleration : deceleration;

        Vector3 dir = GetInputDirection();
        rb.velocity = Vector3.Lerp(rb.velocity, dir * playerStats.MovementSpeed, Time.deltaTime * lerpSpeed);
    }

    private bool IsPressingMovementInput()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }

    private Vector3 GetInputDirection()
    {
        Vector3 direction = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
            direction += new Vector3(0, 0, 1);
        if(Input.GetKey(KeyCode.S))
            direction += new Vector3(0, 0, -1);
        if(Input.GetKey(KeyCode.A))
            direction += new Vector3(-1, 0, 0);
        if(Input.GetKey(KeyCode.D))
            direction += new Vector3(1, 0, 0);

        direction = direction.normalized;
        return direction;
    }

    private void Animations()
    {
        if(IsPressingMovementInput() && GetInputDirection() != Vector3.zero) 
        {
            spriteRenderer.sprite = walkSprite;
            spriteRenderer.material = walkMaterial;

            if(Input.GetKey(KeyCode.A))
            {
                flipSprite = false;
            }
            if(Input.GetKey(KeyCode.D))
            {
                flipSprite = true;
            }

            spriteRenderer.flipX = flipSprite;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
            spriteRenderer.material = idleMaterial;
        }
    }

    private void RotateTowardsCursor()
    {
        if(!hasCamera)
            return;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, mouseColliderLayermask))
        {
            Vector3 pointToRotateTowards = hit.point;
            rotateToCameraTransform.LookAt(pointToRotateTowards, Vector3.up);
            rotateToCameraTransform.transform.rotation = Quaternion.Euler(0, rotateToCameraTransform.transform.rotation.eulerAngles.y, 0);
            AimDirection = rotateToCameraTransform.transform.forward;
        }
    }
}
