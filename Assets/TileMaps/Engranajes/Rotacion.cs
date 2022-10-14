using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    private float rotZ;
    [SerializeField] float rotationSpeed;
    [SerializeField] bool clockWiseRotation;
    

    
    void Update()
    {
        if(clockWiseRotation == false)
        { 
            rotZ += Time.deltaTime * rotationSpeed;
        }
        else
        { 
            rotZ += -Time.deltaTime * rotationSpeed;
        }
        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
