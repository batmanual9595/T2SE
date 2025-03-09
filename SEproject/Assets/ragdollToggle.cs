using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollToggle : MonoBehaviour
{
    public CapsuleCollider mainCollider;
    public GameObject Rig;
    public Animator animator;
    public Rigidbody rb;

    void Start()
    {
        GetRagdollComponents();
        RagdollOff();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            RagdollOn();
        }
    }

    Collider[] ragdollColliders;
    Rigidbody[] ragdollRigidbodies;

    void GetRagdollComponents()
    {
        ragdollColliders = Rig.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = Rig.GetComponentsInChildren<Rigidbody>();
    }

    void RagdollOn()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }

        mainCollider.enabled = false;
        rb.isKinematic = true;
        animator.enabled = false;
    }

    void RagdollOff()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }

        mainCollider.enabled = true;
        rb.isKinematic = false;
        animator.enabled = true;
    }
}
