/*
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
    public Transform[] playersTransform;
    public Collider[] attacHitBoxes;
    public float speed = 2.0f;

    private float translation;
    private float straffe;
    private Animator animator;
    private bool IsPunching = false;
    float punch = 0f;


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
    void FixedUpdate()
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
        if (Input.GetKey(KeyCode.Q)){
            Debug.Log("LEFT PUNCH");
            IsPunching = true;
            animator.SetTrigger("punch");
            animator.SetBool("IsPunching", IsPunching);
            punch = 0f;
            animator.SetFloat("punch_side", punch);
            Attack(attacHitBoxes[0]);
        }
        if (Input.GetKey(KeyCode.E)){
            Debug.Log("RIGHT PUNCH");
            IsPunching = true;
            animator.SetTrigger("punch");
            animator.SetBool("IsPunching", IsPunching);
            punch = 1f;
            animator.SetFloat("punch_side", punch);
            Attack(attacHitBoxes[1]);
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


        //all animation related:
        transform.LookAt(playersTransform[0]);
        animator.SetFloat("vel_horizontal", horizontal);
        animator.SetFloat("vel_vertical", vertical);


    }


    [SerializeField]
    AnimationClip[] TimedAnimationClips;

    private void Attack(Collider col){
        //StartCoroutine(CheckEventTime(TimedAnimationClips[0]));
        StartCoroutine(CheckEventTime(TimedAnimationClips[0]));
        Collider[] colliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hurtboxes"));
        foreach(Collider c in colliders){
            if(c.transform.tag == "P1"){
                //Debug.Log("self!");
                continue;
            }
            //ESCRIBO el tag del collider que toque
            Debug.Log(c.transform.tag);
        }
    }

    private void OnAnimatorMove(){

    }

    IEnumerator CheckEventTime(AnimationClip animationClip){
        yield return new WaitForSeconds(animationClip.length);
        Debug.Log("AAAAAAAA");
        animator.SetBool("IsPunching", false);

    }

}
