using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] float lifePoints;
    private float timer;
    public float time;
    public GameObject shieldBack;
    public GameObject shieldFront;
    public static bool winLevel;
    public float timerWin = 0;
    public ParticleSystem death;
    void Start()
    {
        lifePoints = 1;
        winLevel = false;
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

        if(collision.CompareTag("Win"))
        {
            winLevel = true;
            timer += Time.deltaTime;
            if(timer > 5)
            {
                int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
                if(nextLevel == 6)
                {
                    SceneManager.LoadScene(0);
                }
                if(PlayerPrefs.GetInt("ReachedLevel",1) < nextLevel)
                {
                    PlayerPrefs.SetInt("ReachedLevel", nextLevel);
                }
                SceneManager.LoadScene(nextLevel);
            }
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
        death.Play();
        Destroy(gameObject, 0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
