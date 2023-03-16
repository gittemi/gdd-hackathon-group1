//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float pushingSpeedScale = 0.5f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool pushing = false;

    public bool isOnLadder = false;
    public float ladderSpeed = 2f;

    public Animator animator;

    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SublevelBox")
        {
            Debug.Log("Pushing");
            pushing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SublevelBox")
        {
            Debug.Log("No Longer Pushing");
            pushing = false;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameManager>() != null && FindObjectOfType<GameManager>().isOver)
        {
            GetComponent<Rigidbody2D>().bodyType = UnityEngine.RigidbodyType2D.Static;
            return;
        }
            
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            bool toPlay = !jump;
            jump = true;
            animator.SetBool("IsJumping", true);

            if (toPlay)
                FindObjectOfType<AudioManager>().Play("JumpSound");
        }

        verticalMove = Input.GetAxisRaw("Vertical") * ladderSpeed;
        if (!isOnLadder)
            verticalMove = 0;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    private void FixedUpdate()
    {
        if (pushing)
            horizontalMove *= pushingSpeedScale;

        // Move our characeter
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        GetComponent<Transform>().position += new Vector3(0f, verticalMove, 0f);
    }
}
