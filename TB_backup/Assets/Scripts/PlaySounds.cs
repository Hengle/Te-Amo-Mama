using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] AudioSource soundEffects;
    public AudioClip[] sfxClip;
    [SerializeField] GameObject player;

    private static PlaySounds sfxInstance;

    public static PlaySounds SFXInstance()
    {
        if (sfxInstance == null)
            sfxInstance = FindObjectOfType(typeof(PlaySounds)) as PlaySounds;

        return sfxInstance;
    }

    // Update is called once per frame
    public void PlaySound(int clip)
    {
        player = GameObject.FindGameObjectWithTag("Hen");

        soundEffects = player.GetComponent<AudioSource>();

        //soundEffects.clip = sfxClip [clip];
        soundEffects.PlayOneShot(sfxClip[clip]);
    }
}
