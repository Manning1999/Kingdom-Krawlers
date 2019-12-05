using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float timeTilDestroy = 2;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTimer());    
    }

   private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(timeTilDestroy);
        Destroy(gameObject);
    }
}
