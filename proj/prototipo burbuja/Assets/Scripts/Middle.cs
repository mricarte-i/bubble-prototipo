using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    public float scaleTime;
    private GameObject camera;
    public Transform[] playersTransform;
    public float maxSize = 20f; //max zoom out size of our camera
    public float minSize; //min zoom out size of our camera
    

    private bool isScaling;
    public float currentSize = 2f;
    private float targetSize;
    private float speedRange; // this is used for lerp function
    private float sizeDelta;  // how long it takes to change the camera size 1 unit;

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

        isScaling = false;
        currentSize = maxSize;
        //speedRange = maxSpeed - minSpeed;
        sizeDelta = (maxSize - minSize) / scaleTime;

    }

    public float offset = 2.0f;
    

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
      float distance = Vector3.Distance(playersTransform[0].position, playersTransform[1].position);
      Vector3 middle = new Vector3(xMiddle, yMiddle, zMiddle);

      if (maxSize > distance)
      {
        GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<PlayerControl>().enabled = false;
      }

      transform.position = middle;
      transform.LookAt(playersTransform[1].position);

    }


}