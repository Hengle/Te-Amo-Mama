using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hen") && GameManager.instance.chickIsHiding)
        {
            SceneManager.LoadScene(0);
        }
    }
}
