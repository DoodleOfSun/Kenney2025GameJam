using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour
{
    public static CamFollow instance;
    public GameObject target;

    private Coroutine shakeCoroutine;
    private bool isShake;
    public Vector3 targetChaseWindow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        shakeCoroutine = null;
        isShake = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (isShake)
        {
            transform.position = Random.insideUnitSphere * 1f + target.transform.position + targetChaseWindow;
        }
        else
        {
            this.transform.position = target.transform.position + targetChaseWindow;
        }
  
    }


    public IEnumerator ShakeCam()
    {
        isShake = true;
        yield return new WaitForSeconds(0.5f);
        transform.position = target.transform.position + targetChaseWindow;
        isShake = false;
        shakeCoroutine = null;
    }
}