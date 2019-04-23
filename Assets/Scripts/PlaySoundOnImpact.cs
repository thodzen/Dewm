using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnImpact : MonoBehaviour
{
    public AudioSource impactSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        impactSound.Play();
    }
}
