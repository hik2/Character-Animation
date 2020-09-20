using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// 8-directional movement
public class EveController : MonoBehaviour
{
    public float velocity = 5;
    public float turnSpeed = 10;

    Vector2 input;
    float angle;

    Quaternion targetRotation;
    Transform cam;

    static Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Press space bar to make character jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isJumping");
        }
        GetInput();
        if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1){
            return;
        }
        CalculateDirection();
        Rotate();
        Move();
    }


    // Input based on keyboard input
    void GetInput()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        anim.SetFloat("BlendX", input.x);
        anim.SetFloat("BlendY", input.y);
    }

    // Direction relative to camera's rotation
    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    // Rotate towards calculated angle
    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    // Player moves along its own forward axis
    void Move()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }
}
