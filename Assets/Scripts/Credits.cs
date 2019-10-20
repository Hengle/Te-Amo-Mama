using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject loader;

    LevelLoader loadScript;

    private void Start()
    {
        loadScript = loader.GetComponent<LevelLoader>();
    }

    public void LoadStart()
    {
        loadScript.LoadLevel(0);
    }
}
