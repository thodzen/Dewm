using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
	[SerializeField]
	private List<Weapon> weapons;

	private int current;

	private void Awake()
	{
		weapons = GetComponentsInChildren<Weapon>(true).ToList();
		Switch(0);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Switch(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Switch(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Switch(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Switch(3);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			Switch(4);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			Switch(5);
		}
		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			Switch(6);
		}
		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			Switch(7);
		}
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			Switch(8);
		}
	}

	public void Allow(string name)
	{
		Weapon w = weapons.Find(x => x.name == name);

		if (w == null) return;

		w.canUse = true;
	}

	public void Switch(int weapon)
	{
		if (weapon < 0 || weapon >= weapons.Count) return;
		if (weapons[weapon].canUse)
		{
			weapons[current].gameObject.SetActive(false);
			weapons[weapon].gameObject.SetActive(true);

			Debug.Log("Switched to " + weapons[weapon].name);

			current = weapon;
		}
	}
}
