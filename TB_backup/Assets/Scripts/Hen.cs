using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Hen : MonoBehaviour
{
	//Needs to be shown in Inspector
	[SerializeField] private string moveAxis;
	[SerializeField] private string hideButton;
	[SerializeField] private string jumpButton;
	[SerializeField] private string peckButton;
	[SerializeField] private string chirpButton;
	[SerializeField] private string chargeButton;
	[SerializeField] private float speed = 50f;
	//[SerializeField] private float jumpPower = 50f;
	[SerializeField] private float chargeTime = 3f;
	[SerializeField] private float chargingDistance = 10f;
	[SerializeField] private float chargeSpeed = 3f;
	[SerializeField] Transform groundCheck;
    [SerializeField] private GameObject chick;

    //Does not need to be shown
    private float chargeTimer;
	private float chargeCurrent;
	private bool grounded;
	private bool pecking;
	private bool flipped;
	private bool charging;
	private bool hidingChick;
	Animator anim;
	Transform _transform;
	SpriteRenderer mySpriteRenderer;

	void Awake()
	{
		_transform = GetComponent<Transform>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
        //GameManager.instance.lastCheckpoint = transform.position;
		NullChecking();
	}

	void NullChecking()
	{
		Assert.IsNotNull(moveAxis);
		Assert.IsNotNull(hideButton);
		Assert.IsNotNull(jumpButton);
		Assert.IsNotNull(peckButton);
		Assert.IsNotNull(chirpButton);
		Assert.IsNotNull(chargeButton);
		Assert.IsNotNull(groundCheck);
		Assert.AreNotEqual(0, speed);
		//Assert.AreNotEqual(0, jumpPower);
		Assert.AreNotEqual(0, chargeTime);
		Assert.AreNotEqual(0, chargingDistance);
		Assert.AreNotEqual(0, chargeSpeed);
	}

	void FixedUpdate()
	{
		Move();
	}

	void Update()
	{
		HideChick();
		//Jump();
		Peck();
		Whistle();
		ChargeAlgorithm();
		Charge();
		SetBools();

        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

	void Move()
	{
		if (!hidingChick && !charging && !GameManager.instance.inCutscene && !pecking)
		{
			float translate = Input.GetAxisRaw(moveAxis) * speed * Time.deltaTime;

			anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw(moveAxis)));

			_transform.Translate(translate, 0, 0);

			float move = Input.GetAxis(moveAxis);

			if (move < 0)
			{
				mySpriteRenderer.flipX = true;
				flipped = true;
			}
			else if (move > 0)
			{
				mySpriteRenderer.flipX = false;
				flipped = false;
			}
		}
	}

	/*void Jump()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Floor"));

		if (Input.GetButtonDown(jumpButton) && grounded && !GameManager.instance.hideTheChick)
		{
			rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		}
	}*/

	void Whistle()
	{
        if (Input.GetButtonDown(chirpButton) && !GameManager.instance.inCutscene)
		{
			PlaySounds.SFXInstance().PlaySound(0);
			if (GameManager.instance.followtheHen)
			{
				GameManager.instance.followtheHen = false;

            }

			else
			{
				GameManager.instance.followtheHen = true;

            }
		}
	}

	void ChargeAlgorithm()
	{
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Floor"));

        if (Input.GetButton(chargeButton) && !GameManager.instance.inCutscene && grounded)
		{
			chargeTimer += Time.deltaTime;
		}

		if (Input.GetButtonUp(chargeButton) && chargeTimer > chargeTime && grounded)
		{
			charging = true;
			chargeTimer = 0f;
		}

		if (Input.GetButtonUp(chargeButton) && chargeTimer < chargeTime)
		{
			chargeTimer = 0f;
		}
	}

	void Charge()
	{
		if (charging)
		{
			if (chargeCurrent < chargingDistance)
			{
				float chargeSpeedSigned;

				if (flipped)
				{
					chargeSpeedSigned = -chargeSpeed;
				}
				else
				{
					chargeSpeedSigned = chargeSpeed;
				}

				transform.position = new Vector2(Time.deltaTime * chargeSpeedSigned + transform.position.x, transform.position.y);
				chargeCurrent += Time.deltaTime * chargeSpeed;
			}
			else
			{
				charging = false;
				chargeCurrent = 0f;
				chargeTimer = 0f;
			}
		}
	}

	void Peck()
	{
		if (Input.GetButtonDown(peckButton) && !pecking && !GameManager.instance.inCutscene)
		{
			pecking = true;
		}

		if (Input.GetButtonUp(peckButton))
		{
			pecking = false;
		}
	}

	void HideChick()
	{
        Chick settingTgt = chick.GetComponent<Chick>();

        if (Input.GetButtonDown(hideButton) || Input.GetAxisRaw(hideButton) > 0)
		{
			GameManager.instance.hideTheChick = true;
            settingTgt.SetTarget(transform);
            mySpriteRenderer.sortingOrder = 2;
        }
		else if (Input.GetButtonUp(hideButton) || Input.GetAxisRaw(hideButton) <= 0)
		{
			GameManager.instance.hideTheChick = false;
            mySpriteRenderer.sortingOrder = 0;
        }
	}

	void SetBools()
	{
		hidingChick = GameManager.instance.hideTheChick;
		GameManager.instance.henIsCharging = charging;

		anim.SetBool("Hiding", hidingChick);
		anim.SetBool("Pecking", pecking);
		anim.SetBool("Charging", charging);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Solid") && charging)
		{
			charging = false;
		}
	}
     
}
