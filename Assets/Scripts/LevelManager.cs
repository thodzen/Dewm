using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager lm;

    public GameObject currentCheckpoint;

    public PlayerController player;

    public float respawnDelay;

    private new CameraController camera;

    public const int maxKeys = 16;
    private bool[] keysOwned = new bool[maxKeys]; //Max of 16 keys


    private void Awake()
    {
        if (lm == null)
        {
            lm = GameObject.FindGameObjectWithTag("LM").GetComponent<LevelManager>();
        }
    }

    void Start()
    {

        //player = FindObjectOfType<PlayerController>(); //find PlayerController in the level and assign it to player

        camera = FindObjectOfType<CameraController>();
        
        //healthManager = FindObjectOfType<HealthManager>();
    }

    public void resetKeys()
    {
        for (int i = 0; i < maxKeys; i++)
        {
            keysOwned[i] = false;
        }
    }

    public bool getKeyOwned(int index)
    {
        if (index >= 0 && index < maxKeys)
        {
            return keysOwned[index];
        }
        else
        {
            return false;
        }
    }

    public void setKeyOwned(int index)
    {
        if (index >= 0 && index < maxKeys)
        {
            keysOwned[index] = true;
        }
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    public static void KillPlayer(GameObject player)
    {
        Destroy(player.gameObject);
    }

    public void RespawnPlayer() //handles death and respawn
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        yield return new WaitForSeconds(3.0f);
        player.enabled = false; //makes player disappear after death
        player.GetComponent<Renderer>().enabled = false; //makes player disappear after death
        camera.isFollowing = false;
        Debug.Log("Player Respawn");
        yield return new WaitForSeconds(respawnDelay); //handles delaying respawn
        player.transform.position = new Vector3(currentCheckpoint.transform.position.x, currentCheckpoint.transform.position.y, player.transform.position.z); //handles spawn reposition
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        camera.isFollowing = true;
    } 
    
}