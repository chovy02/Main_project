using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource vfx;

    public AudioClip musicClip;
    public AudioClip hurtClip;
    public AudioClip defeatClip;

    public AudioClip victoryClip;
    public AudioClip pickupClip;

    private PauseMenu paused;

    void Start()
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();

    }

    void Update()
    {
        if (paused.isActiveAndEnabled)
        {
            if (musicSource.isPlaying) musicSource.Pause();
        }
        else
        {
            if (!musicSource.isPlaying) musicSource.UnPause();
        }
    }

    public void Playvfx(AudioClip vfxClip)
    {

        vfx.PlayOneShot(vfxClip);
    }
}
