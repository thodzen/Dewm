using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerController player;

    public bool isFollowing;

    public Vector3 offset;

    public float smoothing;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void FixedUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position + offset, smoothing);
        transform.position = newPosition;
    }
}

