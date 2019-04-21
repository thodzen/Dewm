using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Light thisLight = GetComponent<Light>();
        float dis = (thisLight.transform.position - player.transform.position).magnitude;
        if (dis > thisLight.range)
        {
            thisLight.enabled = false;
        }
        else
        {
            thisLight.enabled = true;
        }
    }
}
