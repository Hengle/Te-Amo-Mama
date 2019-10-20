using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Transform wormSP;

    private float idleMass = 100f;
    private float moveMass = 1f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.mass = idleMass;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hen") && GameManager.instance.henIsCharging)
        {
            rb.mass = moveMass;

            GameObject obj = ObjectPooler.instance.GetPooledObject();

            obj.transform.position = wormSP.position;
            obj.transform.rotation = wormSP.rotation;
            obj.SetActive(true);

            Destroy(gameObject, 1f);
        }
    }
}
