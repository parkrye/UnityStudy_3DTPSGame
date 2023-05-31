using UnityEngine;

public class DropWeaponItem : DropItem
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerWeaponManager>().SetWeapon(name))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
