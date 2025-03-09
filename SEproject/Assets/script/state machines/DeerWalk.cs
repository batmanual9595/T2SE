using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerWalk : IDeerState
{
    protected DeerStateMachine deer;
    private Rigidbody rb;

    private float speed = 2f;
    private float rotationSpeed = 100f;
    private float jumpForce = 5f;
    private float maxSpeed = 2f;
    
    public DeerWalk(DeerStateMachine deer){
        this.deer = deer;
        rb = deer.rb;
    }

    public void handleGravity(){
        if (!Input.anyKey){
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }
    public void handleForward(){
        rb.velocity = new Vector3(deer.transform.forward.x * speed, rb.velocity.y, deer.transform.forward.z * speed);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    public void handleLeft(){
        deer.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }
    public void handleRight(){
        deer.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    public void handleSpace(){
        // deer.setState();
    }
    public void handleShift(){
        deer.setState(new DeerSprint(deer));
    }
    public void advanceState(){

    }
}
