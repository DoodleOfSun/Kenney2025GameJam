using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag.Contains("Player"))
        {
            Debug.Log("ź�� �÷��̾� ������");
            Player.Instance.PowerUp();
            Destroy(gameObject);
        }
    }
}
