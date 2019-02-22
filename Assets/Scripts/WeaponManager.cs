using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;


    public Weapon Weapon
    {
        get { return this.weapon; }
        set { Give(value); }
    }


    public void Give(Weapon weapon)
    {
        this.weapon?.gameObject?.SetActive(false);

        this.weapon = weapon;
    }
}
