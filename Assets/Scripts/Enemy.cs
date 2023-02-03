using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float walkSpeed=2;
    public float walkStepDistance=2;
    public float walkStepInterval=0.2f;

    private bool dead = false;
    private Animator anim;
    
    void Start()
    {
        player = Character.instance.gameObject;
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (dead) return;

        walkingTimer -= Time.deltaTime;
        if(walkingTimer<=0) MoveTowardsPlayer();
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime*walkSpeed);
    }

    private Vector3 target;
    private float walkingTimer = 0;
    void MoveTowardsPlayer()
    {
        Vector3 pos = player.transform.position;
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        walkingTimer = walkStepInterval;
        target = Vector3.MoveTowards(transform.position, pos, walkStepDistance) ;
        
        anim.SetTrigger("WalkStep");
    }
}
