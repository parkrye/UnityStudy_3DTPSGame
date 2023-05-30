using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] GameObject weaponBase;
    [SerializeField] MeshCollider[] weapons;
    [SerializeField] Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        weapons = weaponBase.GetComponentsInChildren<MeshCollider>();
        SetWaepon(-1);
    }

    public bool SetWaepon(int num)
    {
        for (int j = 0; j < weapons.Length; j++)
        {
            if (j != num)
                weapons[j].gameObject.SetActive(false);
        }
        if (num < 0 || num >= weapons.Length)
            return false;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == num)
            {
                weapons[i].gameObject.SetActive(true);
                animator.SetBool("Armored", true);
                return true;
            }
        }
        animator.SetBool("Armored", false);
        return false;
    }

    public bool SetWaepon(string name)
    {
        for (int j = 0; j < weapons.Length; j++)
        {
            if (weapons[j].name != name)
                weapons[j].gameObject.SetActive(false);
        }
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].name == name)
            {
                weapons[i].gameObject.SetActive(true);
                animator.SetBool("Armored", true);
                return true;
            }
        }
        animator.SetBool("Armored", false);
        return false;
    }
}
