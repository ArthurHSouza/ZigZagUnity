using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreation : MonoBehaviour
{
    public GameObject pathPrefab;
    public float offset = -1f;
    public Vector3 lastPosition;
    private int extraPathCount = 0;

    public void StartPathCreation(float rate = 0.5f)
    {
        InvokeRepeating("CreatePath", 1f, rate);
    }

    public void CreatePath()
    {
       
        Vector3 spawnPosition;

        if(Random.Range(0,100) < 50)
        {
            spawnPosition = new Vector3(lastPosition.x - offset, lastPosition.y, lastPosition.z);
        }
        else
        {
            spawnPosition = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z + offset);
        }

        GameObject pathPart = Instantiate(pathPrefab, spawnPosition, Quaternion.identity);
        lastPosition = pathPart.transform.position;
        extraPathCount++;

        if(extraPathCount % 6 == 0) 
        {
            pathPart.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
