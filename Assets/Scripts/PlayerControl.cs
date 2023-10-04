using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    //horizontal movement
    float horizontalMove;
    public float speed = 3f;

    //jumping
    bool grounded = false;
    public float castDist = 0.2f;
    public float jumpLimit = 2f;
    public float gravityScale = 2f;
    public float gravityFall = 40f;
    bool jump = false;
    bool doubleJump = false;

    //rigid body
    Rigidbody2D myBody;

    //animator
    Animator myAnim;

    //sprite renderer
    SpriteRenderer myRend;

    //access game manager
    public GameManager gameManager;

    //access death object
    public GameObject Death;

    //access teleport object
    public GameObject Teleport;

    // Start is called before the first frame update
    void Start()
    {
        //gets components
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement (A&D, left&right)
        horizontalMove = Input.GetAxis("Horizontal");

        //if jump button (space)
        if (Input.GetButtonDown("Jump"))
        {
            //if grounded or double jump is true
            if (grounded || doubleJump)
            {
                //jump is true
                jump = true;
                //double jump is not true
                doubleJump = !doubleJump;
                //animation jumping boolean is true
                myAnim.SetBool("Jumping", true);
            }
        }
        else
        {
            //if not jumping, then jumping animation boolean is false
            myAnim.SetBool("Jumping", false);
        }

        //if horizontal movement is happening to the right
        if (horizontalMove > 0.1f)
        {
            //walking animation boolean is true
            myAnim.SetBool("Walking", true);
            //do not flip the x to face right
            myRend.flipX = false;
        }
        //if horizontal movement is happening to the left 
        else if(horizontalMove < -0.1f)
        {
            //walking animation boolean is true
            myAnim.SetBool("Walking", true);
            //flip the x to face left
            myRend.flipX = true;
        }
        //if not moving
        else
        {
            //walking animation boolean is false
            myAnim.SetBool("Walking", false);
        }
    }

    void FixedUpdate()
    {
        //set movespeed to horizontal move float times speed
        float moveSpeed = horizontalMove * speed;

        //if jumping
        if(jump)
        {
            //add force
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            //stop jumping
            jump = false;
        }
        //is character is jumping
        if(myBody.velocity.y >= 0)
        {
            //put the current grav scale to set grav scale
            myBody.gravityScale = gravityScale;
        }
        //if character is falling
        else if(myBody.velocity.y < 0)
        {
            //put the current grav scale to fall grav scale
            myBody.gravityScale = gravityFall;
        }

        //create raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 0));
        //if touching ground obj
        if(hit.collider != null && hit.transform.name == "Ground")
        {
            //grounded is true
            grounded = true;
        }
        else
        {
            //if not touching ground obj, grounded is false
            grounded = false;
        }
        //set velocity to set velocity
        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
    }

    //if colliding
    void OnCollisionEnter2D(Collision2D collision)
    {
        //with death obj
        if(collision.gameObject.name == "Death")
        {
            //trigger game manager restart function
            gameManager.Restart();
        }

        //with teleport obj
        if(collision.gameObject.name == "Teleport")
        {
            //trigger game manager teleport function
            gameManager.Teleport();
        }
    }
}
