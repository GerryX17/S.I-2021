using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager Instance;

    public AudioClip shootClip;
    public AudioClip enemyHitClip;
    public AudioClip asteroidPushClip;
    public AudioClip transformClip;

    public AudioSource backgroundMusic;
    public bool playBGMusic;
    public bool isBGPlaying;

    public AudioSource finalBossMusic;
    public bool playFBMusic;
    public bool isFBPlaying;

    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;

        AudioSource[] audios = GetComponents<AudioSource>();

        backgroundMusic = audios[0];
        playBGMusic = true;
        isBGPlaying = false;

        finalBossMusic = audios[1];
        playFBMusic = false;
        isFBPlaying = false;
    }

    private void FixedUpdate()
    {
        if (playBGMusic == true &&  isBGPlaying == false)
        {
            if ( playFBMusic )
            {
                playFBMusic = false;
                isFBPlaying = false;
                backgroundMusic.mute = false;
                finalBossMusic.Stop();
            }

            isBGPlaying = true;

            backgroundMusic.Play();
        }

        if (playFBMusic == true && isFBPlaying == false)
        {
            backgroundMusic.mute = true;

            isBGPlaying = false;

            isFBPlaying = true;

            finalBossMusic.Play();
        }
    }

    public void PlayShootClip()
    {
        PlaySound(shootClip);
    }

    public void PlayEnemyHitClip()
    {
        PlaySound(enemyHitClip);
    }

    public void PlayAsteroidHitClip()
    {
        PlaySound(asteroidPushClip);
    }
    public void PlayTransformClip()
    {
        PlaySound(transformClip);
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, cameraPosition, 0.4f);
    }

    public void playFinalBoss()
    {

    }

}
