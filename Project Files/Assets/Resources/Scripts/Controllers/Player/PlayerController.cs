using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : TwoDimensionalPlayerMovement, IHurtable
{

    [Header("Kingdom Krawler Player Attributes")]
    [SerializeField]
    protected int maxHealth;
    public int _maxHealth { get { return maxHealth; } protected set { maxHealth = value; } }

    protected int baseHealth;

    [SerializeField]
    protected int currentHealth;
    public int _health { get { return currentHealth; } protected set { currentHealth = value; } }




    [SerializeField]
    protected KeyCode dashButton = KeyCode.Space;

    [SerializeField]
    protected float dashCoolDownTime = 4;

    protected bool canDash = true;

    protected bool isDashing = false;

    [SerializeField]
    protected float dashSpeed = 7;

    [SerializeField]
    protected int dashDamage = 10;

    protected Vector3 dashLocation;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        baseHealth = maxHealth;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        

        if(isDashing == true)
        {
            transform.position = Vector3.Lerp(transform.position, dashLocation, dashSpeed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, dashLocation) < 0.3f)
        {
            isDashing = false;
        }

        if (Input.GetKeyDown(dashButton) && canDash == true)
        {
            dashLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dashLocation.z = transform.position.z;
            isDashing = true;
            canDash = false;
            StartCoroutine(DashTimer());

        }


    }



    //This is a part of the IHurtable interface
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }



   


    protected IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashCoolDownTime);
        canDash = true;
    }



    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);

        isDashing = false;

        if(col.transform.GetComponent<IHurtable>() != null)
        {
            col.transform.GetComponent<IHurtable>().TakeDamage(dashDamage);
        }
    }



    protected virtual void Die()
    {
        Debug.Log("Player is now dead");

    }
}
