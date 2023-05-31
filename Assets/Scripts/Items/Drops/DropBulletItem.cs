using UnityEngine;

public class DropBulletItem : DropItem
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerDataManager>().Bullets += 30;
        }
    }
}
