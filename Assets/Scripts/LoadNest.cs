using UnityEngine;

public class LoadNest : MonoBehaviour
{
    public GameObject loader;

    LevelLoader loadScript;

    private void Start()
    {
        loadScript = loader.GetComponent<LevelLoader>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hen") && GameManager.instance.chickIsHiding)
        {
            GameManager.instance.ResetGameState(true);
            loadScript.LoadLevel(4);
        }
    }
}
