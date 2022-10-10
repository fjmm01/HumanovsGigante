using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float maxSprintSpeed = 10;
    [SerializeField] float acceleration = 35;
    [SerializeField] float jumpSpeed = 8;
    [SerializeField] float jumpDuration;
    [SerializeField] bool enableDoubleJump = true;
    [SerializeField] bool wallHitDoubleJumpOverride = true;
    [SerializeField] bool isSprinting;
    [SerializeField] Animator anim;

    //internal checks
    
    float jumpDur;
    bool jumpKeyDown = false;
    bool canVariableJump = false;
    Rigidbody2D rigidbody2D;
    public bool onTheGround;
    public bool leftWallHit;
    public bool rightWallHit;



    void Start() 
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
       
        
    }

    
    void Update()
    {
        onTheGround = isOnGround();
        float horizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", horizontal);
        anim.SetBool("onTheGround", onTheGround);
        if(Input.GetButton("Fire1"))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if (horizontal < -0.1f)
        {
            if(!isSprinting)
            {
                if (rigidbody2D.velocity.x > -this.maxSpeed)
                {
                    rigidbody2D.AddForce(new Vector2(-this.acceleration, 0.0f));
                    
                    
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(-this.maxSpeed, rigidbody2D.velocity.y);
                }
            }
            if(isSprinting)
            {
                if (rigidbody2D.velocity.x > -this.maxSprintSpeed)
                {
                    rigidbody2D.AddForce(new Vector2(-this.acceleration * 1.5f, 0.0f));
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(-this.maxSprintSpeed, rigidbody2D.velocity.y);
                }
            }
            
        }
        else if(horizontal > 0.1f)
        {
            if(!isSprinting)
            {
                if (rigidbody2D.velocity.x < this.maxSpeed)
                {
                    rigidbody2D.AddForce(new Vector2(this.acceleration, 0.0f));
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(this.maxSpeed, rigidbody2D.velocity.y);
                }
            }
            else if(isSprinting)
            {
                if (rigidbody2D.velocity.x < this.maxSprintSpeed)
                {
                    rigidbody2D.AddForce(new Vector2(this.acceleration * 1.5f, 0.0f));
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(this.maxSprintSpeed, rigidbody2D.velocity.y);
                }
            }
            
        }

        
        //float vertical = Input.GetAxis("Vertical");

        
        if (Input.GetButton("Jump") && onTheGround)
        {
            
            if(!jumpKeyDown)
            {
                jumpKeyDown = true;
                if(onTheGround || wallHitDoubleJumpOverride)
                {
                    bool wallHit = false;
                    int wallHitDirection = 0;

                    bool leftWallHit = IsOnWallLeft();
                    bool rightWallHit = IsOnWallRight();

                    if(horizontal != 0)
                    {
                        
                        if(leftWallHit)
                        {
                            wallHit = true;
                            wallHitDirection = 1;
                        }
                        else if(rightWallHit)
                        {
                            wallHit = true;
                            wallHitDirection = -1;
                        }
                    }
                    if(!wallHit)
                    {
                        if(onTheGround)
                        {
                            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, this.jumpSpeed);
                            jumpDur = 0.0f;
                            canVariableJump = true;
                        }
                    }
                    else
                    {
                        rigidbody2D.velocity = new Vector2((this.jumpSpeed / 2) * wallHitDirection, this.jumpSpeed);

                        jumpDur = 0.0f;
                        canVariableJump = true;
                    }

                   
                }
                
            }
            else if(canVariableJump)
            {
                jumpDur += Time.deltaTime;
                if(jumpDur < jumpDuration /1000)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, this.jumpSpeed);
                    
    
                }
                
            }
            else if(jumpKeyDown)
            {

            }
            
        }
        
        
       
    }

    /*private bool isOnGround()
    {
        float lengthToSearch = 0.3f;
        float colliderThreshold = 0.001f;

        Vector2 lineStart = new Vector2(this.transform.position.x, this.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.extents.y - colliderThreshold);
        Vector2 vectorToSearch = new Vector2(this.transform.position.x, lineStart.y - lengthToSearch);

        RaycastHit2D hit = Physics2D.Linecast(lineStart, vectorToSearch);
        return hit;
    }
    */
    private bool isOnGround()
    {
        if(rigidbody2D.velocity.y == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*private bool isOnWallLeft()
    {
        bool retVal = false;
        float lengthToSearch = 0.1f;
        float colliderThreshold = 0.01f;

        Vector2 lineStart = new Vector2(this.transform.position.x - this.GetComponent<SpriteRenderer>().bounds.extents.x - colliderThreshold, this.transform.position.y);
        Vector2 vectorToSearch = new Vector2(lineStart.x - lengthToSearch, this.transform.position.y);

        RaycastHit2D hitLeft = Physics2D.Linecast(lineStart, vectorToSearch);

        retVal = hitLeft;
        if(retVal)
        {
            if(hitLeft.collider.GetComponent<NoSlideJump>())
            {
                retVal = false;
            }
        }
        return retVal;
    }

    private bool isOnWallRight()
    {
        bool retVal = false;
        float lengthToSearch = 0.1f;
        float colliderThreshold = 0.01f;

        Vector2 lineStart = new Vector2(this.transform.position.x + this.GetComponent<SpriteRenderer>().bounds.extents.x + colliderThreshold, this.transform.position.y);
        Vector2 vectorToSearch = new Vector2(lineStart.x + lengthToSearch, this.transform.position.y);

        RaycastHit2D hitRight = Physics2D.Linecast(lineStart, vectorToSearch);

        retVal = hitRight;
        if (retVal)
        {
            if (hitRight.collider.GetComponent<NoSlideJump>())
            {
                retVal = false;
            }
        }
        return retVal;
    }
    */
    private bool IsOnWallLeft()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.2f);
        if(hitLeft)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsOnWallRight()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.2f);
        if (hitRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
