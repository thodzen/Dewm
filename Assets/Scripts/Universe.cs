using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    public static Universe Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("A universe already exists!!");
            Destroy(this);
        }

        m_AudioSource = GetComponent<AudioSource>();
    }

    #region Sound

    private AudioSource m_AudioSource;


    public static void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        Instance?.m_AudioSource.PlayOneShot(clip, volume);
    }

    #endregion
}
