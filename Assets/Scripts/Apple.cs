using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private Transform wormSP;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hen") && GameManager.instance.pecking)
        {
            GameObject obj = ObjectPooler.instance.GetPooledObject();

            obj.transform.position = wormSP.position;
            obj.transform.rotation = wormSP.rotation;
            obj.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
