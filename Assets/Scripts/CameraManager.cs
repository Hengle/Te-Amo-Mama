using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject staticCam;
    [SerializeField] private GameObject dynamicCam;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hen") && GameManager.instance.followtheHen)
        {
            staticCam.SetActive(true);
            dynamicCam.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hen") && GameManager.instance.followtheHen)
        {
            dynamicCam.SetActive(true);
            staticCam.SetActive(false);
        }
    }
}
