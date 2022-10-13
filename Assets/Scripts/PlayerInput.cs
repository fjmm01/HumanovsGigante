using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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
    private bool canDoubleJump = true;
    public ParticleSystem trail;

    void Start() 
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        trail.Play();


    }

    
    void Update()
    {
        onTheGround = isOnGround();
        float horizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("speedX", horizontal);
        anim.SetBool("onTheGround", onTheGround);
        if(Input.GetButton("Fire1"))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        if(horizontal == 0 && onTheGround)
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
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
        if(onTheGround)
        {
            canDoubleJump = true;
        }
        
        if (Input.GetButton("Jump"))
        {
            
            if(!jumpKeyDown)
            {
                jumpKeyDown = true;
                if(onTheGround ||(canDoubleJump && enableDoubleJump) || wallHitDoubleJumpOverride)
                {
                    bool wallHit = false;
                    int wallHitDirection = 0;

                    leftWallHit = isOnWallLeft();
                    rightWallHit = isOnWallRight();

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
                        if(onTheGround || (canDoubleJump && enableDoubleJump))
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

                    if(!onTheGround && !wallHit)
                    {
                        canDoubleJump = false;
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
            
            
        }
        else
        {
            jumpKeyDown = false;
            canVariableJump = false;
        }
        
        
       
    }

    /*private bool isOnGround()
    {
        float lengthToSearch = 0.1f;
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
    

    private bool isOnWallLeft()
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
    
    /*private bool IsOnWallLeft()
    {
        Vector2 length = new Vector2((transform.position.x - 0.2f), transform.position.y);
        RaycastHit2D hitLeft = Physics2D.Linecast(transform.position, (Vector2)transform.position - length);


        if (hitLeft.collider.GetComponent<NoSlideJump>())
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    */

    /*private bool IsOnWallRight()
    {
        Vector2 length = new Vector2((transform.position.x + 0.2f), transform.position.y);
        RaycastHit2D hitRight = Physics2D.Linecast(transform.position, (Vector2)transform.position + length );
        
        if (hitRight.collider.GetComponent<NoSlideJump>())
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    */

}
