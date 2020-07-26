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
    private float  zMin,zMax;

    void LateUpdate()
    {
       if(playersTransform.Length == 0)
       {
         Debug.Log("There are no players, dumass");
         return;
       }

       xMin = xMax = playersTransform[0].position.x;
       yMin = yMax = playersTransform[0].position.y;
       zMin = zMax = playersTransform[0].position.z;

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

         if(playersTransform[i].position.z < zMin)
         zMin = playersTransform[i].position.z;

         if(playersTransform[i].position.z > zMax)
         zMax = playersTransform[i].position.z;
       }

      float xMiddle = (xMin + xMax) / 2;
      float yMiddle = (yMin + yMax) / 2;
      float zMiddle = (zMin + zMax) / 2;
      float distance;

      distance = (playersTransform[1].position  - playersTransform[0].position).magnitude;
      distance = distance >= minDistance ? distance : minDistance;
      transform.position = Vector3.Cross((playersTransform[1].position  - playersTransform[0].position),Vector3.up).normalized*distance;
      transform.position += new Vector3(0, offset, 0);
      transform.LookAt(new Vector3(xMiddle, yMiddle, zMiddle));
      //transform.position = new Vector3(xMiddle, yMiddle, zMiddle);


    }
}