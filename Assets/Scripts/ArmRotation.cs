using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 0;

	// Update is called once per frame
	void Update () {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //Subtracting position of player from mouse position
        difference.Normalize(); //The sum of the vector will be equal to 1

        float rotationZ = Mathf.Atan2(-difference.y, -difference.x) * Mathf.Rad2Deg; //Finding the angle and converting into degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);
	}
}
