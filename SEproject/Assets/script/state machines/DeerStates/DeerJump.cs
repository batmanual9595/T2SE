using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerJump : IDeerState
{
    protected DeerStateMachine deer;
    private Rigidbody rb;
    public LayerMask groundLayer = LayerMask.GetMask("groundLayer");
    private int raycastCooldown;
    private Animator animator;

    private float speed;
    private float jumpForce = 60f;
    private float maxSpeed = 6f;
    private bool jumped=false;
    
    public DeerJump(DeerStateMachine deer, float speed){
        this.deer = deer;
        rb = deer.rb;
        this.speed = speed;
        raycastCooldown = 30;
        animator = deer.transform.Find("Deer_001").GetComponent<Animator>();
    }

    public void handleGravity(){
        if (!jumped){
            rb.AddForce(deer.transform.up*jumpForce, ForceMode.Impulse);
            rb.AddForce(deer.transform.forward*speed, ForceMode.Impulse);
            jumped = true;
        }
    }
    public void handleForward(){
        rb.AddForce(deer.transform.forward*100, ForceMode.Impulse);
        rb.velocity = new Vector3(deer.transform.forward.x * speed/2, rb.velocity.y, deer.transform.forward.z * speed/2);

        if (rb.velocity.magnitude > 2*maxSpeed)
        {
            rb.velocity = rb.velocity.normalized *maxSpeed;
        }
    }
    public void handleLeft(){
        // can't turn
    }
    public void handleRight(){
        //can't turn
    }
    public void handleSpace(){
        //can't jump again
    }
    public void handleShift(){
        //no need to sprint when already sprinting
    }
    public void advanceState(){
        RaycastHit hit;
        if (Physics.Raycast(deer.transform.position, Vector3.down, out hit, 1.2f, groundLayer) && raycastCooldown <= 0)
        {
            if (speed == 2f) deer.setState(new DeerWalk(deer));
            if (speed == 6f) deer.setState(new DeerSprint(deer));
            Debug.DrawRay(deer.transform.position, Vector3.down * 1.2f, Color.yellow);
            animator.SetBool("airborne", false);
        }
        else
        {
            Debug.Log("Not on the ground");
            Debug.DrawRay(deer.transform.position, Vector3.down * 1.2f, Color.red);
            animator.SetBool("airborne", true);
        }
        raycastCooldown--;
    }
}