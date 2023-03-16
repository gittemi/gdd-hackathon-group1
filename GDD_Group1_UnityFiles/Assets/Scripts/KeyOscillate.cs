using UnityEngine;

public class KeyOscillate : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float freq = 1.0f;
    public float phi = 0f;

    float originX;
    float originY;

    float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        originX = GetComponent<Transform>().position.x;
        originY = GetComponent<Transform>().position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(originX, originY + (amplitude * (Mathf.Sin(2 * (Mathf.PI) * freq * t + phi))), 1f);
        Vector3 delta = new Vector3(originX, originY + (amplitude * (Mathf.Sin(2 * (Mathf.PI) * freq * t + phi))), 1f) - transform.position;
        transform.position += new Vector3(0f, delta.y, 0f);
        t += Time.deltaTime;
    }
}
