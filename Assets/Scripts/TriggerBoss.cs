using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    [SerializeField] BoxCollider2D trigger;
    [SerializeField] GameObject boss;
    [SerializeField] float timer;
   

    void Start()
    {
        boss.SetActive(false);
        timer = 0;
    }

    
    void Update()
    {
        if(boss.active == true)
        {
            timer += Time.deltaTime;

            if(timer > 60f)
            {
                Health.winLevel = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null)
        {
            return;
        }
        if(collision.CompareTag("Player"))
        {
            Debug.Log("You enter to the trap");
            boss.SetActive(true);
        }
    }

    
}
