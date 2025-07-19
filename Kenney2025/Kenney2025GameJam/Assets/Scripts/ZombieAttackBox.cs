using UnityEngine;

public class ZombieAttackBox : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BoxCollider>(out var box) && other.tag.Contains("Player"))
        {
            Debug.Log("�÷��̾� ������! (Ʈ����)");
            Player.Instance.Damaged();
        }
    }
}
