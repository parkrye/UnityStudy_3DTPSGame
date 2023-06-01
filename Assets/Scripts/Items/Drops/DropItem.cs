using System.Collections;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public ItemDropper dropper;
    [SerializeField] protected bool drop;

    void Start()
    {
        drop = true;
        StartCoroutine(UpDown());
    }

    void Update()
    {
        if (drop)
        {
            transform.Translate(Vector3.down * 5f * Time.deltaTime);
            if(transform.position.y < -100)
                dropper?.DropDestroyed(gameObject);
        }
    }
    IEnumerator UpDown()
    {
        while (true)
        {
            transform.Rotate(Vector3.up);
            yield return new WaitForSeconds(0.1f);
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            dropper?.DropDestroyed(gameObject);
            return;
        }

        if (drop && other.tag == "Ground")
        {
            drop = false;
            StartCoroutine (PopUp());
        }
    }

    IEnumerator PopUp()
    {
        for(int i = 0; i < 100; i++)
        {
            transform.Translate(Vector3.up * 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
