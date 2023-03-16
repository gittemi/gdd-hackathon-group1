//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float pushingSpeedScale = 0.5f;

    float horizontalMove = 0f;
    bool jump = false;
    bool pushing = false;

    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SublevelBox")
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
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (pushing)
            horizontalMove *= pushingSpeedScale;

        // Move our characeter
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
