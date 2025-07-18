using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject target;

    public Vector3 targetChaseWindow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position + targetChaseWindow;    
    }
}