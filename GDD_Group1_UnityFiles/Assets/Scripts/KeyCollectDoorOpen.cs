using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectDoorOpen : MonoBehaviour
{
    public GameObject keySelf;
    public GameObject[] doors;
    public float timeTillDestroy = 0.5f;

    bool keyHit = false;
    float timeDoorGreen= 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Key Hit!");
            foreach(GameObject door in doors)
            {
                SpriteRenderer spriteRenderer = door.GetComponent<SpriteRenderer>();
                if(spriteRenderer != null)
                    spriteRenderer.color = new Color(0f, 1f, 0f, 1f);
                //door.SetActive(false);
            }
            keySelf.GetComponent<SpriteRenderer>().enabled = false;

            keyHit = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(keyHit)
        {
            timeDoorGreen += Time.deltaTime;
        }
        if(timeDoorGreen >= timeTillDestroy)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
            keySelf.SetActive(false);
        }
    }
}
