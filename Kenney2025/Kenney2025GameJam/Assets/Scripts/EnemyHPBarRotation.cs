using UnityEngine;

public class EnemyHPBarRotation : MonoBehaviour
{

    private Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        transform.forward = cam.forward;
    }

}
