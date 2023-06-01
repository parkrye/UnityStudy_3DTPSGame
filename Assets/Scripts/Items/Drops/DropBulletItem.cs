using UnityEngine;

public class DropBulletItem : DropItem
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerDataManager>().Bullets += 100;
            dropper?.DropDestroyed(gameObject);
            return;
        }
        base.OnTriggerEnter(other);
    }
}
