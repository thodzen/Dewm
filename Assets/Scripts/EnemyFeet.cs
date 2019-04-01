using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFeet : MonoBehaviour
{
	[SerializeField]
	private GameObject controllerObject;
	private EnemyController controller;

	void Start ()
	{
		controller = controllerObject.GetComponent<EnemyController> ();
	}

	void Update ()
	{
	}

	public void OnTriggerEnter2D (Collider2D collider)
	{
		controller.SomethingOnFeet (collider);
	}
}
