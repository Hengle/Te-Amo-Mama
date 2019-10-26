using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSchick : MonoBehaviour
{
    bool play;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!play)
        {
            if (other.CompareTag("Chick"))
            {
                PlaySounds.SFXInstance().PlaySound(1);
                GameManager.instance.content = false;
                play = true;
            }
        }
    }
}
