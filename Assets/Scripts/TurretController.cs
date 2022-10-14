using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] PlayerInput player;
    [SerializeField] float fireRate;
    [SerializeField] float nextTimetoFire;
    [SerializeField] float attackRange;
    [SerializeField] bool isDetected;
    [SerializeField] GameObject gunPivot;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootingPoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] int waypointIndex = 0;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    private float distToPoint;
    Vector2 targetPosition;
    Vector2 direction;
    [SerializeField] bool movementTurret;
    [SerializeField] bool torretaFija;
    public GameObject shootingDirection;

    void Start()
    {
        if (movementTurret)
        {
            transform.position = waypoints[waypointIndex].transform.position;
        }
        else
        {
            transform.position = transform.position;
        }
    }

    void Update()
    {
        if(movementTurret)
        {
            TurretMovement();
        }
        
        if(torretaFija)
        {
            targetPosition = player.transform.position;
            direction = targetPosition - (Vector2)transform.position;
            SetTarget();
        }
        else
        {
            direction = shootingDirection.transform.position - transform.position;
            if (Time.time > nextTimetoFire)
            {
                nextTimetoFire = Time.time + 1 / fireRate;
                Shoot();
            }
            
        }
        
        
        
    }


    private void TurretMovement()
    {
        
            distToPoint = Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position);
            
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);

            if (distToPoint < 0.5f)
            {
                ChooseOtherWaypoint();
            Debug.Log(waypoints.Length);
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

    private void SetTarget()
    {
        
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position,direction, attackRange);

        if(rayInfo)
        {
            if(rayInfo.collider.gameObject.tag == "Player")
            {
                if(isDetected == false)
                {
                    isDetected = true;
                }
                
            }
            else
            {
                if(isDetected == true)
                {
                    isDetected = false;
                }
            }
        }
        if(isDetected)
        {
            gunPivot.transform.up = direction;
            if(Time.time > nextTimetoFire)
            {
                nextTimetoFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

    }

    private void Shoot()
    {
        GameObject bulletInstancer = Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
        bulletInstancer.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
    }
}
