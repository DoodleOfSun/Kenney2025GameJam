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
    public GameObject mine;

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

        if(!Input.GetMouseButton(1) && !Input.GetMouseButtonDown(0))
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
        Mine();
    }

    private void MouseControl()
    {
        
        if (Input.GetMouseButton(1))
        {
            LocomotionAnimStop();
            animator.SetBool("Aiming", true);
            if (Input.GetMouseButtonDown(0) && fireCoroutine == null)
            {
                fireCoroutine = StartCoroutine(FiringCoroutine());
            }
        }
    }

    private void Moving()
    {
        LocomotionAnimation();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        rb.AddForce(moveDirection * speed - rb.linearVelocity, ForceMode.VelocityChange);
    }


    private void LocomotionAnimation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float speedForAni = new Vector2(horizontal, vertical).magnitude;

        animator.SetFloat("Speed", speedForAni);
    }

    private void LocomotionAnimStop()
    {
        animator.SetFloat("Speed", 0);
    }

    private void RotateByMouse()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
            GameManagerForGameScene.instance.GameEnded();
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
        Instantiate(bulletCase, blaster.transform.position, Quaternion.identity);
    }

    public void PowerUp()
    {
        int randomWindow = Random.Range(3, 10);
        power += randomWindow;
    }

    private void Mine()
    {
        if (Input.GetButtonDown("Jump") && power >= 10)
        {
            Damaged();
            Instantiate(mine, transform.position, Quaternion.Euler(0, 0, 90));
        }
    }
}