using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnImpact : MonoBehaviour
{
    public AudioSource impactSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            impactSound.Play();
        }
    }
}
