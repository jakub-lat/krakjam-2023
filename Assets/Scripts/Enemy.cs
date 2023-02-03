using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 target;
    public float speed=2;
    void Start()
    {
        target = Character.instance.transform.position;

        target += Character.instance.transform.position - transform.position;
        
        transform.right = target - transform.position;
        
        Destroy(gameObject,10);
    }
    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed*Time.deltaTime);
    }
}
