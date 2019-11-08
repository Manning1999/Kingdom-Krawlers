using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : TwoDimensionalPlayerMovement, IHurtable
{


    //create singleton
    public static PlayerController instance;
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerController>();
            }

            return _instance;
        }
    }



    [Header("Kingdom Krawlers Player Attributes")]
    [SerializeField]
    protected int maxHealth;
    public int _maxHealth { get { return maxHealth; } protected set { maxHealth = value; } }

    protected int baseHealth;

    [SerializeField]
    protected int currentHealth;
    public int _health { get { return currentHealth; } protected set { currentHealth = value; } }

    [SerializeField]
    protected int healthLostOnDeath = 10;

    protected bool isRespawning = false;
    protected bool beenPlaced = false;

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


    [SerializeField]
    [Tooltip("This should be the level where the player will respawn. Leave empty to respawn in the current level")]
    protected string levelToRespawnIn = "";


    [SerializeField]
    [Tooltip("If the player has fallen into a chasm or ravine, how fast should they fall")]
    protected float fallSpeed = 3;

    protected bool isFalling = false;

    protected Vector3 fallPos;



    protected static PlayerController playerInstance;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);


        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    protected void Start()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
        baseHealth = maxHealth;

        
        
    }


    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isFalling = false;
        canMove = true;
        anim.SetInteger("Direction", 4);
        Debug.Log("loaded");
        anim.ResetTrigger("Falling");

        beenPlaced = false;
       // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();



        if (beenPlaced == false)
        {
            if (isRespawning == true)
            {
                try
                {
                    transform.position = RespawnPoint.Instance.transform.position;
                    isRespawning = false;
                    beenPlaced = true;
                }
                catch (Exception e) { }
            }
            else
            {
                try
                {
                    transform.position = EntryPoint.Instance.transform.position;
                    beenPlaced = true;
                }
                catch (Exception e) { }
            }
        }


       

        //if the player is in the process of dashing then move them from their current location to the mouse's location at the specified speed
        if (isDashing == true)
        {
            transform.position = Vector3.Lerp(transform.position, dashLocation, dashSpeed * Time.deltaTime);

        }


        //if the player is near the dash destination then stop dashing (This is done to prevent a bug where if the player isn't EXACTLY at the dash location then it won't reset the isDashing variable)
        if(Vector3.Distance(transform.position, dashLocation) < 0.2f)
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


        if(isFalling == true)
        {
            
            transform.position = Vector3.Lerp(transform.position, fallPos, fallSpeed * Time.deltaTime);
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

        


        if (isDashing == true)
        {
            if (col.transform.GetComponent<IHurtable>() != null)
            {
                col.transform.GetComponent<IHurtable>().TakeDamage(dashDamage);
            }
        }

        isDashing = false;
    }




    protected void OnTriggerStay2D(Collider2D col)
    {

        //if the player has entered a chasm
        if(col.transform.GetComponent<Chasm>() != null && isDashing == false && isFalling == false)
        {
            isFalling = true;


            //change where the player will fall towards depending on whether the chasm is wide or long
            if (col.transform.GetComponent<Chasm>()._fallDirection == Chasm.FallDirection.UpDown)
            {
                fallPos = new Vector3(transform.position.x, col.transform.position.y, transform.position.z);
            }
            else
            {
                fallPos = new Vector3(col.transform.position.x, transform.position.y, transform.position.z);
            }


            //prevent the player from controlling movement and then player the falling animation adn start the death timer to give the animation time to play
            canMove = false;
            anim.SetTrigger("Falling");
            anim.SetInteger("Direction", 0);
            StartCoroutine(DeathTimer());
        }
        

        if(col.transform.GetComponent<EntryPoint>() != null)
        {
            if (levelToRespawnIn.Equals(""))
            {
                Debug.Log("****************This doesn't lead anywhere yet!! Enter what scene it should load****************");
            }
            else
            {
                isRespawning = false;
                SceneManager.LoadScene(col.transform.GetComponent<EntryPoint>()._linkedScene);

            }
        }
    }




    //context menu allows the user to right click on the script in the inspector and immediately execute this method
    [ContextMenu("Die")]
    protected virtual void Die()
    {
        Debug.Log("Player is now dead");
        //Death animation
        StartCoroutine(DeathTimer());

    }


    protected IEnumerator DeathTimer()
    {
        maxHealth -= healthLostOnDeath;
        currentHealth = maxHealth;
        yield return new WaitForSeconds(5f);
        //Bring up death menu 
        //for testing purposes it will automatically be set to respawn
        isRespawning = true;
        if (levelToRespawnIn.Equals(""))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(levelToRespawnIn);
            
        }
        isFalling = false;
       

    }
}
