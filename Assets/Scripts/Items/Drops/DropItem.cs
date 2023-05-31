using System.Collections;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(UpDown());
    }

    IEnumerator UpDown()
    {
        while (true)
        {
            transform.Rotate(Vector3.up);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
