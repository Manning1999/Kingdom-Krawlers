using System;
using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ChestLootDropBehaviour : LootDropBehaviour, ICustomDescription
{

    
    protected enum DropDirection { Up, Down, Left, Right }

    [SerializeField]
    protected DropDirection dropDirection = DropDirection.Down;

    private bool isSpawningLoot = false;

    Transform droppedLoot = null;

    protected bool isOpen = false;

    [SerializeField]
    [Tooltip("This is how far away from the chest the loot to move")]
    protected float dropDistance = 0.2f;

    protected string originalLevel = "";
    public string _originalLevel { get { return originalLevel; } }


    public UnityEvent OnReset;

    protected override void Start()
    {
        base.Start();

        if(originalLevel.Equals(""))
        {
            originalLevel = SceneManager.GetActiveScene().name;
        }
        else
        {
            if (SceneManager.GetActiveScene().name == originalLevel)
            {

            }
            else
            {

            }
        }
       
    }

    protected override void Update()
    {
        base.Update();

        if(isSpawningLoot == true)
        {
            try
            {
                droppedLoot.transform.position = Vector3.MoveTowards(droppedLoot.transform.position, transform.position + transform.up * dropDistance, 0.01f * Time.deltaTime);
            }
            catch (Exception e) { }
        }
    }

    protected override void DropLoot(Loot lootToDrop)
    {

        if (isOpen == false)
        {
            isOpen = true;
            Debug.Log("Created a " + lootToDrop);
            //base.DropLoot(lootToDrop);
            isSpawningLoot = true;
            droppedLoot = (Transform)Instantiate(lootToDrop.transform, transform.position, Quaternion.identity) as Transform;
        }
       
    }

    public string description()
    {
        if (isOpen == false)
        {
            return "Loot Chest\nUnopened";
        }
        else
        {
            return "Loot Chest\nEmpty";
        }
    }

    public void Reset()
    {
        isOpen = false;
        OnReset.Invoke();
    }
}
