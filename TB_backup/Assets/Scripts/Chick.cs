using System;
using UnityEngine;
using UnityEngine.Assertions;

public class Chick : MonoBehaviour
{
	//Needs to be shown in Inspector
	[SerializeField] private float speed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float letWalk;
    [SerializeField] private float hideDistance;
    [SerializeField] private Transform targetObj;

    //Does Not Need to be shown
    private bool hide;
    private bool play;
    private bool following;
	private bool isMoving;
    private bool content;
	SpriteRenderer mySpriteRenderer;
	Animator anim;
    public float stopDistract = 0f;
    public float timerToChange = 3f;

	void Awake()
	{
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		NullChecking();
    }

	void Update()
	{
		Follow();
		Flip();
		HideChick();
		SetAnimBools();
        content = GameManager.instance.content;
    }

    private void OnParticleCollision()
    {
        content = false;
        print("I'm scared");
        if (!play) 
        {
            PlaySounds.SFXInstance().PlaySound(2);
            play = true;
        }
    }

    void NullChecking()
	{
		//Check to make sure values have been set in Inspector
		Assert.AreNotEqual(0, speed);
		Assert.AreNotEqual(0, stoppingDistance);
		Assert.IsNotNull(targetObj);
	}

    public void SetTarget(Transform newTarget)
    { 
        targetObj = newTarget;
        timerToChange -= Time.deltaTime;
    }

    void Follow()
    {
        var henPos = new Vector2(targetObj.position.x, transform.position.y);
        following = GameManager.instance.followtheHen;
        var movingToMother = Vector2.Distance(transform.position, targetObj.position) > stoppingDistance && following && content && isMoving;
        var waitingToWalk = Vector2.Distance(transform.position, targetObj.position) > letWalk && following && content;

        if (movingToMother && GameManager.instance.followInCutscene)
		{
			transform.position = Vector2.MoveTowards(transform.position, henPos, speed * Time.deltaTime);
		}
        if (waitingToWalk && GameManager.instance.followInCutscene)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }


    void Flip()
	{
		if (targetObj.position.x > transform.position.x)
		{
			mySpriteRenderer.flipX = false;
		}

		else if (targetObj.position.x < transform.position.x)
		{
			mySpriteRenderer.flipX = true;
		}
	}

	void HideChick()
	{
		hide = GameManager.instance.hideTheChick;

        //print(Vector2.Distance(transform.position, targetObj.position));

		//print("Is moving: " + isMoving);
		//print("Hiding: " + hide);

		if (!isMoving && hide && Vector2.Distance(transform.position, targetObj.position) <= hideDistance)
		{
			anim.SetBool("Hiding", true);
            GameManager.instance.content = true;
            GameManager.instance.chickIsHiding = true;
            content = true;
			//mySpriteRenderer.enabled = false;
		}

		else if (isMoving || !hide)
		{
            GameManager.instance.chickIsHiding = false;
            anim.SetBool("Hiding", false);
		}
	}

   
    void SetAnimBools()
	{
		anim.SetBool("isMoving", isMoving);

        if (GameManager.instance.crackTheEgg)
        {
            anim.SetBool("Cracked", true);
        }
	}
}
