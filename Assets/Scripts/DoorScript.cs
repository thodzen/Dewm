using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int doorID = 0;
    public bool opened = false;

    private double preY = 0;
    // Start is called before the first frame update
    void Start()
    {
        preY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.lm.getKeyOwned(doorID))
        {
            opened = true;
        }

        if(opened==true)
        {
            if(transform.position.y < preY+2)
                transform.Translate(0, 0.1f, 0);
        }

    }
}
