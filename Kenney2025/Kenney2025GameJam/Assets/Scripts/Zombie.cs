using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class Zombie : MonoBehaviour
{
    public float attackDistance;
    public GameObject powerUp;
    public GameObject player;
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
            Debug.Log("���� ü���� ���� ����");
            nav.isStopped = true;
            animator.SetTrigger("Die");
            dieCoroutine = StartCoroutine(Die());
        }


        // ���� ���� ���� ��� �߰�
        if (Vector3.Distance(transform.position, player.transform.position) > attackDistance && !nav.isStopped)
        {
            nav.ResetPath();
            animator.SetInteger("AttackType", 0);
            animator.SetFloat("Speed", nav.velocity.magnitude);
            nav.stoppingDistance = attackDistance;
            nav.destination = player.transform.position;
        }

        // ���� ���� ���� ��� ���� �ִϸ��̼� ���
        else if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance 
            && attackCoroutine == null && attackBox != null)
        {
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
        nav.isStopped = true;
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