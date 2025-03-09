using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] ragdollBodies;
    private Rigidbody mainRagdollBody; // Main body for launching
    private Animator animator;
    private bool isRagdoll = false;
    public float launchPower = 100f; // Strength of the upward launch
    public float collisionDelay = 3f; // Time before re-enabling collisions

    void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        mainRagdollBody = GetComponentInChildren<Rigidbody>(); // Choose the main ragdoll part

        SetRagdoll(false);
    }

    public void SetRagdoll(bool activate, bool launchUp = false)
    {
        isRagdoll = activate;
        animator.enabled = !activate;

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !activate;
            rb.detectCollisions = activate;
            rb.velocity = Vector3.zero; // Reset velocity
        }

        if (activate && launchUp && mainRagdollBody != null)
        {
            DisableCollisionsTemporarily();
            mainRagdollBody.AddForce(Vector3.up * launchPower, ForceMode.Impulse);
        }
    }

    void DisableCollisionsTemporarily()
    {
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.detectCollisions = false;
        }
        Invoke(nameof(EnableCollisions), collisionDelay);
    }

    void EnableCollisions()
    {
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.detectCollisions = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") && !isRagdoll)
        {
            SetRagdoll(true, true);
        }
    }
}
