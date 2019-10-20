using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSKnock : MonoBehaviour
{ 
    Animator anim;
    private bool charg;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.instance.restartingFromLastCheckpoint)
        {
            charg = false;
        }

        anim.SetBool("Knocked", charg);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.henIsCharging && !charg && collision.gameObject.CompareTag("Hen"))
        {
            charg = true;
        }
    }

}
