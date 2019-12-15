using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Skeleton : EnemyIdentifier, IHurtable
{

   

    [SerializeField]
    private float speed;

    [SerializeField]
    private float stoppingDistance;

    [SerializeField]
    private float retreatDistance;

    [SerializeField]
    private float timeBtwShots;

    [SerializeField]
    private float startTimeBtwShots;


    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform player;


    public UnityEvent OnDie;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); 

        if (player == null)
        {
            // player = GameObject.FindGameObjectWithTag("Player").transform;
            player = PlayerController.Instance.transform;
        }

        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        OnDie.Invoke();
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
