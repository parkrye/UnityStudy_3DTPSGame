using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Poolable : MonoBehaviour
{
    [SerializeField] bool autoRelease;
    [SerializeField] float releaseTime;

    public PoolManager pool;

    private void OnEnable()
    {
        if (autoRelease)
            releaseRoutine = StartCoroutine(ReleaseRoutine());
    }

    public void Release()
    {
        if (releaseRoutine != null)
            StopCoroutine(releaseRoutine);
        if (pool != null)
            pool.Release(this);
    }

    Coroutine releaseRoutine;
    IEnumerator ReleaseRoutine()
    {
        yield return new WaitForSeconds(releaseTime);
        if (pool != null)
            pool.Release(this);
    }
}
