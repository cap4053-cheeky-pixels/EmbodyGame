using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour, IWeaponController
{
    [SerializeField]
    private string projectileTag = "Untagged";
    [SerializeField]
    private GameObject weaponInstance;

    private Weapon weapon;


    /* Used to set up this script.
     */
    void Awake()
    {
        if (weaponInstance.scene.name == null)
            Debug.LogError("ShootController has non-active weapon reference!!");

        SetWeaponInstance(weaponInstance);
    }

    /**
        This method should be called to set this controller's weapon.
        The controller will handle firing the weapon through the exposed fire
        methods.
     */
    public void SetWeaponInstance(GameObject w)
    {
        weaponInstance = w;
        // Maybe this should look for ProjectileWeapon?
        weapon = weaponInstance.GetComponentInChildren<Weapon>();
    }
    public GameObject GetWeaponInstance()
    {
        return weaponInstance;
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
        if (weapon == null) return;
        weapon.Attack(projectileTag);
    }
}
