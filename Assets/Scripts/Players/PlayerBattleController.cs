using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnFire()
    {
        animator.SetTrigger("Shot");
    }

    void OnReload()
    {
        animator.SetTrigger("Reload");
    }
}
