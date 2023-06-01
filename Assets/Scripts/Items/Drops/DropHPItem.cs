using UnityEngine;

public class DropHPItem : DropItem
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerDataManager>().HP += 10;
            dropper?.DropDestroyed(gameObject);
            return;
        }
        base.OnTriggerEnter(other);
    }
}
