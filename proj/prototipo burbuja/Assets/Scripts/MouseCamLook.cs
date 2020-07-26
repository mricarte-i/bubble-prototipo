/*
 * author : jiankaiwang
 * description : The script provides you with basic operations
 *               of first personal camera look on mouse moving.
 * platform : Unity
 * date : 2017/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamLook : MonoBehaviour
{
    // lo que esta siguiendo la camara
    public Transform[] playersTransform;
    public void Start()
    {
        //LLamar los tags de los tags, en lugar de hacerlos dos objetos distintos
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        playersTransform = new Transform[Players.Length];
        for(int i = 0; i < Players.Length; i++)
        {
           playersTransform[i] = Players[i].transform;
        }
    }

    public float offset = 2.0f;
    public float minDistance = 7.5f;

    private float  xMin,xMax,yMin,yMax;

    // Update is called once per frame
    void LateUpdate()
    {
       if(playersTransform.Length == 0)
       {
         Debug.Log("There are no players, dumass");
         return;
       }

       xMin = xMax = playersTransform[0].position.x;
       yMin = yMax = playersTransform[0].position.y;
       for(int i = 1; i < playersTransform.Length; i++)
       {
         if(playersTransform[i].position.x < xMin)
         xMin = playersTransform[i].position.x;

         if(playersTransform[i].position.x > xMax)
         xMax = playersTransform[i].position.x;

         if(playersTransform[i].position.y < yMin)
         yMin = playersTransform[i].position.y;

         if(playersTransform[i].position.y > yMax)
         yMax = playersTransform[i].position.y;
       }

        for(int j = 1; j < playersTransform.Length; j++)
       {
         if(playersTransform[j].position.x < xMin)
         xMin = playersTransform[j].position.x;

         if(playersTransform[j].position.x > xMax)
         xMax = playersTransform[j].position.x;

         if(playersTransform[j].position.y < yMin)
         yMin = playersTransform[j].position.y;

         if(playersTransform[j].position.y > yMax)
         yMax = playersTransform[j].position.y;
       }

       float xMiddle = (xMin + xMax) / 2;
       float yMiddle = (yMin + yMax) / 2;
       float distance = xMax - xMin;

       if(distance < minDistance)
          distance = minDistance;

       transform.position = new Vector3(xMiddle, yMiddle, -distance);

    }
}