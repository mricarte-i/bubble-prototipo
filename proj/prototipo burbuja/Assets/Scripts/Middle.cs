using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    public GameObject camera;
    public Transform[] playersTransform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        playersTransform = new Transform[Players.Length];
        for(int i = 0; i < Players.Length; i++)
        {
           playersTransform[i] = Players[i].transform;
        }
        camera = GameObject.FindGameObjectsWithTag("CameraCube")[0];

    }

    public float offset = 2.0f;
    public float minDistance = 7.5f;
    public float maxDistance = 24.5f;

    private float  xMin,xMax,yMin,yMax;
    private float  zMin,zMax;


    // Update is called once per frame
    void Update()
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


        transform.position =middle;
        transform.LookAt(playersTransform[1].position);


    }
}
