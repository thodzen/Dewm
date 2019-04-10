using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyOnTrigger : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform enemyPos;
    public AudioSource sound;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i <= 3; i++)
            {
                Debug.Log(i + " enemies spawned");
                Instantiate(enemyPrefab, enemyPos.position, enemyPos.rotation);               
            }
            sound.Play();
            Destroy(gameObject);
        }
    }

}
