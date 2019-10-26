using UnityEngine;

public class Worm : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chick"))
        {
            gameObject.SetActive(false);
        }
    }
}
