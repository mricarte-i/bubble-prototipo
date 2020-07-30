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
    public float thetaAVG = 90f;
    public float theta1;
    public float theta2;

    public float distance2P1;
    public float distance2P2;
    public float distance2Mid;

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
    public float maxDistance = 24.5f;

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
      Vector3 middle = new Vector3(xMiddle, yMiddle, zMiddle);


      distance = (playersTransform[1].position - playersTransform[0].position).magnitude;
      float radius = 5*distance/4;
      distance = distance >= minDistance ? distance : minDistance;
      distance = distance < maxDistance ? distance : distance/2;

      //float angle = Mathf.atan(playersTransform[0].position.x - transform.position.x) / (playersTransform[1].position.z - transform.position.z);

      //transform.position = Vector3.Cross((playersTransform[1].position  - playersTransform[0].position),Vector3.up).normalized*distance;
      Vector3 Cross = middle - (playersTransform[0].position - playersTransform[1].position);
      //transform.position = Vector3.Cross(Cross, Vector3.up).normalized * distance;

      //transform.position = Vector3.Cross((middle - playersTransform[0].position), Vector3.up);
      //Quaternion rotation = Quaternion.Euler(0, angle, 0);





      float hyp1 = Mathf.Sqrt(Mathf.Pow(playersTransform[1].position.x, 2) + Mathf.Pow(playersTransform[1].position.z, 2));
      theta1 = Mathf.Deg2Rad*(playersTransform[1].position.z/hyp1);

      float hyp2 = Mathf.Sqrt(Mathf.Pow(playersTransform[0].position.x, 2) + Mathf.Pow(playersTransform[0].position.z, 2));
      theta2 = Mathf.Deg2Rad*(playersTransform[0].position.z/hyp2);

      thetaAVG = theta1 + theta2 + Mathf.PI/2;
      float x = Mathf.Cos(thetaAVG)*radius + middle.x;
      float z = Mathf.Sin(thetaAVG)*radius + middle.z;
      float y = middle.y + offset;

      transform.position = new Vector3(x, y, z);






      //DEBUG GIZMOS, for understanding vectors and stuff:
      Debug.DrawLine(playersTransform[0].position, playersTransform[1].position, Color.green);
      Debug.DrawLine(Vector3.zero, Vector3.up, Color.red);
      Debug.DrawLine(middle,transform.position, Color.blue);
      Debug.DrawLine(middle, Vector3.Cross((middle - playersTransform[0].position), Vector3.up), Color.white);
      Debug.DrawLine(middle, new Vector3(x, y, z), Color.magenta);

      //transform.position += new Vector3(0, offset, 0);



      distance2P1 = (playersTransform[1].position - transform.position).magnitude;
      distance2P2 = (playersTransform[0].position - transform.position).magnitude;
      distance2Mid = (middle - transform.position).magnitude;

      Debug.DrawLine(playersTransform[0].position, transform.position, Color.yellow);
      Debug.DrawLine(playersTransform[1].position, transform.position, Color.grey);
      Debug.DrawLine(middle, transform.position, Color.cyan);

      Quaternion.LookRotation(middle);

      //transform.position = new Vector3(xMiddle, yMiddle, zMiddle);




      transform.LookAt(new Vector3(xMiddle, yMiddle, zMiddle), Vector3.up);

    }
}