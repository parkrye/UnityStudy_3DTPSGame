using UnityEngine;

public class DropSkinItem : DropItem
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerSkinManager skinManager = other.GetComponent<PlayerSkinManager>();
            if (skinManager.SetModel(name))
            {
                Destroy(gameObject);
                return;
            }

            if (skinManager.SetHair(name))
            {
                Destroy(gameObject);
                return;
            }

            if (skinManager.SetPack(name))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
