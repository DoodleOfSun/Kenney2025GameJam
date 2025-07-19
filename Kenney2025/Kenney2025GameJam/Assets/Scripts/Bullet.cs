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
        Debug.Log("źȯ ������");
        StartCoroutine(DestroyByTime());
        isHit = false;


        // ���� ����: ������
        Vector3 forward = Player.Instance.transform.forward;

        // �¿� ���� ����
        Vector3 right = Player.Instance.transform.right;

        // �¿� ���� ��
        float spreadRange = 0.2f;

        // x�� �������� ���� offset �߰�
        Vector3 sideOffset = right * Random.Range(-spreadRange, spreadRange);

        // ���� ���� (����ȭ X!)
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
            Debug.Log("�浹�� ������Ʈ �̸�: " + other.name + ", �±�: " + other.tag);


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