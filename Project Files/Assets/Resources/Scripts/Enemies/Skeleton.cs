using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Skeleton : EnemyIdentifier, IHurtable
{

   

    [SerializeField]
    private float speed;

    [SerializeField]
    private float detectionDistance = 3;

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

    [SerializeField]
    private int experienceGainedOnDeath = 10;


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

        if (Vector2.Distance(transform.position, player.position) < detectionDistance)
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
                // Instantiate(projectile, transform.position, Quaternion.identity);

                GameObject newArrow = Instantiate(projectile, transform.position + transform.right * 0.15f, Quaternion.identity) as GameObject;
                newArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90));
                


                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
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
        PlayerController.Instance.GainExperience(experienceGainedOnDeath);
        gameObject.SetActive(false);
    }
}
