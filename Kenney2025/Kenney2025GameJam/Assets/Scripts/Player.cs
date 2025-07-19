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
                Debug.Log("발사");
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

        // 입력 받기
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 현재 위치
        Vector3 currentPosition = transform.position;

        // 목표 위치 계산 (이동 방향 비정규화 유지)
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        Vector3 targetPosition = currentPosition + moveDirection * speed * Time.fixedDeltaTime;

        // 부드럽게 목표 위치로 이동
        rb.MovePosition(Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.fixedDeltaTime));
    }


    private void LocomotionAnimation()
    {
        // WASD 입력값을 받아서 속도 계산
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float speedForAni = new Vector2(horizontal, vertical).magnitude;

        // Animator에 속도 전달
        animator.SetFloat("Speed", speedForAni);
    }

    private void LocomotionAnimStop()
    {
        animator.SetFloat("Speed", 0);
    }

    private void RotateByMouse()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 월드의 바닥면과 평면 정의 (Y축 기준)
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
        // 총 소리 필요함
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
