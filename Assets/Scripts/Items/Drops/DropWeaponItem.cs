using UnityEngine;

public class DropWeaponItem : DropItem
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerWeaponManager>().SetWeapon(name))
            {
                dropper?.DropDestroyed(gameObject);
                return;
            }
        }
        base.OnTriggerEnter(other);
    }
}
