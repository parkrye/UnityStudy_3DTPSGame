using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSkinItem : MonoBehaviour
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
