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

    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;
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
    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, cameraPosition, 0.4f);
    }
}
