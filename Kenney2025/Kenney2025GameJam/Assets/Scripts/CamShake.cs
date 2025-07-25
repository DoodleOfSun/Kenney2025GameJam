using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CamShake instance;
    private Vector3 originPos;
    private bool isShake;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        originPos = transform.position;
        isShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (shakeCoroutine == null)
        //{
        //    shakeCoroutine = StartCoroutine(ShakeCam());
        //}

        if (isShake)
        {
            transform.position = Random.insideUnitSphere * 1f + originPos;
        }
        else
        {
            transform.position = originPos;
        }
    }

    public IEnumerator ShakeCam()
    {
        isShake = true;
        yield return new WaitForSeconds(0.5f);
        transform.position = originPos; 
        isShake = false;
    }
}
