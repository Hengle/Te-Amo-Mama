using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderScare : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chick")) 
        {
            PlaySounds.SFXInstance().PlaySound(1);
        }
    }
}
