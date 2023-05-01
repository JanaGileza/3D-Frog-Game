using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform orientation;
    
    [Header("Movement settings")]
    [SerializeField] float moveSpeed;
    
    [Header("Jump settings")]
    [SerializeField] float jumpForce;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    
    [Header("Ground check")]
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float maxJumpFrameBufferInSeconds = 0.45f;
    bool triedToJump;
    float lastTimeTriedToJump;

    Rigidbody _rb;
    Collider _collider;
    float _horizontalInput;
    float _verticalInput;

    public bool IsGrounded
    {
        get
        {
            return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        }
    }

    // Awake is called when the script instance is being loaded (only once, before Start())
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    void Start()
    {
        lastTimeTriedToJump = 10;
    }

    void Update()
    {
        ListenForInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
        if (triedToJump && IsGrounded)
        {
            Jump();
            triedToJump = false;
        }
    }

    void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.y);

        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void ListenForInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey))
        {
            lastTimeTriedToJump = Time.time;
            triedToJump = true;
        }
        else if (Time.time > lastTimeTriedToJump + maxJumpFrameBufferInSeconds)
            triedToJump = false;
    }

    void MovePlayer()
    {
        Vector3 movementDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        movementDirection.Normalize();

        Vector3 targetVelocity = movementDirection * moveSpeed;
        _rb.velocity = new Vector3(targetVelocity.x, _rb.velocity.y, targetVelocity.z);
    }
}
