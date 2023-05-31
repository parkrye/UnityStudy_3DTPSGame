using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] GameObject weaponBase;
    [SerializeField] BoxCollider[] weapons;
    [SerializeField] Animator animator;
    [SerializeField] MultiAimConstraint rightHand;
    [SerializeField] TwoBoneIKConstraint leftHand;

    [SerializeField] int nowWeapon;

    public int Damage { get { return weapons[nowWeapon].GetComponent<Gun>().damage; } }
    public float CoolDown { get { return weapons[nowWeapon].GetComponent<Gun>().cooldown; } }

    void Awake()
    {
        weapons = weaponBase.GetComponentsInChildren<BoxCollider>();
        for (int j = 0; j < weapons.Length; j++)
        {
            weapons[j].gameObject.SetActive(false);
        }
        rightHand.weight = 0f;
        leftHand.weight = 0f;
    }

    public bool SetWeapon(int num)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == num)
            {
                weapons[i].gameObject.SetActive(true);
                animator.SetBool("Armored", true);

                for (int j = 0; j < weapons.Length; j++)
                {
                    if(i != j)
                        weapons[j].gameObject.SetActive(false);
                }
                animator.SetLayerWeight(1, 1f);
                rightHand.weight = 1f;
                leftHand.weight = 1f;
                nowWeapon = num;
                return true;
            }
        }
        animator.SetLayerWeight(1, 0f);
        rightHand.weight = 0f;
        leftHand.weight = 0f;
        return false;
    }

    public bool SetWeapon(string name)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].name == name)
            {
                weapons[i].gameObject.SetActive(true);
                animator.SetBool("Armored", true);

                for (int j = 0; j < weapons.Length; j++)
                {
                    if (i != j)
                        weapons[j].gameObject.SetActive(false);
                }
                animator.SetLayerWeight(1, 1f);
                rightHand.weight = 1f;
                leftHand.weight = 1f;
                nowWeapon = i;
                return true;
            }
        }
        animator.SetLayerWeight(1, 0f);
        rightHand.weight = 0f;
        leftHand.weight = 0f;
        return false;
    }
}
