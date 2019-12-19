using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : EnemyIdentifier, IHurtable
{

    private Rigidbody2D myRigidbody;

    private bool moving;
    public float moveSpeed;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;
 
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range;

    [SerializeField]
    private int damage = 10;

    [SerializeField]
    private float timeBetweenAttacks = 1.3f;

    private bool canAttack = true;

    public UnityEvent OnDie;

    [SerializeField]
    private int experienceGainedOnDeath = 10;

    [SerializeField]
    private GameObject bloodParticles;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        


        myRigidbody = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            try
            {
                target = PlayerController.Instance.transform;
            }
            catch (Exception e) { }

            if (moving)
            {
                timeToMoveCounter -= Time.deltaTime;
                myRigidbody.velocity = moveDirection;

                if (timeToMoveCounter < 0f)
                {
                    moving = false;
                    timeBetweenMoveCounter = timeBetweenMove;
                }
            }
            else
            {
                timeBetweenMoveCounter -= Time.deltaTime;
                myRigidbody.velocity = Vector2.zero;

                if (timeBetweenMoveCounter < 0f)
                {
                    moving = true;
                    timeToMoveCounter = timeToMove;

                    moveDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f) * UnityEngine.Random.Range(-1f, 1f) * moveSpeed, 0f);
                }
            }

            if (Vector3.Distance(target.position, transform.position) <= range)
            {
                FollowPlayer();
            }

        }

    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taken damage");
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    [ContextMenu("Kill")]
    private void Die()
    {
        Debug.Log("Died");
        OnDie.Invoke();
        PlayerController.Instance.GainExperience(experienceGainedOnDeath);
        gameObject.SetActive(false);
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.GetComponent<IHurtable>() != null && other.transform.GetComponent<Destructable>() == null && other.transform.GetComponent<EnemyIdentifier>() == null)
        {
            if (canAttack == true)
            {
                Debug.Log(transform.name + "   Dealt damage to " + other.transform.name);
                other.transform.GetComponent<IHurtable>().TakeDamage(damage);
                canAttack = false;
                Instantiate(bloodParticles, other.transform.position, Quaternion.identity);

                //In a try catch statement to prevent errors if the enemy is trying to attack while it is dying
                try
                {
                    StartCoroutine(AttackTimer());
                }
                catch (Exception e) { }
            }
        }
    }


    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }


}
