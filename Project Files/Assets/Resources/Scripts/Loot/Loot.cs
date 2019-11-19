using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    [SerializeField]
    protected Sprite icon = null;

    [SerializeField]
    protected new string name;
    public string _name { get { return name; } }

    [SerializeField]
    protected int value;
    public string _value { get { return name; } }

    [SerializeField]
    [TextArea(2,5)]
    protected string description;
    public string _description { get { return description; } }


    protected bool inInventory = false;
    protected bool statsDecided = false;


    //Components
    protected SpriteRenderer rend;
    protected Animator anim;


    [SerializeField]
    protected GameObject owner = null;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (statsDecided == false)
        {
            Generate();
        }


        rend = transform.GetComponent<SpriteRenderer>();
        rend.sprite = icon;
        anim = transform.GetComponent<Animator>();
    }

    protected virtual void Awake()
    {

        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    protected virtual void Update()
    {
    
        
       

    }

    public virtual void Generate()
    {
        Debug.Log("The loot is being geenrated now");
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {

        if(inInventory == false)
        {
            if(col.transform.GetComponent<PlayerController>() != null)
            {
                Debug.Log("Should be adding now");
                AddToInventory();
                
            }
        }
    }



    public virtual void AddToInventory()
    {
        Debug.Log(InventoryController.Instance.AddToInventory(gameObject));
        if (InventoryController.Instance.AddToInventory(gameObject) == true)
        {
            anim.SetTrigger("Collect");
            inInventory = true;
        }

        owner = PlayerController.Instance.gameObject;
        transform.GetComponent<Collider2D>().enabled = false;
    }



    public virtual void RemoveFromInventory()
    {

    }



    public virtual void Sell()
    {
            
    }

    



   

    public virtual void Use()
    {
        Debug.Log("This object has been used");
    }
}
