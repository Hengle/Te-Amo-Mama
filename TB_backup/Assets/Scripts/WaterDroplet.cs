using UnityEngine;

public class WaterDroplet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chick") || collision.CompareTag("Hen") || collision.CompareTag("Floor"))
        {
            gameObject.SetActive(false);
        }
    }
}
