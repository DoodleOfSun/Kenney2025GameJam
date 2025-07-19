using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Zombie : MonoBehaviour
{
    public float attackDistance;
    public GameObject player;
    public GameObject attackBox;
    private NavMeshAgent nav;
    private Animator animator;
    private Coroutine attackCoroutine;
    private bool isAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackBox.SetActive(false);
        attackCoroutine = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 공격 범위 외일 경우 추격
        if (Vector3.Distance(transform.position, player.transform.position) > attackDistance)
        {

            animator.SetInteger("AttackType", 0);
            animator.SetFloat("Speed", nav.velocity.magnitude);
            nav.stoppingDistance = attackDistance;
            nav.destination = player.transform.position;
        }

        // 공격 범위 안일 경우 공격 애니메이션 재생
        else if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance 
            && attackCoroutine == null)
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
        yield return new WaitForSeconds(1.4f);
        attackBox.SetActive(false);
        animator.SetInteger("AttackType", 0);
        nav.isStopped = false;
        attackCoroutine = null;
    }

}