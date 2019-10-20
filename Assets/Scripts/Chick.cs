using UnityEngine;
using UnityEngine.Assertions;

public class Chick : MonoBehaviour
{
	//Needs to be shown in Inspector
    [SerializeField] private float normalSpeed;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float letWalk;
    [SerializeField] private float hideDistance;
    [SerializeField] private Transform targetObj;
    [SerializeField] GameObject Hearts;
    [SerializeField] GameObject ambiRain;


    //Does Not Need to be shown
    private float speed;
    private float timeSinceLastScare = float.MaxValue;
    private bool hide;
    private bool play;
    private bool following;
	private bool isMoving;
    private bool content;
	SpriteRenderer mySpriteRenderer;
	Animator anim;
    public AudioSource ambientRain;
    public float stopDistract = 0f;
    public float timerToChange = 3f;


	void Awake()
	{
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		NullChecking();
        ambientRain = GetComponent<AudioSource>();
    }

    void Update()
	{
		Follow();
		Flip();
		HideChick();
		SetAnimBools();
        LovetheDis();
        //ChangeRain();
        content = GameManager.instance.content;
    }

    private void OnParticleCollision()
    {
        //timeSinceLastScare += Time.deltaTime;
        GameManager.instance.content = false;
        PlaySounds.SFXInstance().PlaySound(4);
        PlaySounds.SFXInstance().PlaySound(2);


        anim.SetBool("Hiding", true);

        /*if (timeSinceLastScare >= timeToScare) 
        {
            
            timeSinceLastScare = 0;
        }*/
    }

    void NullChecking()
	{
		//Check to make sure values have been set in Inspector
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
			//anim.SetBool("Hiding", true);
            GameManager.instance.content = true;
            GameManager.instance.chickIsHiding = true;
            //GameManager.instance.distracted = false;
            //GameManager.instance.playScare = false;
            content = true;
            //mySpriteRenderer.enabled = false;
        }

		else if (isMoving || !hide && content)
		{
            GameManager.instance.chickIsHiding = false;
            anim.SetBool("Hiding", false);
        }
	}

   
    void SetAnimBools()
	{
		anim.SetBool("isMoving", isMoving);

        if (GameManager.instance.inLaterLevel && !GameManager.instance.inCutscene)
        {
            GameManager.instance.crackTheEgg = true;
        }

        if (GameManager.instance.crackTheEgg)
        {
            anim.SetBool("Cracked", true);
        }

        else
        {
            anim.SetBool("Cracked", false);
        }

        if (GameManager.instance.inCutscene)
        {
            speed = chargeSpeed;
        }

        else
        {
            speed = normalSpeed;
        }

        if (!GameManager.instance.content)
        {
            anim.SetBool("Hiding", true);
        }

        
	}

    void LovetheDis() 
    { 
        if (GameManager.instance.distracted || GameManager.instance.chickIsHiding) 
        {
            Hearts.SetActive(true);
        }

        if (!GameManager.instance.distracted && !GameManager.instance.chickIsHiding)
        {
            Hearts.SetActive(false);
        }
    }

   /*void ChangeRain(AudioClip music) 
    {

        ambientRain.Stop();
        ambientRain.clip = music;
        ambientRain.Play();

    */

    /*IEnumerator DamageFlash()
    {
        mySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        mySpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        mySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        mySpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        mySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        mySpriteRenderer.color = Color.white;
    }*/
}
