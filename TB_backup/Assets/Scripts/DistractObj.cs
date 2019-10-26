using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DistractObj : MonoBehaviour
{
    [SerializeField] private Transform chick;
    public bool close;
    public GameObject bbychick;


    // Update is called once per frame
    void Update()
    {
        Distraction();
    }

    void Distraction()
    {
        Chick settingTgt = bbychick.GetComponent<Chick>();


        float distractDis = Vector2.Distance(chick.transform.position, transform.position);

        if (distractDis < 2)
        {
            settingTgt.SetTarget(transform);
            close = true;
        }


    }
}