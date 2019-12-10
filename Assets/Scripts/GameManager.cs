using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script

    public GameObject hen;
    public GameObject chick;
    public GameObject staticCam;
    public GameObject movingCam;
    public GameObject loader;
    LevelLoader loadScript;
    public bool hideTheChick;
    public bool fullRestart;
    public bool followtheHen;
	public bool henIsCharging;
    public bool inCutscene;
    public bool chickIsHiding;
    public bool inLaterLevel;
    public bool crackTheEgg;
    public bool content;
    public bool pecking;
    public bool distracted;
    public bool followInCutscene = true;
    public bool restartingFromLastCheckpoint;
    public Vector3 lastCheckpoint;
    bool notAtStart;
    public AudioSource music;

    //Awake is always called before any Start functions
    void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

        //Make the game run as fast as possible
        //Application.targetFrameRate = 60;

        music = GetComponent<AudioSource>();
	}

    public void RestartFromCheckpoint()
    {
        restartingFromLastCheckpoint = true;

        movingCam.SetActive(false);
        staticCam.SetActive(true);

        StartCoroutine(ResetPos());
    }

    public void FullRestart()
    {
        ResetGameState(true);
        

        loadScript = loader.GetComponent<LevelLoader>();

        loadScript.LoadLevel(3);
    }

    public void ExitToMainMenu()
    {
        ResetGameState(true);
        

        loadScript = loader.GetComponent<LevelLoader>();

        loadScript.LoadLevel(0);
    }

    public void GetReferences()
    {
        hen = GameObject.FindGameObjectWithTag("Hen");
        chick = GameObject.FindGameObjectWithTag("Chick");
        staticCam = GameObject.FindGameObjectWithTag("StaticCam");
        movingCam = GameObject.FindGameObjectWithTag("MovingCam");
        loader = GameObject.FindGameObjectWithTag("LevelLoader");
    }

    public void ResetGameState(bool isFullRestart)
    {
        if (isFullRestart)
        {
            hideTheChick = false;
            fullRestart = false;
            followtheHen = false;
            henIsCharging = false;
            inCutscene = false;
            chickIsHiding = false;
            inLaterLevel = false;
            crackTheEgg = false;
            content = false;
            pecking = false;
            followInCutscene = true;
            restartingFromLastCheckpoint = false;
            PlaySounds.SFXInstance().ambientRain.Stop();
            PlaySounds.SFXInstance().ambientRain.clip = PlaySounds.SFXInstance().insideRain;
        }
    }

    IEnumerator ResetPos()
    {
        yield return new WaitForSeconds(.5f);

        content = true;
        hen.transform.position = lastCheckpoint;
        chick.transform.position = new Vector3(lastCheckpoint.x + 2, lastCheckpoint.y, lastCheckpoint.z);

        yield return new WaitForSeconds(1f);

        staticCam.SetActive(false);

        if (inLaterLevel)
        {
            movingCam.SetActive(true);
        }

        restartingFromLastCheckpoint = false;
    }
}