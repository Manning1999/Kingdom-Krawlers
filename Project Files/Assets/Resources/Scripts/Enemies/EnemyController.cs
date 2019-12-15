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
 
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range;

    [SerializeField]
    private int damage = 10;



    public UnityEvent OnDie;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        target = FindObjectOfType<PlayerController>().transform;
        Debug.Log(target);

        myRigidbody = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = moveDirection;

            if(timeToMoveCounter < 0f)
            {
                moving = false;
                timeBetweenMoveCounter = timeBetweenMove;
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;

            if(timeBetweenMoveCounter < 0f)
            {
                moving = true;
                timeToMoveCounter = timeToMove;

                moveDirection = new Vector3(Random.Range(-1f, 1f) * Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }

        if (Vector3.Distance(target.position, transform.position) <= range)
        {
            FollowPlayer();
        }
        
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    [ContextMenu("Kill")]
    private void Die()
    {
        OnDie.Invoke();
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<IHurtable>() != null)
        {
            Debug.Log("Dealt damage");
            other.transform.GetComponent<IHurtable>().TakeDamage(damage);
        }
    }

}
