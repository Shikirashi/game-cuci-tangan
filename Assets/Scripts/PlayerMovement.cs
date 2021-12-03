﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public CharacterController controller;
    public float speed = 12f;
    public float sprintSpeed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public float jumpHeight = 3f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Update(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool shift = Input.GetKey(KeyCode.LeftShift);

        Vector3 move = transform.right * x + transform.forward * z;

        if(shift){
            controller.Move(move * (speed * sprintSpeed) * Time.deltaTime);
        }else{
            controller.Move(move * speed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
