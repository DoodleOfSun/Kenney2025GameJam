using UnityEngine;
using System.Collections;

public class BulletCaseDestroy : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // X,Y로 양의 방향 (오른쪽대각선)
        rb.AddForce(new Vector3(5, 0, this.transform.position.z), ForceMode.Impulse);
        StartCoroutine(DestroyByTime());
    }

    private IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
}