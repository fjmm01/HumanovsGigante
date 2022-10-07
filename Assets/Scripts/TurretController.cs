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
    Vector2 targetPosition;
    Vector2 direction;
    

    void Start()
    {
        
    }

    void Update()
    {
        targetPosition = player.transform.position;
        direction = targetPosition - (Vector2)transform.position;
        SetTarget();
        
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
