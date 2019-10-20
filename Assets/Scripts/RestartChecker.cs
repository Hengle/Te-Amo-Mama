using UnityEngine;

public class RestartChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hen") && !GameManager.instance.content)
        {
            GameManager.instance.RestartFromCheckpoint();
        }
    }
}
