using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThunderScare : MonoBehaviour
{
    public int numFlashes = 2;
    public float onTime = 0.2f;
    public float offTime = 0.2f;
    public Color flashColor;

    public Image flashImage;

    IEnumerator ThunderFlash()
    {
        Color defaultColor = flashImage.color;

        for (int i = 0; i < numFlashes; i++)
        {
            // if the current color is the default color - change it to the flash color
            if (flashImage.color == defaultColor)
            {
                flashImage.color = flashColor;
            }

            yield return new WaitForSeconds(onTime);

            if (flashImage.color != defaultColor) // otherwise change it back to the default color
            {
                flashImage.color = defaultColor;
            }
            yield return new WaitForSeconds(offTime);
        }
        //Destroy(input.gameObject, 1); // magic door closes - remove object
        //yield return new WaitForSeconds(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chick")) 
        {
            StartCoroutine(ThunderFlash());
            PlaySounds.SFXInstance().PlaySound(1);
            GameManager.instance.content = false;
        }
    }
}
