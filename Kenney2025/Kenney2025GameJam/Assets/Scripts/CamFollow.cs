using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour
{
    public static CamFollow instance;
    public GameObject target;
    public bool isShake;
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
        isShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShake)
        {
            transform.position = Random.insideUnitSphere * 1f + target.transform.position + targetChaseWindow;
        }
        else if(!isShake)
        {
            this.transform.position = target.transform.position + targetChaseWindow;
        }
    }

    public IEnumerator ShakeCam()
    {
        Debug.Log("카메라 흔들기 발동!");
        isShake = true;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("흔들기 종료!");
        transform.position = target.transform.position + targetChaseWindow;
        isShake = false;
    }
}