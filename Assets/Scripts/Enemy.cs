using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    [Header("Walking")]
    public float walkSpeed=2;
    public float walkStepDistance=2;
    public float walkStepInterval=0.2f;
    
    [Header("Attacking")]
    public float attackDistanceMin = 2f;
    public float attackDamage = 10;

    private bool dead = false;
    private Animator anim;
    private EnemyAttack _enemyAttack;
    private bool attacking = false;
    
    void Start()
    {
        player = PlayerMovement.Current.gameObject;
        anim = GetComponent<Animator>();
        _enemyAttack = GetComponentInChildren<EnemyAttack>();
    }

    private Vector3 ppos;
    void Update()
    {
        if (dead) return;
        ppos = player.transform.position;
        ppos.y = transform.position.y;
        ppos.z = transform.position.z;

        if (!attacking)
        {
            walkingTimer -= Time.deltaTime;
            if (walkingTimer <= 0) MoveTowardsPlayer();
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * walkSpeed);
            
            if (Vector3.Distance(ppos, transform.position) <= attackDistanceMin)
            {
                //Make attack
                anim.SetTrigger("Attack");
                attacking = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.T) && !attacking) anim.SetTrigger("Attack");
    }

    private Vector3 target;
    private float walkingTimer = 0;
    void MoveTowardsPlayer()
    {
        
        walkingTimer = walkStepInterval;
        target = Vector3.MoveTowards(transform.position, ppos, walkStepDistance) ;
        
        anim.SetTrigger("WalkStep");
    }

    public void StartAttack()
    {
        _enemyAttack.Attack(true);
    }
    
    public void EndAttack()
    {
        attacking = false;
        _enemyAttack.Attack(false);
    }
}
