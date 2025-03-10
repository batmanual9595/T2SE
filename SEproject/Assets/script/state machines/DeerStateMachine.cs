using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DeerStateMachine : MonoBehaviour
{
    private IDeerState deerState;
    private bool isAlive;

    public Rigidbody rb;
    private Animator animator;

    public bool IsGrounded { get; set; } = true;

    [SerializeField] CinemachineFreeLook mainCam;
    [SerializeField] CinemachineFreeLook deadCam;

    [SerializeField] RagdollController ragdollController;

    [SerializeField] public LayerMask groundLayer;
    public LayerMask GroundLayer => groundLayer;

    private DeerHealth deerHealth; // Reference to health system

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
        deerHealth = GetComponent<DeerHealth>(); // Get health reference
        isAlive = true;
    }

    public void setState(IDeerState d)
    {
        deerState = d;
    }

    void Update()
    {
        if (!isAlive) return;

        if (deerState == null)
        {
            Debug.LogError("deerstate null");
            return;
        }

        animator.SetFloat("speed", rb.velocity.magnitude);

        if (Input.GetKey(KeyCode.W)) deerState.handleForward();
        if (Input.GetKey(KeyCode.A)) deerState.handleLeft();
        if (Input.GetKey(KeyCode.D)) deerState.handleRight();
        if (Input.GetKeyDown(KeyCode.Space)) deerState.handleSpace();
        if (Input.GetKeyDown(KeyCode.LeftShift)) deerState.handleShift();

        deerState.handleGravity();
        deerState.advanceState();

        if (Input.GetKey(KeyCode.P))
        {
            ActivateDeath();
        }

        // Detect if deer falls off the edge
        if (transform.position.y < -10)
        {
            deerHealth.FallOffEdge();
        }
    }

    void ActivateDeath()
    {
        if (!isAlive) return;

        isAlive = false;
        mainCam.Priority = 0;
        deadCam.Priority = 1;

        animator.SetTrigger("Death");
        animator.enabled = false;

        // Enable ragdoll
        ragdollController.SetRagdoll(true);
    }


    void OnCollisionEnter(Collision c)
    {
        IsGrounded = true;
        if (c.gameObject.tag == "Car")
        {
            ActivateDeath();
            ragdollController.SetRagdoll(true);
            GetComponent<DeerHealth>().TakeDamage();
        }
    }

    public void EnableDeer()
    {
        isAlive = true;
        animator.enabled = true;
        ragdollController.SetRagdoll(false);

        mainCam.Priority = 1;
        deadCam.Priority = 0;
    }

    public void DisableDeer()
    {
        isAlive = false;
        rb.velocity = Vector3.zero;
    }
}
