using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] AudioSource soundEffects;
    public AudioClip[] sfxClip;
    public AudioClip insideRain;
    public AudioClip outsideRain;
    public AudioSource ambientRain;
    
    [SerializeField] GameObject player;

    private static PlaySounds sfxInstance;

    public static PlaySounds SFXInstance()
    {
        if (sfxInstance == null)
            sfxInstance = FindObjectOfType(typeof(PlaySounds)) as PlaySounds;

        return sfxInstance;
    }

    private void Awake()
    {
        ambientRain = GetComponent<AudioSource>();
        ambientRain.Play();
    }

    public void PlaySound(int clip)
    {
        player = GameObject.FindGameObjectWithTag("Hen");

        soundEffects = player.GetComponent<AudioSource>();

        soundEffects.PlayOneShot(sfxClip[clip]);
    }
}
