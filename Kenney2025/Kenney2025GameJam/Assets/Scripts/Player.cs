using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public int power;

    public GameObject blaster;
    public GameObject bullet;
    public GameObject bulletCase;

    public float speed;
    private float speedOrigin;  // Saving Original Speed of Player. Do not Init or change that

    private Animator animator;

    private Rigidbody rb;

    private Coroutine fireCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        fireCoroutine = null;
        speedOrigin = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        MouseControl();
        RotateByMouse(); 
    }

    private void MouseControl()
    {
        
        if (Input.GetMouseButton(1))
        {
            LocomotionAnimStop();
            animator.SetBool("Aiming", true);

            speed = 0;
            if (Input.GetMouseButton(0) && fireCoroutine == null)
            {
                Debug.Log("�߻�");
                fireCoroutine = StartCoroutine(FiringCoroutine());
            }
        }

        else
        {
            animator.SetBool("Aiming", false);
            speed = speedOrigin;
            Moving();
        }
    }

    private void Moving()
    {
        LocomotionAnimation();

        // �Է� �ޱ�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ���� ��ġ
        Vector3 currentPosition = transform.position;

        // ��ǥ ��ġ ��� (�̵� ���� ������ȭ ����)
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        Vector3 targetPosition = currentPosition + moveDirection * speed * Time.fixedDeltaTime;

        // �ε巴�� ��ǥ ��ġ�� �̵�
        rb.MovePosition(Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.fixedDeltaTime));
    }


    private void LocomotionAnimation()
    {
        // WASD �Է°��� �޾Ƽ� �ӵ� ���
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float speedForAni = new Vector2(horizontal, vertical).magnitude;

        // Animator�� �ӵ� ����
        animator.SetFloat("Speed", speedForAni);
    }

    private void LocomotionAnimStop()
    {
        animator.SetFloat("Speed", 0);
    }

    private void RotateByMouse()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ������ �ٴڸ�� ��� ���� (Y�� ����)
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float enter;
        if (groundPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            transform.LookAt(hitPoint);
        }
    }

    public void Damaged()
    {
        if (power <= 0)
        {
            //empty
        }
        else
        {
            power--;
        }
    }

    private IEnumerator FiringCoroutine()
    {
        FireBulletByPower();
        yield return new WaitForSeconds(0.3f);
        FireBulletCase();
        // �� �Ҹ� �ʿ���
        yield return new WaitForSeconds(0.5f);
        fireCoroutine = null;
    }

    private void FireBulletByPower()
    {
        Instantiate(bullet, blaster.transform.position, Quaternion.identity);
    }

    private void FireBulletCase()
    {
        Instantiate(bulletCase, blaster.transform.position, Quaternion.identity);
    }
}
