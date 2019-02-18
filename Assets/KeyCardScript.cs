using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardScript : MonoBehaviour
{
    public int keyCardID = 0;
    public PlayerController p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(this.GetComponent<BoxCollider2D>().IsTouching( p.GetComponent<BoxCollider2D>()))
        {
            LevelManager.lm.setKeyOwned(keyCardID);
            Destroy(gameObject);
        }
    }
}
