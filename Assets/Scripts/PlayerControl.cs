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

    public GameManager gameManager;

    public GameObject Death;

    public GameObject Teleport;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded || doubleJump)
            {
                jump = true;
                doubleJump = !doubleJump;
                myAnim.SetBool("Jumping", true);

            }
        }
        else
        {
            myAnim.SetBool("Jumping", false);
        }

        if (horizontalMove > 0.1f)
        {
            myAnim.SetBool("Walking", true);
            myRend.flipX = false;
        }
        else if(horizontalMove < -0.1f)
        {
            myAnim.SetBool("Walking", true);
            myRend.flipX = true;
        }
        else
        {
            myAnim.SetBool("Walking", false);
        }
    }

    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if(jump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
        }
        
        if(myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if(myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 0));
        if(hit.collider != null && hit.transform.name == "Ground")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Death")
        {
            gameManager.Restart();
        }

        if(collision.gameObject.name == "Teleport")
        {
            gameManager.Teleport();
        }
    }
}
