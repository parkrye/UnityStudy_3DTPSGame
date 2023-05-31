using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBattleController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform shotPoint, shotTarget;
    [SerializeField] ParticleSystem particle;
    [SerializeField] bool shot, cooldown;
    [SerializeField] UnityEvent ShotEvent;

    void Start()
    {
        cooldown = true;
    }

    void OnFire()
    {
        if (animator.GetBool("Armored"))
        {
            if (!shot)
            {
                shot = true;
                StartCoroutine(Shot());
            }
            else
            {
                shot = false;
            }
        }
    }

    void OnReload()
    {
        if (animator.GetBool("Armored"))
        {
            animator.SetTrigger("Reload");
        }
    }

    IEnumerator Shot()
    {
        while (shot && GetComponent<PlayerDataManager>().Bullets > 0)
        {
            if (cooldown)
            {
                ShotEvent?.Invoke();
                particle.Play();
                cooldown = false;
                animator.SetTrigger("Shot");
                RaycastHit hit;
                if (Physics.Raycast(shotPoint.position, (shotTarget.position - shotPoint.position).normalized, out hit, 9999f, LayerMask.GetMask("Hitable")))
                {
                    hit.transform.GetComponent<Hitable>().Hit(GetComponent<PlayerWeaponManager>().Damage);
                }
                StartCoroutine(CoolDown());
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(GetComponent<PlayerWeaponManager>().CoolDown);
        cooldown = true;
    }
}
