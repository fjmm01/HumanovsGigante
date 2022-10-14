using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy016AI : MonoBehaviour
{
    [SerializeField] float attackTime;
    [SerializeField] float attackRate;
    [SerializeField] GameObject target;
    [SerializeField] int waypointIndex = 0;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    private float distToPoint;
    [SerializeField] BoxCollider2D collider;
    [SerializeField] Animator anim;
    
    void Start()
    {
        collider.enabled = false;
        transform.position = waypoints[waypointIndex].transform.position;
    }

    
    void Update()
    {
        anim.SetFloat("speed", 1);
        Movement();
        if(Time.time > attackTime)
            {
                attackTime = Time.time + 1 / attackRate;
                Attack();
            }
        
    }

    private void Movement()
    {
        distToPoint = Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);

        if(distToPoint  < 0.5f)
        {
            ChooseOtherWaypoint();
        }
        
    }

    private void ChooseOtherWaypoint()
    {
        if (waypointIndex == waypoints.Length - 1)
        {
            waypointIndex = waypointIndex;
        }
        else
        {
            waypointIndex++;
        }
        
        
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Hit()
    {
        collider.enabled = true;
    }

    public void End()
    {
        collider.enabled = false;
    }
    
}
