using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScripts : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerMovement pm;

    float g;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ladder");
        if (other.gameObject.tag == "Player")
        {
            
            rb.gravityScale = 0;
            pm.isOnLadder = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb.gravityScale = g;
            pm.isOnLadder = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        g = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
