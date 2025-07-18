using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float angle = 90f;
        transform.rotation = Quaternion.Euler(angle, 0f, 0f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BoxCollider>(out var box) && other.name.Contains("Zombie"))
        {
            Debug.Log("Á»ºñ ¸ÂÃè¾î!");
            Destroy(this);
        }
    }
}