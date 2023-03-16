using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachGoal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Flag Reached!");
        FindObjectOfType<GameManager>().LevelComplete();
    }
}

