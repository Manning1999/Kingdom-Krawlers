//Description:
//This script is used for destructable objects that may have multiple phases to their destruction e.g. a bolder that gets broken down over time
//An animator on the object should be disabled at the start

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour, IHurtable
{

    [SerializeField]
    [Tooltip("TRUE: The object will use it's health as a basis for how destroyed it will look, this means that it can be destroyed faster or slower depending on how much damage is dealt to it.  FALSE: The object will look damage based on the number of times it has been hit rather than how much damage is dealth meaning that it will always take the same amount of time to destroy")]
    protected bool useHealth = false;

    [SerializeField]
    protected int health = 100;

    protected int baseHealth;

    [SerializeField]
    [Tooltip("These should be sprites that show different levels of destruction e.g. first one could be an intact boulder, 2nd boulder with cracks, 3rd, a boulder with more cracks and some broken off bits etc.")]
    protected List<Sprite> damageStates;

    protected int reachedState = 0;

    public UnityEvent OnDestroy;


    //used to automatically calculate what range of health each damage state would be
    protected List<int> damageStateRanges = new List<int>();


    //Components
    protected Animator anim;
    protected SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            anim = transform.GetComponent<Animator>();
        }
        catch (Exception e) { }


        rend = transform.GetComponent<SpriteRenderer>();
        baseHealth = health;


        //Calculate the damage ranges by dividing health by the number of damage states
        for(int i = damageStates.Count; i > 0; i--)
        {
            int numToAdd = Mathf.RoundToInt(health / damageStates.Count * i);
            damageStateRanges.Add(numToAdd);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {


        if(useHealth == true)
        {
            health -= damage;


            for (int i = 0; i < damageStateRanges.Count; i++) {

                if (health >= damageStateRanges[i])
                {
                    rend.sprite = damageStates[i];
                }
            }
            if(health <= 0)
            {
                Destruction();
            }
            
        }
        else
        {
            reachedState++;

            if (reachedState < damageStates.Count)
            {
                rend.sprite = damageStates[reachedState];
            }
            else
            {
                Destruction();
            }
        }
    }


    protected void Destruction()
    {
        try
        {
            anim.enabled = true;
            OnDestroy.Invoke();
        }
        catch (Exception e) { }
        StartCoroutine(DestroyTimer());
    }


    protected IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
