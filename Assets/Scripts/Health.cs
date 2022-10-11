using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float lifePoints;
    private float timer;
    public float time;
    public GameObject shieldBack;
    public GameObject shieldFront;
    void Start()
    {
        lifePoints = 1;
    }

    
    void Update()
    {
        if(lifePoints <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shield"))
        {
            StartCoroutine(Inmunity());
        }

        if(collision.CompareTag("Enemy"))
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
        shieldBack.SetActive(true);
        shieldFront.SetActive(true);
        lifePoints = 1000000;
        yield return new WaitForSeconds(time);
        lifePoints = 1;
        shieldBack.SetActive(false);
        shieldFront.SetActive(false);
        yield return null;
    }
    
    private void Die()
    {
        Destroy(this.gameObject);
    }
}
