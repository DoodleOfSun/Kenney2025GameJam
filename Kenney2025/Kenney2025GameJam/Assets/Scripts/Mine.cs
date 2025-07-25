using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name.Contains("Zombie"))
        {
            Debug.Log("��������!");
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
