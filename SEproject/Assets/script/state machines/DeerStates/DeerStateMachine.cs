using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DeerStateMachine : MonoBehaviour
{
    private IDeerState deerState;
    private bool isAlive;

    public Rigidbody rb;

    public bool IsGrounded { get; set; } = true;
    private Animator animator;

    [SerializeField] CinemachineFreeLook mainCam;
    [SerializeField] CinemachineFreeLook deadCam;

    [SerializeField] RagdollController ragdollController;

    [SerializeField] public LayerMask groundLayer;
    public LayerMask GroundLayer => groundLayer;

    [SerializeField] private float jumpCooldown = 1f;  // Cooldown time in seconds
    private float lastJumpTime = 0f;  // Time of the last jump

    void Start()
    {
        mainCam.Priority = 1;
        deadCam.Priority = 0;

        rb = GetComponent<Rigidbody>();
        deerState = new DeerWalk(this);
        if (rb == null)
        {
            Debug.LogError("Rigidbody missing");
        }
        animator = transform.Find("Deer_001").GetComponent<Animator>();
        isAlive = true;
    }

    public void setState(IDeerState d)
    {
        deerState = d;
    }

    void Update()
    {
        if (deerState == null)
        {
            Debug.LogError("deerstate null");
            return;
        }
        animator.SetFloat("speed", rb.velocity.magnitude);
        if (isAlive)
        {
            // Only allow jumping if the cooldown has elapsed
            if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastJumpTime >= jumpCooldown)
            {
                deerState.handleSpace();
                lastJumpTime = Time.time;  // Reset jump timer
            }

            if (Input.GetKey(KeyCode.W)) deerState.handleForward();
            if (Input.GetKey(KeyCode.A)) deerState.handleLeft();
            if (Input.GetKey(KeyCode.D)) deerState.handleRight();
            if (Input.GetKeyDown(KeyCode.LeftShift)) deerState.handleShift();
        }
        deerState.handleGravity();
        deerState.advanceState();

        if (Input.GetKey(KeyCode.P))
        {
            ActivateDeath();
        }
    }

    void ActivateDeath()
    {
        mainCam.Priority = 0;
        deadCam.Priority = 1;
        animator.SetTrigger("Death");
        animator.enabled = false;
    }

    void OnCollisionEnter(Collision c)
    {
        IsGrounded = true;
        if (c.gameObject.tag == "Car")
        {
            ActivateDeath();
            ragdollController.SetRagdoll(true);
            isAlive = false;
        }
    }
}
