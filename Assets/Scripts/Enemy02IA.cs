using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02IA : MonoBehaviour
{
    [SerializeField] float attackTime;
    [SerializeField] float nexTimeAttack;
    [SerializeField] Animator anim;
    [SerializeField] GameObject misil;
    [SerializeField] TriggerBoss trigger;
   

    public Vector3 center;
    public Vector2 spawnBounds;
    public Vector3 offset;
    void Start()
    {
        transform.position = new Vector3(0, -5.3f, 0);
        center = new Vector3(transform.position.x, transform.position.y + transform.position.z);
        InvokeRepeating("SpawnMisil", nexTimeAttack, attackTime);
    }

    
    void Update()
    {
        if(trigger.timer >= 10)
        {
            anim.SetTrigger("Death");
            CancelInvoke();
        }
        
    }

    public void SpawnMisil()
    {
        Vector3 pos1 = center + new Vector3(Random.Range(-spawnBounds.x / 2, spawnBounds.x / 2), spawnBounds.y, transform.position.z);
        Vector3 pos2 = center + new Vector3(-spawnBounds.x, Random.Range(-spawnBounds.y / 2, spawnBounds.y / 2), transform.position.z);

        int indexPosition = Random.Range(1, 3);
        if(indexPosition == 1)
        {
            Instantiate(misil, pos2, Quaternion.identity);
        }
        else if(indexPosition == 2)
        {
            Instantiate(misil, pos1, Quaternion.Euler(0, 0, -90));
        }
    }

   


}
