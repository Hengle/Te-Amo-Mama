using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hen")) 
        {
            GameManager.instance.lastCheckpoint = transform.position; 
        }
    }
}
