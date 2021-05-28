using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager Instance; // 1

    public AudioClip shootClip; // 2
    public AudioClip shipHitClip; // 3
    public AudioClip shipDroppedClip; // 4

    private Vector3 cameraPosition; // 5
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this; // 1
        cameraPosition = Camera.main.transform.position; // 2
    }
    public void PlayShootClip()
    {
        PlaySound(shootClip);
    }

    public void PlayShipHitClip()
    {
        PlaySound(shipHitClip);
    }


    //BORRAR EL DROPPED PARA MAS ADELANTEE !!!
    public void PlayShipDroppedClip()
    {
        PlaySound(shipDroppedClip);
    }
    private void PlaySound(AudioClip clip) // 1
    {
        AudioSource.PlayClipAtPoint(clip, cameraPosition); // 2
    }
}
