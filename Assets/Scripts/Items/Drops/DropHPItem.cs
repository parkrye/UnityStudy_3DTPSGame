using UnityEngine;

public class DropHPItem : DropItem
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerDataManager>().HP += 10;
        }
    }
}
