//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SublevelBoxEnvironment : MonoBehaviour
{
    public Transform playerTransform;
    //public GameObject player;
    public Transform boxCentre;
    //public PlayerMovement playerMovement;
    public Rigidbody2D playerRigidBody;
    public Rigidbody2D portalFrame;
    public CharacterController2D characterController;
    public Camera camera;
    public float playerScale = 0.2f;
    public float cameraZoomSpeed = 10.0f;
    public float cameraMoveSpeed = 10.0f;
    public float cameraRotSpeed = 10.0f;

    public string gravityDirection = "Down";

    float targetCameraSize;
    Vector3 defaultCameraPos;
    Vector3 targetCameraPos;
    Quaternion targetCameraRot;

    bool inBox = false;
    int shrunkLevel = 0;

    Vector2 oldGravity;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && shrunkLevel == 0)
        {
            if (gravityDirection == "Right")
            {
                //Physics2D.gravity = new Vector2(-oldGravity.y, oldGravity.x);
                Physics2D.gravity = new Vector2(20f, 0f);
                playerTransform.Rotate(0f, 0f, 90.0f);
                characterController.direction = "R";
            }
            else
            {
                Physics2D.gravity = oldGravity;
                characterController.direction = "D";
            }

            Debug.Log("Player hit!");
            inBox = true;
            playerTransform.localScale = playerTransform.localScale * playerScale;
            //playerMovement.jumpSpeed *= Mathf.Pow(playerScale, 0.5f);
            characterController.m_JumpForce *= Mathf.Pow(playerScale, 0.5f);
            playerRigidBody.gravityScale *= Mathf.Pow(playerScale, 0.5f);
            //playerMovement.moveSpeed *= playerScale;

            targetCameraSize *= playerScale;

            //collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            portalFrame.bodyType = RigidbodyType2D.Static;
            shrunkLevel++;
        }

        //Debug.Log("Collided!");

        //camera.orthographicSize *= playerScale;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && shrunkLevel == 1)
        {
            if(gravityDirection == "Right")
                playerTransform.Rotate(0f, 0f, -90.0f);
            Physics2D.gravity = oldGravity;
            characterController.direction = "D";

            Debug.Log("Left");
            inBox = false;
            playerTransform.localScale = playerTransform.localScale / playerScale;
            //playerMovement.jumpzSpeed /= Mathf.Pow(playerScale, 0.5f);
            //playerMovement.moveSpeed /= playerScale;
            characterController.m_JumpForce /= Mathf.Pow(playerScale, 0.5f);
            playerRigidBody.gravityScale /= Mathf.Pow(playerScale, 0.5f);
            targetCameraSize /= playerScale;

            //collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            portalFrame.bodyType = RigidbodyType2D.Dynamic;

            shrunkLevel--;
        }

        //camera.orthographicSize /= playerScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetCameraSize = camera.orthographicSize;
        targetCameraRot = camera.GetComponent<Transform>().rotation;
        defaultCameraPos = targetCameraPos = camera.transform.position;
        oldGravity = Physics2D.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (inBox)
            targetCameraPos = boxCentre.position;
        //targetCameraPos = gameObject.transform.position;
        else
            targetCameraPos = defaultCameraPos;

        float sizeIncrement = (targetCameraSize - camera.orthographicSize);
        Vector3 posIncrement = (targetCameraPos - camera.transform.position);

        float frameZoomSpeed = cameraZoomSpeed * Time.deltaTime;
        float frameMoveSpeed = cameraMoveSpeed * Time.deltaTime;

        if (Mathf.Abs(sizeIncrement) > frameZoomSpeed)
        {
            if (sizeIncrement < 0)
                sizeIncrement = -frameZoomSpeed;
            else
                sizeIncrement = frameZoomSpeed;
        }
        //if(Mathf.Abs(posIncrement.x) >= )
        posIncrement.x = Mathf.Max(posIncrement.x, -frameMoveSpeed);
        posIncrement.x = Mathf.Min(posIncrement.x, frameMoveSpeed);

        posIncrement.y = Mathf.Max(posIncrement.y, -frameMoveSpeed);
        posIncrement.y = Mathf.Min(posIncrement.y, frameMoveSpeed);

        posIncrement.z = 0;

        camera.orthographicSize += sizeIncrement;
        //camera.transform += posIncrement;
        camera.transform.position += posIncrement;
    }
}