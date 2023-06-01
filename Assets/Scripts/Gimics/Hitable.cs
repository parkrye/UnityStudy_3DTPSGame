using UnityEngine;
using UnityEngine.Events;

public class Hitable : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] bool destroyable;
    [SerializeField] UnityEvent<int> HitEvent;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] Rigidbody rb;

    void Awake()
    {
        hitParticle = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
    }

    public void Hit(int damage, RaycastHit hit)
    {
        hp -= damage;
        if (rb != null)
            rb.AddForceAtPosition(hit.normal * -5f, hit.point, ForceMode.Impulse);
        if (hitParticle != null)
            hitParticle?.Play();
        HitEvent?.Invoke(hp);
        if (hp <= 0 && destroyable)
            Destroy(gameObject, 0.5f);
    }
}
