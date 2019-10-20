using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class StartUp : MonoBehaviour
{
    VideoPlayer splash;
    public GameObject startMenu;

    private void Start()
    {
        GameManager.instance.music.GetComponent<AudioSource>().Pause();
        startMenu.SetActive(false);
        splash = GetComponent<VideoPlayer>();

        splash.enabled = true;

        splash.Prepare();

        StartCoroutine(SplashWait());
    }

    IEnumerator SplashWait()
    {
        while (!splash.isPrepared)
        {
            yield return null;
        }

        splash.Play();

        yield return new WaitForSeconds(6f);

        startMenu.SetActive(true);
        GameManager.instance.music.GetComponent<AudioSource>().Play();
    }
}
