using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag.Contains("Player"))
        {
            Debug.Log("≈∫≈Î «√∑π¿ÃæÓ ¡¢√À«‘");
            Player.Instance.PowerUp();
            Destroy(gameObject);
        }
    }
}
