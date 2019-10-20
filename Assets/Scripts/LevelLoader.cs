using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadScreen;
    public TextMeshProUGUI loadText;

    private void Start()
    {
        loadScreen.SetActive(false);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadText.text = progress * 100f + "%";

            yield return null;
        }

        if (operation.isDone)
        {
            GameManager.instance.ResetGameState(true);
            //GameManager.instance.fullRestart = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
