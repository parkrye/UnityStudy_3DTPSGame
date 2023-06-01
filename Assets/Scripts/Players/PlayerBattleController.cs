using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class PlayerBattleController : MonoBehaviour
{
    [SerializeField] PlayerWeaponManager weaponManager;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particle;
    [SerializeField] bool shot, cooldown, isReloading;
    [SerializeField] UnityEvent ShotEvent;
    [SerializeField] TwoBoneIKConstraint leftHand;
    [SerializeField] GameObject trail;

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
                shot = false;
        }
    }

    void OnReload()
    {
        if (animator.GetBool("Armored") && !isReloading)
        {
            leftHand.weight = 0f;
            isReloading = true;
            animator.SetTrigger("Reload");
            StartCoroutine(Reloading());
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
                StartCoroutine(Trailing(particle.transform.position, Camera.main.transform.forward * weaponManager.Range));
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponManager.Range, LayerMask.GetMask("Hitable")))
                    hit.transform.GetComponent<Hitable>().Hit(weaponManager.Damage, hit);
                StartCoroutine(CoolDown());
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(weaponManager.CoolDown);
        cooldown = true;
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(2.5f);
        isReloading = false;
        leftHand.weight = 1f;
    }

    IEnumerator Trailing(Vector3 startPos, Vector3 endPos)
    {
        trail.transform.position = startPos;
        trail.SetActive(true);
        float time = 0;
        while(time < 1)
        {
            trail.transform.transform.position = Vector3.Lerp(startPos, endPos, time);
            time += Time.deltaTime;
            yield return null;
        }
        trail.SetActive(false);
        trail.transform.position = startPos;
    }
}
