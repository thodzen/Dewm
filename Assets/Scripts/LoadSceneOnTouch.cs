using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTouch : MonoBehaviour
{
    public float delay = 440;

    public string levelToLoad;

    public string sceneName;

    public AudioSource soundClip;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            soundClip.Play();
            Invoke("DelayedAction", delay);
        }
    }

    public void DelayedAction()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
