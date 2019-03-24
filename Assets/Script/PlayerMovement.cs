using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movePower = 4f;
    public float jumpPower = 5f;

    Rigidbody2D rigid;
    Animator animator;
    Vector3 movement;
    bool isJumping = false;
    // Start is called before the first frame update

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        if(Input.GetAxisRaw("Horizontal")==0)
        {
            animator.SetBool("ismoving", false);
        }
        else if(Input.GetAxisRaw("Horizontal")!=0)
        {
            animator.SetBool("ismoving", true);
        }
        if (Input.GetButtonDown("Jump") &&!animator.GetBool("isjumping"))
        {
            isJumping = true;
            animator.SetBool("isjumping", true);
            animator.SetTrigger("doJumping");
        }
    }


    void FixedUpdate()
    {
        Move();
        Jump();

    }

    void Move()

    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {

            moveVelocity = Vector3.left;
            
            transform.localScale = new Vector3(-1, 1, 1);
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if (!isJumping)
            return;
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attach : " + other.gameObject.layer);
        if (other.gameObject.layer == 0 && rigid.velocity.y < 0)
            animator.SetBool("isjumping", false);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Detach : " + other.gameObject.layer);
        if (other.gameObject.layer == 0 && rigid.velocity.y < 0)
            animator.SetBool("isjumping", false);
    }
}
