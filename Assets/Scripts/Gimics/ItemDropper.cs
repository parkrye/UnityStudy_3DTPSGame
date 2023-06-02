using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] BoxCollider rangeCollider;
    [SerializeField] GameObject[] itemPrefabs;
    [SerializeField] DropItem[] dropItems;
    [SerializeField] float dropDelay;
    [SerializeField] int dropCount, dropMax;
    [SerializeField] List<GameObject> itemPool;

    void Awake()
    {
        rangeCollider = GetComponent<BoxCollider>();
        dropItems = GetComponentsInChildren<DropItem>();
        itemPrefabs = new GameObject[dropItems.Length];
        for(int i = 0; i < dropItems.Length; i++)
        {
            itemPrefabs[i] = dropItems[i].gameObject;
            itemPrefabs[i].SetActive(false);
        }

        itemPool = new List<GameObject>();
        for(int i = 0; i < dropItems.Length; i++)
        {
            GameObject drop = Instantiate(itemPrefabs[i], Return_RandomPosition(), Quaternion.identity);
            drop.name = itemPrefabs[i].name;
            drop.transform.parent = transform;
            drop.GetComponent<DropItem>().dropper = this;
            itemPool.Add(drop);
        }
    }

    void Start()
    {
        StartCoroutine(DropItem());
    }

    IEnumerator DropItem()
    {
        while(true)
        {
            if(dropCount < dropMax)
            {
                if(itemPool.Count == 0)
                {
                    for (int i = 0; i < dropItems.Length; i++)
                    {
                        GameObject add = Instantiate(itemPrefabs[i], Return_RandomPosition(), Quaternion.identity);
                        add.name = itemPrefabs[i].name;
                        add.transform.parent = transform;
                        add.GetComponent<DropItem>().dropper = this;
                        itemPool.Add(add);
                    }
                }

                int randNum = Random.Range(0, itemPool.Count);
                GameObject drop = itemPool[randNum];
                itemPool.Remove(drop);
                drop.SetActive(true);
                dropCount++;
            }
            yield return new WaitForSeconds(dropDelay);
        }
    }

    public void DropDestroyed(GameObject getback)
    {
        getback.SetActive(false);
        getback.transform.position = transform.position;
        itemPool.Add(getback);
        dropCount--;
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = transform.position;

        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
}
