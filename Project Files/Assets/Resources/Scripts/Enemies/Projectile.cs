using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;

    [SerializeField]
    private int damage = 10;

    private Transform player;
    private Vector2 target;

    public float lifeDuration = 2f;
    private float lifeTimer;

    [SerializeField]
    private GameObject bloodParticles = null;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
     //       DestroyProjectile();
        }

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            DestroyProjectile();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        } */

        if(other.transform.GetComponent<IHurtable>() != null)
        {
            Debug.Log("Dealt damage");
            other.transform.GetComponent<IHurtable>().TakeDamage(damage);
        }
        Debug.Log(other.name);
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        GameObject blood = Instantiate(bloodParticles, transform.position, Quaternion.identity) as GameObject;
        blood.transform.position = transform.position;
        Destroy(gameObject);
    }
}
