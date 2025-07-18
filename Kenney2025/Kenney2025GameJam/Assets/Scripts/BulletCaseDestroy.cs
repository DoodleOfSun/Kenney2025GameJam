using UnityEngine;
using System.Collections;

public class BulletCaseDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DestroyByTime();
    }

    private IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(this);
    }
}