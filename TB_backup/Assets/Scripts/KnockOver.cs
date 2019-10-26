using UnityEngine;

public class KnockOver : MonoBehaviour
{
	Animator anim;
	private bool knocked;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Hen") && !knocked)
		{
			knocked = true;
			anim.SetBool("Knocked", knocked);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Hen") && !knocked)
		{
			knocked = true;
			anim.SetBool("Knocked", knocked);
		}
	}
}