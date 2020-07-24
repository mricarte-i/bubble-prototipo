﻿/*
 * author : jiankaiwang
 * description : The script provides you with basic operations of first personal control.
 * platform : Unity
 * date : 2017/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{

    public float speed = 10.0f;
    private float translation;
    private float straffe;

    // Use this for initialization
    void Start()
    {
        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       float vertical = 0f;
        float horizontal = 0f;
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        if (Input.GetKey(KeyCode.A)){
            horizontal = -1;
        }
        if (Input.GetKey(KeyCode.D)){
            horizontal = 1;
        }
        if (Input.GetKey(KeyCode.W)){
            vertical = 1;
        }
        if (Input.GetKey(KeyCode.S)){
            vertical = -1;
        }
        translation = vertical * speed * Time.deltaTime;
        straffe = horizontal * speed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(0, speed/2.0f * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(0, -speed/2.0f * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = (Cursor.lockState == CursorLockMode.None) ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
