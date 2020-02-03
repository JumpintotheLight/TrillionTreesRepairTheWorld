using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attach this script to a GameObject to rotate around the target position.
public class RotateAround : MonoBehaviour
{
    public Vector3 pivot = new Vector3(5.0f, 0.0f, 0.0f);
    public GameObject target;
    public float speed = 1; 

    private void Start()
    {
        
    }

    void Update()
    {
        // Spin the object around the world origin at 20 degrees/second.
        transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}