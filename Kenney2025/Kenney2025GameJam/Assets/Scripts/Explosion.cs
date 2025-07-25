using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(explosion());
    }

    private IEnumerator explosion()
    {
        AudioManager.instance.MineExplosion();
        StartCoroutine(CamFollow.instance.ShakeCam());
        yield return new WaitForSeconds(0.51f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            Player.Instance.Damaged();
        }

        else if (other.name.Contains("Zombie"))
        {
            Debug.Log("좀비 데미지!");
            Zombie zombie = other.GetComponent<Zombie>();
            for (int i = 0; i < 10; i++)
            {
                zombie.Damaged();
            }
        }
    }
}
