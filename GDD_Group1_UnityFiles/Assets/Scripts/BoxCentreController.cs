using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCentreController : MonoBehaviour
{
    public Transform reference;
    public Vector3 toAdd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = reference.position + toAdd;
    }
}
