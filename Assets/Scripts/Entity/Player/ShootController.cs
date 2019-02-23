using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject weapon;
    private IWeapon fireableWeapon;


    /* Used to set up this script.
     */
    void Awake()
    {
        SetWeapon(weapon);
    }

    /**
        This method should be called to set this controller's weapon.
        The controller will handle firing the weapon through the exposed fire
        methods.
     */
    public void SetWeapon(GameObject w)
    {
        weapon = w;
        fireableWeapon = weapon.GetComponentInChildren<IWeapon>();
    }

    /**
        This method will fire the weapon whilst ensuring this GameObject is facing
        the correct direction first.
     */
    public void FireWeaponTowards(Vector3 direction)
    {
        // Turn the GameObject
        transform.forward = direction;
        FireWeapon();
    }

    public void FireWeapon()
    {
        if (fireableWeapon == null) return;
        fireableWeapon.Fire("PlayerProjectile");
    }
}
