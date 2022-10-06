using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private bool isOnGround()
    {
        float lengthToSearch = 0.1f;
        float colliderThreshold = 0.001f;

        Vector2 lineStart = new Vector2(this.transform.position.x, this.transform.position.y - this.GetComponentInChildren<SpriteRenderer>().bounds.extents.y - colliderThreshold);
        Vector2 vectorToSearch = new Vector2(this.transform.position.x, lineStart.y - lengthToSearch);

        RaycastHit2D hit = Physics2D.Linecast(lineStart, vectorToSearch);
        return hit;
    }

    private bool isOnWallLeft()
    {
        float lengthToSearch = 0.1f;
        float collidetThreshold = 0.01f;

        Vector2 lineStart = 
    }
}
