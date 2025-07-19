using UnityEngine;

public class ZombieAttackBox : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BoxCollider>(out var box) && other.tag.Contains("Player"))
        {
            Debug.Log("플레이어 감지됨! (트리거)");
            Player.Instance.Damaged();
        }
    }
}
