using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject spark;
    private Rigidbody rb;
    private bool isHit;

    private Vector3 shootDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float angle = 90f;
        transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        rb = GetComponent<Rigidbody>();
        Debug.Log("탄환 생성됨");
        StartCoroutine(DestroyByTime());
        isHit = false;


        // 기준 방향: 앞으로
        Vector3 forward = Player.Instance.transform.forward;

        // 좌우 방향 기준
        Vector3 right = Player.Instance.transform.right;

        // 좌우 오차 값
        float spreadRange = 0.2f;

        // x축 방향으로 랜덤 offset 추가
        Vector3 sideOffset = right * Random.Range(-spreadRange, spreadRange);

        // 최종 방향 (정규화 X!)
        shootDirection = forward + sideOffset;

    }

    void FixedUpdate()
    {
        rb.AddForce(shootDirection * bulletSpeed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Zombie") && !other.name.Contains("Attack") && !isHit && !other.tag.Contains("Body"))
        {
            isHit = true;
            Debug.Log("충돌한 오브젝트 이름: " + other.name + ", 태그: " + other.tag);


            Zombie zombie = other.GetComponent<Zombie>();

            zombie.Damaged();

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