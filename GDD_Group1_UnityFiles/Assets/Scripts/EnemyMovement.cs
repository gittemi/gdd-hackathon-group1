using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float moveSpeed;
    public bool isRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight)
        {
            GetComponent<Transform>().position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            if (GetComponent<Transform>().position.x >= rightPoint.position.x)
            {
                isRight = false;
                GetComponent<Transform>().localScale = new Vector3(-GetComponent<Transform>().localScale.x,
                    GetComponent<Transform>().localScale.y, GetComponent<Transform>().localScale.z);
            }
        }
        else
        {
            GetComponent<Transform>().position += new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f);
            if (GetComponent<Transform>().position.x <= leftPoint.position.x)
            {
                isRight = true;
                GetComponent<Transform>().localScale = new Vector3(-GetComponent<Transform>().localScale.x,
                    GetComponent<Transform>().localScale.y, GetComponent<Transform>().localScale.z);
            }
        }
    }
}
