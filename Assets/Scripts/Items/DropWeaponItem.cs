using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeaponItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerWeaponManager weaponManager = other.GetComponent<PlayerWeaponManager>();
            if (weaponManager.SetWaepon(name))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
