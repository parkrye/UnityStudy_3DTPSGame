using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemySpawner spawner;
    [SerializeField] bool find;
    [SerializeField] Transform player, weapon;
    [SerializeField] NavMeshAgent agent;

    public void Work(Transform _player)
    {
        find = false;
        player = _player;
        agent = GetComponent<NavMeshAgent>();
        gameObject.SetActive(true);
    }

    void Update()
    {
        if(player != null)
        {
            DestroyDoor();
            if (!find)
                Search();
            else
                Chase();
        }
    }

    void Search()
    {
        if(Vector3.Distance(transform.position, player.position) < 10f)
        {
            Find(1);
            return;
        }

        if (Vector3.Distance(transform.position, agent.destination) < 0.5f)
            agent.SetDestination(player.position);
    }

    public void Find(int hp)
    {
        if (hp <= 0)
        {
            Die();
            return;
        }
        if (!find)
        {
            find = true;
            StartCoroutine(Attack());
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        player = null;
        agent = null;
        spawner.DestroyEnemy(gameObject);
    }

    void Chase()
    {
        transform.LookAt(player.position);
        agent.SetDestination(player.position);
    }

    IEnumerator Attack()
    {
        while (find)
        {
            for(int i = 0; i < 60; i++)
            {
                weapon.localEulerAngles += new Vector3(5f, 0f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
            for(int i = 0; i < 60; i++)
            {
                weapon.localEulerAngles -= new Vector3(5f, 0f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    void DestroyDoor()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1f, LayerMask.GetMask("Hitable")))
            if(hit.transform.tag != "Enemy")
                hit.transform.GetComponent<Hitable>().Hit(1, hit);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            other.GetComponent<PlayerDataManager>().HP -= 5;
    }
}
