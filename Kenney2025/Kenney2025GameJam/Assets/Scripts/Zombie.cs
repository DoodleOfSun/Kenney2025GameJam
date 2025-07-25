using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class Zombie : MonoBehaviour
{
    public float attackDistance;
    public GameObject powerUp;
    public GameObject target;
    public GameObject attackBox;
    private NavMeshAgent nav;
    private Animator animator;
    private Coroutine attackCoroutine;
    private Coroutine dieCoroutine;
    public float hp;
    private float maxHP;
    public Slider hpBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Survivor_Player");
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackBox.SetActive(false);
        attackCoroutine = null;
        dieCoroutine = null;
        maxHP = hp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hpBar.gameObject.SetActive(GameManagerForGameScene.instance.enemyHPBarToggle.isOn);
        hpBar.value = (float)hp / (float)maxHP;

        if (hp <= 0 && dieCoroutine == null)
        {
            Debug.Log("좀비 체력이 다해 죽음");
            nav.isStopped = true;
            animator.SetTrigger("Die");
            dieCoroutine = StartCoroutine(Die());
        }


        // 공격 범위 외일 경우 추격
        if (Vector3.Distance(transform.position, target.transform.position) > attackDistance && !nav.isStopped)
        {
            animator.SetInteger("AttackType", 0);
            animator.SetFloat("Speed", nav.velocity.magnitude);
            nav.stoppingDistance = attackDistance;
            nav.destination = target.transform.position;
        }

        // 공격 범위 안일 경우 공격 애니메이션 재생
        else if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance 
            && attackCoroutine == null && attackBox != null)
        {
            nav.ResetPath();
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        int attackAni = Random.Range(1, 3);
        animator.SetInteger("AttackType", attackAni);
        nav.isStopped = true;
        attackBox.SetActive (true);
        yield return new WaitForSeconds(1.6f);

        if (attackBox != null)
        {
            attackBox.SetActive(false);
        }
        animator.SetInteger("AttackType", 0);
        nav.isStopped = false;
        attackCoroutine = null;
    }

    private IEnumerator Die()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        box.enabled = false;
        nav.isStopped = true;
        hpBar.enabled = false;
        //nav.enabled = false;
        GameManagerForGameScene.instance.killCount++;
        animator.SetTrigger("Die");
        Destroy(attackBox);
        yield return new WaitForSeconds(10f);
        RandomPowerUpDrop();
        Destroy(gameObject);
    }

    private void RandomPowerUpDrop()
    {
        int randomWindow = Random.Range (1, 3);
        if (randomWindow == 2)
        {
            Instantiate(powerUp, this.transform.position, Quaternion.identity);
        }
    }

    public void Damaged()
    {
        hp--;
    }

}