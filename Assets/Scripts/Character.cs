using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public bool dead = false;
    public static Character instance;
    //public int points = 0;
    //public Text pointsText;
    //public Text recordText;
    //public GameObject deadText;
    //public GameObject enemySpawner;
    //public GameObject pointSpawner;

    private float record=0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public float MoveSpeed = 10;

    private void Start()
    {
        speed = MoveSpeed;
    }

    private int dir = -1;

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        if (Input.GetKey(KeyCode.A)) dir = 0;
        //if (Input.GetKeyDown(KeyCode.S)) dir = 1;
        else if (Input.GetKey(KeyCode.D)) dir = 2;
        //if (Input.GetKeyDown(KeyCode.W)) dir = 3;
        else dir = -1;

        Move();
    }

    private float speed;

    void Move()
    {
        Vector3 change = Vector3.zero;
        switch (dir)
        {
            case 0:
                change = new Vector3(-speed, 0, 0);
                break;
            case 1:
                change = new Vector3(0, -speed, 0);
                break;
            case 2:
                change = new Vector3(speed, 0, 0);
                break;
            case 3:
                change = new Vector3(0, speed, 0);
                break;
        }

        transform.position = Vector3.Lerp(transform.position, transform.position + change, Time.deltaTime);
    }

    void Die()
    {
        if (dead) return;
        dead = true;
        // deadText.SetActive(true);
        // record = Mathf.Max(record, points);
        // recordText.text = "Record: " + record.ToString();
    }
    

    public void Restart()
    {
        // transform.position = Vector2.zero;
        // points = 0;
        // dead = false;
        // dir = -1;
        // pointsText.text = "Points: 0";
        // deadText.SetActive(false);
        //
        // foreach (Transform o in enemySpawner.transform)
        // {
        //     Destroy(o);
        // }
        // foreach (Transform oo in pointSpawner.transform)
        // {
        //     Destroy(oo);
        // }
    }

private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Die();
            Destroy(col.gameObject);
        } else if (col.CompareTag("Point"))
        {
            Destroy(col.gameObject);
        }
    }
}
