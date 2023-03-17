using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public string deathText = "";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<GameManager>().GameOver(deathText);
        FindObjectOfType<AudioManager>().Play("LossMusic");
    }
}
