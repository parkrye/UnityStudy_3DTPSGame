using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUser : MonoBehaviour
{
    PoolManager poolManager;
    public GameObject prefab;

    void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            poolManager.Get(prefab);
        }
    }
}
