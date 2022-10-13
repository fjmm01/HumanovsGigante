using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misil : MonoBehaviour
{
    [SerializeField] float speed;
    
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }
}
