using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float lifePoints;
    private float timer;
    public float time;
    void Start()
    {
        lifePoints = 1;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shield"))
        {
            StartCoroutine(Inmunity());
        }

        if(collision.CompareTag("Bullet"))
        {

            GetDamage();
        }
    }

    private void GetDamage()
    {
        lifePoints = lifePoints - 1;
    }

    IEnumerator Inmunity()
    {
        lifePoints = 1000000;
        yield return new WaitForSeconds(time);
        lifePoints = 1;
        yield return null;
    }
    
}
