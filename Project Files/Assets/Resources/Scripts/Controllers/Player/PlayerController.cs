using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable CS0168 // Variable is declared but never used

#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
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
    public int _currentHealth { get { return currentHealth; } protected set { currentHealth = value; } }

    [SerializeField]
    protected int healthLostOnDeath = 10;

    protected bool isRespawning = false;
    protected bool beenPlaced = false;

    [SerializeField]
    protected KeyCode dashButton = KeyCode.Space;

    [SerializeField]
    protected float dashCoolDownTime = 4;

    protected bool canDash = true;

    public bool _canDash { get { return canDash; } protected set { canDash = value; } }

    protected bool isDashing = false;

    [SerializeField]
    protected float dashSpeed = 7;

    [SerializeField]
    [Tooltip("When the player dashes and hits an enemy, how much damage should they deal to the enemy")]
    protected int dashDamage = 10;

    [SerializeField]
    [Tooltip("This should be all the layers that the player is NOT able to dash through")]
    protected LayerMask dashObstacles;

    protected Vector3 dashLocation;


    [SerializeField]
    [Tooltip("This should be the level where the player will respawn. Leave empty to respawn in the current level")]
    protected string levelToRespawnIn = "";


    protected string travelLocation = "";


    [SerializeField]
    [Tooltip("If the player has fallen into a chasm or ravine, how fast should they fall")]
    protected float fallSpeed = 3;

    protected bool isFalling = false;

    protected Vector3 fallPos;


	[SerializeField]
	protected GameObject deadBody;


    [Header("Levelling Stats")]
    protected int playerLevel = 1;
    public int _playerLevel { get { return playerLevel; } protected set { playerLevel = value; } }

    protected int experienceGained = 0;
    public int _experienceGained { get { return experienceGained; } protected set { experienceGained = value; } }

    [SerializeField]
    protected int experienceToLevelUp = 150;
    public int _experienceToLevelUp { get { return experienceToLevelUp; } protected set { experienceToLevelUp = value; } }

    [SerializeField]
    protected int healthGainOnLevelUp = 10;

    [SerializeField]
    protected float dashTimerDecreaseOnLevel = 0.1f;

    [SerializeField]
    protected int dashDamageIncreaseOnLevel = 2;

    [SerializeField]
    protected int experienceRequiredPercentageIncrease = 12;


    [SerializeField]
    protected GameObject levelUpEffects = null;


    [Header("Sounds")]

    [SerializeField]
    protected AudioClip levelUpSound;

    [SerializeField]
    protected List<AudioClip> hitSounds;


    [SerializeField]
    protected AudioClip lootCollectedSound;

    [SerializeField]
    protected AudioClip dashSound;



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
        audio = transform.GetComponent<AudioSource>();

        
        
    }


    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isFalling = false;
        canMove = true;
        transform.GetComponent<SpriteRenderer>().enabled = true;
        rb.simulated = true;
        anim.SetInteger("Direction", 4);
        Debug.Log("loaded");
        anim.ResetTrigger("Falling");

        beenPlaced = false;
       
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
                catch (Exception) { }
            }
            else
            {
                try
                {
                    foreach (EntryPoint possibleEntrance in FindObjectsOfType<EntryPoint>())
                    {
                        if (possibleEntrance.name == travelLocation)
                        {
                            switch (possibleEntrance.spawnDirection) {

                                case EntryPoint.SpawnDirection.Above:
                                transform.position = possibleEntrance.transform.position + transform.up * possibleEntrance._spawnDistance;
                                break;

                                case EntryPoint.SpawnDirection.Below:
                                    transform.position = possibleEntrance.transform.position + transform.up * -possibleEntrance._spawnDistance;
                                    break;

                                case EntryPoint.SpawnDirection.Right:
                                    transform.position = possibleEntrance.transform.position + transform.right * possibleEntrance._spawnDistance;
                                    break;

                                case EntryPoint.SpawnDirection.Left:
                                    transform.position = possibleEntrance.transform.position + transform.right * -possibleEntrance._spawnDistance;
                                    break;

                            }
                        }
                    }
                    
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



            Vector2 direction = dashLocation - transform.position;
            float distance = Vector2.Distance(dashLocation, transform.position);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, dashObstacles);

            if (hit.collider != null)
            {
                Debug.Log("Can't get through");
                dashLocation = hit.point;
            }
            else
            {
                Debug.Log("can get through");
            }



                dashLocation.z = transform.position.z;
            isDashing = true;
            canDash = false;
            StartCoroutine(DashTimer());
            audio.clip = dashSound;
            audio.Play();

        }


        if(isFalling == true)
        {
            canDash = false;
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
                audio.clip = hitSounds[UnityEngine.Random.Range(0, hitSounds.Count)];
                col.transform.GetComponent<IHurtable>().TakeDamage(dashDamage);
            }
        }

        isDashing = false;
    }



    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.GetComponent<DeadBody>() != null)
        {
            Destroy(col.gameObject);
            audio.clip = levelUpSound;
            audio.Play();
            LoseDeathPenalty();
        }
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
        

        //if the player moves into an entrypoint, travel to the relavent
        if(col.transform.GetComponent<EntryPoint>() != null)
        {
            Travel(col);   
        }
    }





    protected void Travel(Collider2D col)
    {
        if (col.transform.GetComponent<EntryPoint>()._linkedScene.Equals("") || col.transform.GetComponent<EntryPoint>()._linkedEntrance.Equals(""))
        {
            Debug.Log("****************This doesn't lead anywhere yet!! Enter what scene it should load****************");
        }
        else
        {
            isRespawning = false;
            SceneManager.LoadScene(col.transform.GetComponent<EntryPoint>()._linkedScene);
            travelLocation = col.transform.GetComponent<EntryPoint>()._linkedEntrance;
        }
    }




   


    //Call this function whenever an experience gaining activity is completed e.g. An enemy dying. 
    public void GainExperience(int experience)
    {
        experienceGained += experience;

        //if the player has gained enough experience to level up then call the level up functions
        if(experienceGained >= experienceToLevelUp)
        {
            LevelUp();
        }

        
    }



    [ContextMenu("Level Up")]
    protected void LevelUp()
    {
        playerLevel++;
        maxHealth += healthGainOnLevelUp;
        currentHealth = maxHealth;

        dashCoolDownTime -= dashTimerDecreaseOnLevel;
        canDash = true;

        dashDamage += dashDamageIncreaseOnLevel;


        experienceGained = experienceGained - experienceToLevelUp;
        experienceToLevelUp = experienceToLevelUp + (experienceToLevelUp / 100 * experienceRequiredPercentageIncrease);


        levelUpEffects.transform.GetComponent<Animator>().SetTrigger("LevelUp");
        audio.clip = levelUpSound;
        audio.Play();
    }




    protected void LoseDeathPenalty()
    {
        levelUpEffects.transform.GetComponent<Animator>().SetTrigger("LevelUp");
        maxHealth += healthLostOnDeath;
        
    }




   


    //context menu allows the user to right click on the script in the inspector and immediately execute this method
    [ContextMenu("Die")]
    protected virtual void Die()
    {
        Debug.Log("Player is now dead");
        //Death animation
        StartCoroutine(DeathTimer());
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<Rigidbody2D>().simulated = false;
		Instantiate(deadBody, transform.position, Quaternion.identity);

    }





    //How long the game should wait before respawning the player
    protected IEnumerator DeathTimer()
    {
        
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
        maxHealth -= healthLostOnDeath;
        currentHealth = maxHealth;


    }


#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
#pragma warning restore CS0168 // Variable is declared but never used
}
