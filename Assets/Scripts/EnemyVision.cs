using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
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
		if (collider.gameObject.tag.Equals ("Player"))
		{
			controller.SetPlayerInSight (true);
		}
	}

	public void OnTriggerExit2D (Collider2D collider)
	{
		if (collider.gameObject.tag.Equals ("Player"))
		{
			controller.SetPlayerInSight (false);
		}
	}
}
