using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyThisObject());
    }
    private IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
