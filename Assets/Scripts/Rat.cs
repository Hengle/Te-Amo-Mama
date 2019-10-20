using UnityEngine;
using UnityEngine.SceneManagement;

public class Rat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private float startWaitTime;
    [SerializeField] Transform[] moveSpot;
    bool movingLeft = true;
    SpriteRenderer ratSprite;
    private int nextSpot;

    // Start is called before the first frame update
    void Start()
    {
        ratSprite = GetComponent<SpriteRenderer>();
        waitTime = startWaitTime;
        nextSpot = Random.Range(0, moveSpot.Length);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot[nextSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot[nextSpot].position) < .2f)
        {
            if (waitTime <= 0)
            {
                nextSpot = Random.Range(0, moveSpot.Length);
                waitTime = startWaitTime;
                if (nextSpot == 0)
                {
                    movingLeft = true;
                }

                else if (nextSpot == 1)
                {
                    movingLeft = false;
                }

                //PlaySounds.SFXInstance().PlaySound(3);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

            if (movingLeft)
            {
                ratSprite.flipX = false;

            }

            else
            {

                ratSprite.flipX = true;
            }

        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Hen mother = other.gameObject.GetComponent<Hen>();

        if (other.CompareTag("Chick"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (mother != null && mother.pecking)
        {
            Destroy(gameObject, .5f);
        }

    }


    /*void flip()
     * 
    {
    
         
    }*/

}
