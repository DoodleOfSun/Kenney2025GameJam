using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject spark;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float angle = 90f;
        transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        rb = GetComponent<Rigidbody>();
        Debug.Log(Player.Instance.transform.forward);
        StartCoroutine(DestroyByTime());
    }

    void FixedUpdate()
    {
        rb.AddForce(Player.Instance.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Zombie"))
        {
            Debug.Log("Á»ºñ ¸ÂÃè¾î!");

            Instantiate(spark, other.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}