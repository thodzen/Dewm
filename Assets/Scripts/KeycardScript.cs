using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardScript : MonoBehaviour
{
    public AudioSource doorSound;
    public AudioSource keySound;
    public AudioSource additionalSound;

    [SerializeField]
    private int keyID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager.lm.setKeyOwned(keyID);
            Destroy(gameObject);
            doorSound.Play();
            keySound.Play();
            additionalSound.Play();
        }
    }
}
