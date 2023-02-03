using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform obj;
    public float speed = 10;
    
    void Update()
    {
        Vector3 objPos = obj.position;
        objPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, objPos, speed*Time.deltaTime);
    }
}
