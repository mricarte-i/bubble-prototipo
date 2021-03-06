/*
 * author : jiankaiwang
 * description : The script provides you with basic operations of first personal control.
 * platform : Unity
 * date : 2017/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public Transform[] playersTransform;
    public float speed = 2.0f;
    private float translation;
    private float straffe;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        playersTransform = new Transform[Players.Length];
        for(int i = 0; i < Players.Length; i++)
        {
            if(Players[i] != this){
                playersTransform[i] = Players[i].transform;
            }
        }
        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = 0f;
        float horizontal = 0f;
        float punch = 0f;
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        if (Input.GetKey(KeyCode.LeftArrow)){
            horizontal = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            horizontal = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            vertical = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            vertical = -1;
        }
        if (Input.GetKey(KeyCode.N)){
            punch = -1;
            Debug.Log("LEFT PUNCH");
        }
        if (Input.GetKey(KeyCode.M)){
            punch = 1;
            Debug.Log("RIGHT PUNCH");
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

        //Animation related stuff:
        transform.LookAt(playersTransform[1]);
        animator.SetFloat("vel_horizontal", horizontal);
        animator.SetFloat("vel_vertical", vertical);
        animator.SetFloat("punching", punch);
    }
}
