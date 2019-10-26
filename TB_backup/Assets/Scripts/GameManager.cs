using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script

	public bool hideTheChick;
	public bool nextToHen;
    public bool followtheHen;
	public bool henIsCharging;
    public bool inCutscene;
    public bool chickIsHiding;
    public bool inLaterLevel;
    public bool crackTheEgg;
    public bool content;
    public bool followInCutscene = true;
    public Vector3 lastCheckpoint;


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
		Application.targetFrameRate = 300;
	}

    public void StartCutscene()
    {
        inCutscene = true;
        inLaterLevel = true;
        followInCutscene = false;
    }

    public void EndCutscene()
    {
        inCutscene = false;
    }

    public void FollowInCutscene()
    {
        followInCutscene = true;
    }

    public void CrackTheEgg()
    {
        crackTheEgg = true;
    }
}