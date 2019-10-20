using UnityEngine;

public class KnockOver : MonoBehaviour
{
	Animator anim;
	private bool knocked;
    bool played;
    public float timeToPlay = 0f;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

    private void Update()
    {
        if (GameManager.instance.restartingFromLastCheckpoint)
        {
            knocked = false;
        }

        anim.SetBool("Knocked", knocked);
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{


        if (collision.gameObject.CompareTag("Hen") && !knocked)
        {

            knocked = true;
        }
    }

    public void EndAnim() 
    {
        PlaySounds.SFXInstance().PlaySound(3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Hen") && !knocked)
		{
            knocked = true;
		}
    }
}