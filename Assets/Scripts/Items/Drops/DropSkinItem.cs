using UnityEngine;

public class DropSkinItem : DropItem
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerSkinManager skinManager = other.GetComponent<PlayerSkinManager>();
            if (skinManager.SetModel(name))
            {
                dropper?.DropDestroyed(gameObject);
                return;
            }

            if (skinManager.SetHair(name))
            {
                dropper?.DropDestroyed(gameObject);
                return;
            }

            if (skinManager.SetPack(name))
            {
                dropper?.DropDestroyed(gameObject);
                return;
            }
        }
        base.OnTriggerEnter(other);
    }
}
