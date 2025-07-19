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

    public GameObject resultPanel;

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
        resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if(!Input.GetMouseButton(1))
        {
            animator.SetBool("Aiming", false);
            speed = speedOrigin;
            Moving();
        }
    }

    void Update()
    {
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
            if (Input.GetMouseButtonDown(0) && fireCoroutine == null)
            {
                Debug.Log("�߻�");
                fireCoroutine = StartCoroutine(FiringCoroutine());
            }
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
            power = 0;
            Time.timeScale = 0f;
            resultPanel.SetActive(true);
            // ���ӿ���
        }
        else
        {
            power = power - 10;
            if (power < 0)
            {
                power = 0;
            }
        }
    }

    private IEnumerator FiringCoroutine()
    {
        FireBulletByPower();
        AudioManager.instance.ShotgunFire();
        yield return new WaitForSeconds(0.7f);
        FireBulletCase();
        AudioManager.instance.ShotgunPumping();
        StartCoroutine(AudioManager.instance.ShotgunShallDrop());

        yield return new WaitForSeconds(0.5f);
        Debug.Log("���� �غ��");
        fireCoroutine = null;
    }

    private void FireBulletByPower()
    {
        if (power <= 0)
        {
            Instantiate(bullet, blaster.transform.position, Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < power; i++)
            {
                Instantiate(bullet, blaster.transform.position, Quaternion.identity);
            }
        }
    }

    private void FireBulletCase()
    {
        Debug.Log("ź�� ������");
        Instantiate(bulletCase, blaster.transform.position, Quaternion.identity);
    }

    public void PowerUp()
    {
        int randomWindow = Random.Range(1, 10);
        power += randomWindow;
    }
}