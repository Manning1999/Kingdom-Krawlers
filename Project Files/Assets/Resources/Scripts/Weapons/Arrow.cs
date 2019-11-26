using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField]
    private float arrowSpeed;

    [SerializeField]
    private int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * arrowSpeed * Time.deltaTime;   
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.GetComponent<IHurtable>() != null)
        {
            col.transform.GetComponent<IHurtable>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
