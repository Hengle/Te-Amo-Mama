using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DistractObj : MonoBehaviour
{
    private GameObject chick;
    bool distractionFinished = false;
    bool close;


    private void Awake()
    {
        chick = GameObject.FindGameObjectWithTag("Chick");
    }

    // Update is called once per frame
    void Update()
    {
        Distraction();
    }

    void Distraction()
    {
        float distractDis = Vector2.Distance(chick.transform.position, transform.position);

        if (distractDis < 2 && !distractionFinished)
        {
            //print("Should be distracting");
            close = true;
            GameManager.instance.distracted = true;
            GameManager.instance.followtheHen = false;
            distractionFinished = true;
        }

        if (GameManager.instance.restartingFromLastCheckpoint)
        {
            distractionFinished = false;
            GameManager.instance.distracted = false;
        }
    }
}