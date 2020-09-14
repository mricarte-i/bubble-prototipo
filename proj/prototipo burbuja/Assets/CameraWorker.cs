using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWorker : MonoBehaviour
{
    public Transform[] playersTransform;
    public Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        playersTransform = new Transform[Players.Length];
        for(int i = 0; i < Players.Length; i++)
        {
           playersTransform[i] = Players[i].transform;
        }
        initPos = transform.position;
    }

    public float offset = 2.0f;


    // Update is called once per frame
    void LateUpdate()
    {

        if(playersTransform.Length == 0)
        {
            Debug.Log("There are no players, dumass");
            return;
        }

        float distance = (playersTransform[1].position - playersTransform[0].position).magnitude;
        Vector3 newPos = new Vector3(distance + offset, initPos.y, initPos.z);


        transform.position = transform.parent.TransformPoint(newPos);


    }
}
