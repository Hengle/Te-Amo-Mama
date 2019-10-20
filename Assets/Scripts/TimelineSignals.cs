using UnityEngine;

public class TimelineSignals : MonoBehaviour
{
    public ParticleSystem eggBreak;

    public void StartCutscene()
    {
        GameManager.instance.inCutscene = true;
        GameManager.instance.inLaterLevel = true;
        GameManager.instance.followInCutscene = false;
    }

    public void ResetTime()
    {
        Time.timeScale = 1f;
    }

    public void EndCutscene()
    {
        GameManager.instance.inCutscene = false;
        GameManager.instance.inLaterLevel = true;
    }

    public void FollowInCutscene()
    {
        GameManager.instance.followInCutscene = true;
        GameManager.instance.followtheHen = true;
    }

    public void CrackTheEgg()
    {
        GameManager.instance.crackTheEgg = true;
    }

    public void CrackEffects()
    {
        PlaySounds.SFXInstance().PlaySound(5);
        Time.timeScale = 0.5f;
        eggBreak.Play();
    }

    public void AddRain()
    {
        PlaySounds.SFXInstance().ambientRain.Stop();
        PlaySounds.SFXInstance().ambientRain.clip = PlaySounds.SFXInstance().outsideRain;
        PlaySounds.SFXInstance().ambientRain.Play();
    }
}
