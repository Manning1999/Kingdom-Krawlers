//Description:
//This script is able to be used in any 2D platformer, top down or 360 degree movement game
//It's animation system uses an integer that ranges from 0-8 for direction which means that the sprite can look in up to 8 directions (For less, just assign the closest number to that animation)
//1 = up, 2 = up-right, 3 = right, 4 = down-right, 5 = down, 6 = down-left, 7= left, 8 = up-left
//0 = don't animate direction

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class TwoDimensionalPlayerMovement : MonoBehaviour
{


    protected enum LookMode { Mouse, Movement }

    [SerializeField]
    [Tooltip("What should control where the player looks? MOUSE = the character will rotate to look towards the mouse position. MOVEMENT = the character will look in the direction that they are moving")]
    protected LookMode lookMode = LookMode.Movement;

    [SerializeField]
    [Tooltip("Should the sprite use the animator to display rotation or should the entire sprite be physically rotated")]
    protected bool animateRotation = false;

    protected enum MovementType { Platformer, FullMovement, FourDirectional }

    

    [SerializeField]
    [Tooltip("Platformer = mario like movement. FullMovement = can move in any direction, FourDirection = can only move up, down, left, right but NOT diagonally")]
    MovementType movementType = MovementType.FullMovement;


    //Only used for 4 directional movement
    protected enum Direction { Up, Down, Left, Right, Idle }

    [SerializeField]
    [Header("Platformer Options")]
    [Tooltip("Force with which the player will jump. ONLY APPLIES TO PLATFORMER GAMES")]
    protected float jumpForce = 300;

    [Tooltip("Set the gravity strength here INSTEAD of in the rigidbody to prevent conflicts")]
    protected float gravityStrength = 1;

    //if the player is on the ground
    protected bool isGrounded = false;

    [SerializeField]
    [Header("Full Movement Options")]
    protected Direction direction = Direction.Idle;


    protected float speedModifier = 1;
    public float _speedModifier { get { return speedModifier; } }

    [Header("Player Attributes")]
    [SerializeField]
    [Tooltip("How fast the character should move when walking")]
    protected float walkSpeed;

    [SerializeField]
    [Tooltip("Speed at which the player will run. SET TO 0 TO DISABLE RUNNING")]
    protected float runSpeed;

    protected bool isRunning = false;

    protected float currentMoveSpeed;


    [Header("Buttons")]
    [SerializeField]
    [Tooltip("used to move up in the fullmovement and four directional movement modes. Is used for the jump button in platformer mode")]
    protected KeyCode moveUpButton = KeyCode.W;
    [SerializeField]
    protected KeyCode moveDownButton = KeyCode.S;
    [SerializeField]
    protected KeyCode moveLeftButton = KeyCode.A;
    [SerializeField]
    protected KeyCode moveRightButton = KeyCode.D;

    [SerializeField]
    protected KeyCode runButton = KeyCode.LeftShift;



    [SerializeField]
    [Header("Sounds")]
    protected List<AudioClip> walkingSounds;






    //Components
    protected Rigidbody2D rb;
    protected Animator anim;
    protected AudioSource audio;



    //Don't touch
    protected Vector2 mousePos;
    protected Vector2 thisPos;
    protected bool isMoving = false;
    protected Vector3 oldPosition;
    protected bool canMove = true;


    protected Vector3 moveDirection;

    protected int moveDirectionNum;




    // Start is called before the first frame update
    protected virtual void Awake()
    {
        try
        {
            rb = transform.GetComponent<Rigidbody2D>();

            if(movementType != MovementType.Platformer)
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = gravityStrength;
            }
        }
        catch (Exception e)
        {
            
            Debug.Log("No Rigidbody2D attached");
        }

        try
        {
            audio = transform.GetComponent<AudioSource>();

           
        }
        catch (Exception e)
        {

            Debug.Log("No AudioSource attached");
        }



        try
        {
            anim = transform.GetComponent<Animator>();
        }
        catch (Exception e)
        {
            Debug.Log("No Animator Attached");
        }

        oldPosition = transform.position;
        canMove = true;    
    }




    // Update is called once per frame
    protected virtual void Update()
    {

        moveDirection = (transform.position - oldPosition).normalized;

        moveDirectionNum = anim.GetInteger("Direction");


        if(oldPosition != transform.position)
        {
            isMoving = true;
            anim.SetBool("isMoving", true);

        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", false);

        }
        oldPosition = transform.position;


        //if running is enabled then when the player presses the run button (LShift by default), set the "isRunning" variable to true
        if (runSpeed >= 0)
        {
            if (Input.GetKeyDown(runButton))
            {
                isRunning = true;
            }
            if (Input.GetKeyUp(runButton))
            {
                isRunning = false;
            }
        }




        //Chance the players current speed to the runspeed if isRunning is true
        if (isRunning == true)
        {
            currentMoveSpeed = runSpeed * speedModifier;
        }
        else
        {

            currentMoveSpeed = walkSpeed * speedModifier;
        }


        if (canMove == true)
        {
            //A switch for the different types of look modes (Looking at the mouse, or looking in the direction that the player is moving
            switch (lookMode)
            {

                //if the player is set to look where they are moving
                case LookMode.Movement:


                    //A switch that controls the movement type (full 360 degree movement, can only move in four directions or platformer movement)
                    switch (movementType)
                    {


                        case MovementType.FullMovement:
                            if (Input.GetKey(moveRightButton))
                            {

                                transform.position = new Vector3(transform.position.x + currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                                if (Input.GetKey(moveUpButton))
                                {
                                    anim.SetInteger("Direction", 2);

                                }
                                else if (Input.GetKey(moveDownButton))
                                {
                                    anim.SetInteger("Direction", 4);
                                }
                                else
                                {
                                    anim.SetInteger("Direction", 3);
                                }

                            }
                            if (Input.GetKey(moveLeftButton))
                            {

                                transform.position = new Vector3(transform.position.x - currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                                if (Input.GetKey(moveUpButton))
                                {
                                    anim.SetInteger("Direction", 8);
                                }
                                else if (Input.GetKey(moveDownButton))
                                {
                                    anim.SetInteger("Direction", 6);
                                }
                                else
                                {
                                    anim.SetInteger("Direction", 7);
                                }
                            }

                            if (Input.GetKey(moveUpButton))
                            {

                                transform.position = new Vector3(transform.position.x, transform.position.y + currentMoveSpeed * Time.deltaTime, transform.position.z);


                                if (Input.GetKey(moveRightButton))
                                {
                                    anim.SetInteger("Direction", 2);
                                }
                                else if (Input.GetKey(moveLeftButton))
                                {
                                    anim.SetInteger("Direction", 8);
                                }
                                else
                                {
                                    anim.SetInteger("Direction", 1);
                                }
                            }

                            if (Input.GetKey(moveDownButton))
                            {

                                transform.position = new Vector3(transform.position.x, transform.position.y - currentMoveSpeed * Time.deltaTime, transform.position.z);

                                if (Input.GetKey(moveRightButton))
                                {
                                    anim.SetInteger("Direction", 4);
                                }
                                else if (Input.GetKey(moveLeftButton))
                                {
                                    anim.SetInteger("Direction", 6);
                                }
                                else
                                {
                                    anim.SetInteger("Direction", 5);
                                }

                            }

                            if (!Input.GetKey(moveDownButton) && !Input.GetKey(moveUpButton) && !Input.GetKey(moveLeftButton) && !Input.GetKey(moveRightButton))
                            {

                                anim.SetInteger("Direction", anim.GetInteger("Direction"));

                            }
                            break;

                        case MovementType.FourDirectional:

                            if (Input.GetKey(moveRightButton))
                            {
                                direction = Direction.Right;
                                anim.SetInteger("Direction", 3);
                            }


                            if (Input.GetKey(moveLeftButton))
                            {
                                direction = Direction.Left;
                                anim.SetInteger("Direction", 7);
                            }


                            if (Input.GetKey(moveUpButton))
                            {
                                direction = Direction.Up;
                                anim.SetInteger("Direction", 1);
                            }


                            if (Input.GetKey(moveDownButton))
                            {
                                direction = Direction.Down;
                                anim.SetInteger("Direction", 5);
                            }
                            if (!Input.GetKey(moveDownButton) && !Input.GetKey(moveUpButton) && !Input.GetKey(moveLeftButton) && !Input.GetKey(moveRightButton))
                            {
                                direction = Direction.Idle;
                                anim.SetInteger("Direction", anim.GetInteger("Direction"));
                            }


                            switch (direction)
                            {
                                case Direction.Up:
                                    transform.position = new Vector3(transform.position.x, transform.position.y + currentMoveSpeed * Time.deltaTime, transform.position.z);
                                    break;
                                case Direction.Down:
                                    transform.position = new Vector3(transform.position.x, transform.position.y - currentMoveSpeed * Time.deltaTime, transform.position.z);
                                    break;
                                case Direction.Left:
                                    transform.position = new Vector3(transform.position.x - currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                                    break;
                                case Direction.Right:
                                    transform.position = new Vector3(transform.position.x + currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                                    break;
                            }
                            break;

                        case MovementType.Platformer:



                            if (Input.GetKey(moveRightButton))
                            {
                                transform.position = new Vector3(transform.position.x + currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                                anim.SetInteger("Direction", 3);
                            }

                            if (Input.GetKey(moveLeftButton))
                            {
                                transform.position = new Vector3(transform.position.x - currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                                anim.SetInteger("Direction", 6);
                            }

                            if (Input.GetKey(moveUpButton) && isGrounded == true)
                            {
                                isGrounded = false;
                                rb.AddForce(transform.up * jumpForce);
                                anim.SetInteger("Direction", 0);
                            }

                            if (Input.GetKeyUp(moveLeftButton) || Input.GetKeyUp(moveRightButton))
                            {
                                anim.SetInteger("Direction", 0);
                            }
                            break;


                    }
                    break;



                //if the player is set to look at where the mouse is then move in exactly the same way but set the look direction according to where the mousen is instead
                case LookMode.Mouse:

                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
                    float angleRadius = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
                    float angle = (180 / Mathf.PI) * angleRadius;
                    angle = (angle < 0) ? angle + 360 : angle;


                    switch (movementType)
                    {

                        case MovementType.FullMovement:


                            if (Input.GetKey(moveRightButton))
                            {
                                transform.position = new Vector3(transform.position.x + currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                            }
                            if (Input.GetKey(moveLeftButton))
                            {

                                transform.position = new Vector3(transform.position.x - currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                            }

                            if (Input.GetKey(moveUpButton))
                            {

                                transform.position = new Vector3(transform.position.x, transform.position.y + currentMoveSpeed * Time.deltaTime, transform.position.z);
                            }

                            if (Input.GetKey(moveDownButton))
                            {

                                transform.position = new Vector3(transform.position.x, transform.position.y - currentMoveSpeed * Time.deltaTime, transform.position.z);

                                if (Input.GetKey(moveRightButton))
                                {

                                }
                                else if (Input.GetKey(moveLeftButton))
                                {

                                }
                                else
                                {

                                }

                            }

                            if (!Input.GetKey(moveDownButton) && !Input.GetKey(moveUpButton) && !Input.GetKey(moveLeftButton) && !Input.GetKey(moveRightButton))
                            {



                            }



                            if (animateRotation == true)
                            {

                                //if the mouse y pos is lower than this y pos, then

                                if (mousePos.y < transform.position.y)
                                {
                                    angle = 180 + (180 - angle);
                                }


                                if (angle > 337.5f && angle < 22.5f) anim.SetInteger("Direction", 3);

                                if (angle > 22.5f && angle < 67.5f) anim.SetInteger("Direction", 2);

                                if (angle > 67.5f && angle < 112.5f) anim.SetInteger("Direction", 1);

                                if (angle > 112.5f && angle < 157.5f) anim.SetInteger("Direction", 8);

                                if (angle > 157.5f && angle < 202.5f) anim.SetInteger("Direction", 7);

                                if (angle > 202.5f && angle < 247.5f) anim.SetInteger("Direction", 6);

                                if (angle > 247.5f && angle < 292.5f) anim.SetInteger("Direction", 5);

                                if (angle > 292.5f && angle < 337.5f) anim.SetInteger("Direction", 4);
                            }
                            else
                            {

                                Vector3 mousePos3D = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);

                                transform.right = mousePos3D - transform.position;
                            }


                            break;

                        case MovementType.FourDirectional:



                            if (Input.GetKey(moveRightButton))
                            {
                                direction = Direction.Right;

                            }


                            if (Input.GetKey(moveLeftButton))
                            {
                                direction = Direction.Left;

                            }


                            if (Input.GetKey(moveUpButton))
                            {
                                direction = Direction.Up;

                            }


                            if (Input.GetKey(moveDownButton))
                            {
                                direction = Direction.Down;

                            }
                            if (!Input.GetKey(moveDownButton) && !Input.GetKey(moveUpButton) && !Input.GetKey(moveLeftButton) && !Input.GetKey(moveRightButton))
                            {
                                direction = Direction.Idle;

                            }


                            switch (direction)
                            {
                                case Direction.Up:
                                    transform.position = new Vector3(transform.position.x, transform.position.y + currentMoveSpeed * Time.deltaTime, transform.position.z);
                                    break;
                                case Direction.Down:
                                    transform.position = new Vector3(transform.position.x, transform.position.y - currentMoveSpeed * Time.deltaTime, transform.position.z);
                                    break;
                                case Direction.Left:
                                    transform.position = new Vector3(transform.position.x - currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                                    break;
                                case Direction.Right:
                                    transform.position = new Vector3(transform.position.x + currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                                    break;
                            }







                            if (animateRotation == true)
                            {
                                mousePos = new Vector2(Camera.main.ViewportToWorldPoint(Input.mousePosition).x, Camera.main.ViewportToWorldPoint(Input.mousePosition).y);
                                thisPos = new Vector2(transform.position.x, transform.position.y);




                                if (angle > 315 || angle < 45) anim.SetInteger("Direction", 3);

                                if (angle > 45 && angle < 135) anim.SetInteger("Direction", 1);

                                if (angle > 135 && angle < 225) anim.SetInteger("Direction", 7);

                                if (angle > 225 && angle < 315) anim.SetInteger("Direction", 5);


                                /*if (angle > 45 && angle < 135 ) anim.SetInteger("Direction", 1);

                                if (angle > 135 && angle < -135) anim.SetInteger("Direction", 7);

                                if (angle > 45 && angle < -45) anim.SetInteger("Direction", 3);

                                if (angle < -45 && angle > -135) anim.SetInteger("Direction", 5);*/




                            }
                            else
                            {

                                Vector3 mousePos3D = new Vector3(Camera.main.ViewportToWorldPoint(Input.mousePosition).x, Camera.main.ViewportToWorldPoint(Input.mousePosition).y, transform.position.z);

                                transform.right = mousePos3D - transform.position;
                            }



                            break;

                        case MovementType.Platformer:



                            if (Input.GetKey(moveRightButton))
                            {
                                transform.position = new Vector3(transform.position.x + currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                            }

                            if (Input.GetKey(moveLeftButton))
                            {
                                transform.position = new Vector3(transform.position.x - currentMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

                            }

                            if (Input.GetKey(moveUpButton) && isGrounded == true)
                            {
                                isGrounded = false;
                                rb.AddForce(transform.up * jumpForce);

                            }


                            break;


                    }
                    break;
            }
        }

    }

    
    protected virtual void OnCollisionEnter2D(Collision2D col)
    {

        switch (movementType)
        {

            case MovementType.Platformer:
                if (col.transform.tag.Equals("Ground"))
                {
                   
                    isGrounded = true;
                }
                break;

            case MovementType.FullMovement:
                break;

            case MovementType.FourDirectional:
                break;
        }

    }



    public virtual void ChangeSpeedModifier(float modifier)
    {
        speedModifier = modifier;
    }

}
