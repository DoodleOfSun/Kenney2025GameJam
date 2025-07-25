using UnityEngine;
using System.Collections;

public class ZombieGenerating : MonoBehaviour
{
    public GameObject zombie;
    public float time;
    private Coroutine generatingCoroutine;
    public float unlockTime;
    public bool isFirstSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generatingCoroutine = null;
        if (generatingCoroutine == null && isFirstSpawn)
        {
            generatingCoroutine = StartCoroutine(GeneratingByTime(time));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (generatingCoroutine == null && Time.time >= unlockTime)
        {
            generatingCoroutine = StartCoroutine(GeneratingByTime(time));
        }
    }

    private IEnumerator GeneratingByTime(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(zombie, this.transform.position, this.transform.rotation);
        generatingCoroutine = null;
    }
}
