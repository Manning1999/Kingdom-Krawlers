using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//suppresses warnings for unused variables
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

    //This is what the player's maximum health originally was before leveling up
    protected int baseHealth;

    double test = 0.05;

    [SerializeField]
    [Tooltip("This is what the player's current health is")]
    protected int currentHealth;
    public int _currentHealth { get { return currentHealth; } protected set { currentHealth = value; } }

    [SerializeField]
    [Tooltip("This is the health penalty that the player receives when they die")]
    protected int healthLostOnDeath = 10;

    protected bool isRespawning = false;
    protected bool beenPlaced = false;

    [Header("Dashing")]
    

    [SerializeField]
    [Tooltip("This is how long the player must wait between dashes")]
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

    [SerializeField]
    protected GameObject dashHitParticles;

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
    [Tooltip("This is a gameobject that the player will leave behind when they die. The dead body should have the DeadBody script attached")]
	protected GameObject deadBody;


    [Header("Leveling Stats")]
    protected int playerLevel = 1;
    public int _playerLevel { get { return playerLevel; } protected set { playerLevel = value; } }

    protected int experienceGained = 0;
    public int _experienceGained { get { return experienceGained; } protected set { experienceGained = value; } }

    [SerializeField]
    [Tooltip("This is how much experience the player needs to level up")]
    protected int experienceToLevelUp = 150;
    public int _experienceToLevelUp { get { return experienceToLevelUp; } protected set { experienceToLevelUp = value; } }

    [SerializeField]
    [Tooltip("This is how much ethe player's maximum health will increase by with each level")]
    protected int healthGainOnLevelUp = 10;

    [SerializeField]
    [Tooltip("This is how much the dash cooldown will decrease by with each level")]
    protected float dashTimerDecreaseOnLevel = 0.1f;

    [SerializeField]
    [Tooltip("This is how much extra damage the dash will do with each level")]
    protected int dashDamageIncreaseOnLevel = 2;

    [SerializeField]
    [Tooltip("If the player is set to dash in the direction of movement, how dfar should they dash")]
    protected float dashDistance = 4;

    [SerializeField]
    [Tooltip("This is the percentage that experience needed to level up will increase by")]
    protected int experienceRequiredPercentageIncrease = 12;


    [SerializeField]
    [Tooltip("This is the gameobject with the particles affects for levelling up")]
    protected GameObject levelUpEffects = null;


    [Header("Sounds")]
    [SerializeField]
    protected AudioClip levelUpSound;

    [SerializeField]
    [Tooltip("These are the sounds that the player will make when they dash into something")]
    protected List<AudioClip> hitSounds;


    [SerializeField]
    protected AudioClip lootCollectedSound;

    [SerializeField]
    protected AudioClip dashSound;

    [SerializeField]
    protected bool dashInMouseDirection = true;

    [SerializeField]
    protected Sword equippedSword;
    public Sword _equippedSword { get { return equippedSword; } }

    [SerializeField]
    protected Bow equippedBow;
    public Bow _equippedBow { get { return equippedBow; } }

    [SerializeField]
    protected GameObject bowSlot;
    public GameObject _bowSlot { get { return bowSlot; } }

    [SerializeField]
    protected GameObject swordSlot;
    public GameObject _swordSlot { get { return swordSlot; } }

   



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

        dashInMouseDirection = GameplaySettings.Instance._dashTowardsMousePosition;

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
        canDash = true;
        beenPlaced = false;
       
    }



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();



        if (Input.GetMouseButtonDown(0))
        {
            //use sword
            equippedSword.Use();
            equippedBow.Show(false);
            equippedSword.Show(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            //use bow
            equippedBow.Use();
            equippedBow.Show(true);
            equippedSword.Show(false);
            
        }
        

        //if the player is respawning or travelling between areas, this checks for whether they have been moved to the appropriate spot
        if (beenPlaced == false)
        {

            //if the player is respawning, keep trying to move them to the respawn point until they have been moved there.
            //The reason why it keeps trying is because not everything loads in a convenient order i.e. the player might load in before the
            //respawn point and that will cause an error because at that point there is no respawn point forthe player to be moved to
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
                //If the player is traveling, find the one that is linked to the one where the player travelled from and keep trying to move the player there until the player has been moved there.
                //This is done for the same reason as resurrection i.e. sometimes things don't load in a convenient order
                try
                {
                    foreach (EntryPoint possibleEntrance in FindObjectsOfType<EntryPoint>())
                    {
                        if (possibleEntrance.name == travelLocation)
                        {
                            //get which side of the entryPoint that the player is supposed to load on and move them there and then shift them by whatever distance is specified by the entry point
                            //This is done to prevent the player from loading on top of the entryPoint and being transported straight back to where they came from and causing an infinite loop of loading 
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
            transform.position = Vector3.LerpUnclamped(transform.position, dashLocation, dashSpeed * Time.deltaTime);

        }


        //if the player is near the dash destination then stop dashing (This is done to prevent a bug where if the player isn't EXACTLY at the dash location then it won't reset the isDashing variable)
        if(Vector3.Distance(transform.position, dashLocation) < 0.05f)
        {
            isDashing = false;
            canMove = true;
        }

        

        if (canDash == true)
        {


            if (dashInMouseDirection == true)
            {
                if (Input.GetMouseButtonDown(2))
                {
                    dashLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    SetDashLocation();
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log(moveDirection);
                    dashLocation = new Vector2(transform.position.x, transform.position.y) + (new Vector2(moveDirection.x, moveDirection.y) * dashDistance);
                    SetDashLocation();

                }
            }



            
        }


        if(isFalling == true)
        {
            canDash = false;
            transform.position = Vector3.Lerp(transform.position, fallPos, fallSpeed * Time.deltaTime);
        }


    }



    //This is a part of the IHurtable interface                                                                              //If in OnCollisionEnter2D or variation
    //From another script that can cause damage to the player use "PlayerController.Instance.TakeDamage(damage)"     or       "col.transform.GetComponent<IHurtable>().TakeDamage(damage)"
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    

   

     //Wait for the specified amount of time and then re-enable dashing
    protected IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashCoolDownTime);
        canDash = true;
        
    }





    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);

        

        //If the player is dashing and they collide with something that can be hurt, then hurt them and play the hit sound
        if (isDashing == true)
        {
            audio.clip = hitSounds[UnityEngine.Random.Range(0, hitSounds.Count)];
            if (col.transform.GetComponent<IHurtable>() != null)
            {
                
                col.transform.GetComponent<IHurtable>().TakeDamage(dashDamage);
            }
            dashHitParticles.transform.position = col.collider.ClosestPoint(transform.position);
            dashHitParticles.transform.GetComponent<Animator>().SetTrigger("Hit");
            canMove = true;
        }
       
        isDashing = false;
    }



    protected void OnTriggerEnter2D(Collider2D col)
    {

        //If the player walksover their corpse, destroy it, play the level up sound (which doubles as the corpse collection sound and remove the death penalty
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

        //if the player has entered a chasm and is NOT dashing then set them to faling, move them towards the chasm's center, play the falling animation and start the death timer
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

            GameObject fallenBody = Instantiate(deadBody, transform.position, Quaternion.identity);

            //instantiate a dead body and hide it so it doesn't show up immediately
            fallenBody.transform.GetComponent<DeadBody>().Hide();

        }
        

        //if the player moves into an entrypoint, travel to the relavent area
        if(col.transform.GetComponent<EntryPoint>() != null)
        {
            Travel(col);   
        }
    }





    protected void Travel(Collider2D col)
    {
        //if the entrypoint hasn't been set up yet then bring up an error message
        if (col.transform.GetComponent<EntryPoint>()._linkedScene.Equals("") || col.transform.GetComponent<EntryPoint>()._linkedEntrance.Equals(""))
        {
            Debug.Log("****************This doesn't lead anywhere yet!! Enter what scene it should load or where int he scene they should load****************");
        }
        else
        {
            //Set isRespawning to false so the game knows the player is traveling
            isRespawning = false;
            //load the relevant scene
            SceneManager.LoadScene(col.transform.GetComponent<EntryPoint>()._linkedScene);
            //find the linked EntryPoint
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


    //Do everything that should happen whent he player levels up
    [ContextMenu("Level Up")]
    protected void LevelUp()
    {
        //increase the player's level, increase their max health, restore their health
        playerLevel++;
        maxHealth += healthGainOnLevelUp;
        currentHealth = maxHealth;

        //reduce the dash cooldown timer
        dashCoolDownTime -= dashTimerDecreaseOnLevel;

        //Allow teh player to dash immediately
        canDash = true;

        //increase the amoubnt of damage dashing does
        dashDamage += dashDamageIncreaseOnLevel;

        //increase the amount of experience needed to level up
        experienceGained = experienceGained - experienceToLevelUp;
        experienceToLevelUp = experienceToLevelUp + (experienceToLevelUp / 100 * experienceRequiredPercentageIncrease);

        //play the level up affects
        levelUpEffects.transform.GetComponent<Animator>().SetTrigger("LevelUp");
        audio.clip = levelUpSound;
        audio.Play();
    }



    //Remove the health penalty that the player has recieved (called when the player walks over their corpse
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

        //disable the player's renderer and rigidbody
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<Rigidbody2D>().simulated = false;

        //instantiate the dead body for the player to collect
		Instantiate(deadBody, transform.position, Quaternion.identity);

    }

    public virtual void ChangeDashType(bool type)
    {
        dashInMouseDirection = type;
    }

    protected void SetDashLocation()
    {

        Vector2 direction = dashLocation - transform.position;
        float distance = Vector2.Distance(dashLocation, transform.position);

        //use a raycast to see if anything is in the way of the dash. If there is something in the way, then dash to that thing that is in the way
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


    public void EquipBow(Bow bow)
    {
        equippedBow = bow;
        equippedBow.transform.parent = bowSlot.transform;
    }

    public void EquipSword(Sword sword)
    {
        equippedSword = sword;
        equippedSword.transform.parent = swordSlot.transform;
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
