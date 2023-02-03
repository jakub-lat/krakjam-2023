using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public GameObject skeleton;
    [Header("Walking")]
    public float walkSpeed=2;
    public float walkStepDistance=2;
    public float walkStepInterval=0.2f;
    
    [Header("Attacking")]
    public float attackDistanceMin = 2f;
    public float attackDamage = 10;

    [Header("GotHit")] 
    public float knockBackDistance = 2f;
    public float knockedSpeed = 2f;

    private bool dead = false;
    private Animator anim;
    private EnemyAttack _enemyAttack;
    public bool attacking = false;
    public bool gotHit = false;
    
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

        if (!attacking && !gotHit)
        {
            if (transform.position.x < ppos.x) skeleton.transform.localScale = new Vector3(-1, 1, 1);
            else skeleton.transform.localScale = new Vector3(1, 1, 1);
            
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

        if (gotHit)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * knockedSpeed);
        }
        

        if(Input.GetKeyDown(KeyCode.T) && !attacking) anim.SetTrigger("Attack");
        if(Input.GetKeyDown(KeyCode.H)) GotHit();
    }

    private Vector3 target;
    private float walkingTimer = 0;
    void MoveTowardsPlayer()
    {
        walkingTimer = walkStepInterval;
        target = Vector3.MoveTowards(transform.position, ppos, walkStepDistance) ;
        
        anim.SetTrigger("WalkStep");
    }

    public void GotHit()
    {
        Debug.Log("Enemy hit!");
        
        target = transform.position + ((transform.position - ppos).normalized * knockBackDistance);
        anim.SetTrigger("GotHit");
        gotHit = true;
        attacking = false;
    }

    public void EndGotHit()
    {
        gotHit = false;
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
