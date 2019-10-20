using UnityEngine;
using UnityEngine.Playables;

public class DogDoor : MonoBehaviour
{
    [SerializeField] private GameObject timelineManager;
    private PlayableDirector timeline;

	private void Awake()
	{
        timeline = timelineManager.GetComponent<PlayableDirector>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hen") && GameManager.instance.henIsCharging && !GameManager.instance.inCutscene && !GameManager.instance.inLaterLevel)
        {
            timeline.Play();
            GameManager.instance.followtheHen = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Hen") && GameManager.instance.henIsCharging && !GameManager.instance.inCutscene && !GameManager.instance.inLaterLevel)
		{
            timeline.Play();
            GameManager.instance.followtheHen = false;
		}
	}
}
