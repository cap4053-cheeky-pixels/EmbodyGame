using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
    void SetWeaponInstance(GameObject wi);
    GameObject GetWeaponInstance();

}
