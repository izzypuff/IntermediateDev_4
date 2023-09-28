using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //vars for horiztontal input and movement
    float horizontalMove;
    public float speed = 2f;

    //ref to this object's rigidbody
    Rigidbody2D myBody;

    //to see if we touched the ground
    bool grounded = false;

    //how far we should check for the ground
    public float castDist = 1f;

    //vars for jumping physics
    public float jumpPower = 2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;

    //for jumping input
    bool jump = false;

    //ref to this object's animator
    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        //set my component references
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //get any horizontal inputs (A+D, arrow keys, joystick etc)
        horizontalMove = Input.GetAxis("Horizontal");
        Debug.Log(horizontalMove);

        //get any "jump" input (spacebar) and check if the bool grounded is true
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //play the jump animation
            myAnim.SetBool("jumping", true);
            //set the jump bool to true
            jump = true;
        }

        //if we're moving horizontally
        if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
            //turn on the running animation
            myAnim.SetBool("running", true);
        }
        else
        {
            //if not, turn off the running animatino
            myAnim.SetBool("running", false);
        }
    }

    //fixed update runs at a fixed rate
    //it is the event where Unity runs physics calculations
    void FixedUpdate()
    {
        //set how fast we should move on the x axis this frame
        float moveSpeed = horizontalMove * speed;

        //if we should jump
        if (jump)
        {
            //jump!
            myBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            //say we should no longer jump
            jump = false;
        }

        //if we're going up
        if (myBody.velocity.y >= 0)
        {
            //have a lower gravity
            myBody.gravityScale = gravityScale;
        }
        else if (myBody.velocity.y < 0)
        {
            //if we're going down have a higher gravity
            myBody.gravityScale = gravityFall;
        }

        //cast a line beneath my player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        //if that raycast hits something and that thing is named "Ground"
        //if(hit.collider != null && hit.transform.tag == "Ground")
        if (hit.collider != null && hit.transform.name == "Ground")
        {
            //turn off the jump animation
            myAnim.SetBool("jumping", false);
            //and set grounded to true (so we can jump later)
            grounded = true;
        }
        else
        {
            //otherwise, we are in the air
            grounded = false;
        }

        //actually move my players
        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
    }
}
