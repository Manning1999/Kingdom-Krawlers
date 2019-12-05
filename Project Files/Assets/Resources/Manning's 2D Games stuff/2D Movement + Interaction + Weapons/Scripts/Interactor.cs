using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{


    [SerializeField]
    protected List<GameObject> listOfInteractables = new List<GameObject>();

    [SerializeField]
    protected GameObject objectToInteractWith;

    [SerializeField]
    protected GameObject interactionIcon = null;

    [SerializeField]
    protected TMPro.TextMeshProUGUI interactionIconText = null;

    [SerializeField]
    protected bool showIconAtObject = true;

    [SerializeField]
    protected bool showNameOfInteractable = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        

            Vector3 mousePos3D = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);

            transform.right = mousePos3D - transform.position;


        
        if(objectToInteractWith != null)
        {
            if (Input.GetKeyDown("e"))
            {
                objectToInteractWith.transform.GetComponent<IInteractable>().InteractWith();
            }
            if (showIconAtObject == true)
            {
                try
                {
                    interactionIcon.transform.position = Camera.main.WorldToScreenPoint(objectToInteractWith.transform.position);
                }
                catch(Exception e)
                {
                    interactionIcon.SetActive(false);
                }
            }
        }

    }

    //if an interactable object enters the trigger zone, add it to the list of interactables
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.transform.GetComponent<IInteractable>() != null)
        {
            if (!listOfInteractables.Contains(col.gameObject))
            {
                Debug.Log("Something can be interacted with now");
                listOfInteractables.Add(col.gameObject);
                CheckList();
            }
        }
    }


    //if an interactable object leaves the trigger zone then remove it from the list of interactables
    private void OnTriggerExit2D(Collider2D col)
    {


        if (col.transform.GetComponent<IInteractable>() != null)
        {
            if (listOfInteractables.Contains(col.gameObject))
            {
                Debug.Log("Something cannot be interacted with now");
                listOfInteractables.Remove(col.gameObject);
                CheckList();
            }
        }
    }




    //check what is closest to the player out of the things in the list of interactables
    private void CheckList()
    {


        if (listOfInteractables.Count > 0)
        {
            objectToInteractWith = listOfInteractables[0];

            foreach (GameObject interaction in listOfInteractables)
            {
                Vector2 previousPos2D = new Vector2(objectToInteractWith.transform.position.x, objectToInteractWith.transform.position.y);
                Vector2 otherPos2D = new Vector2(interaction.transform.position.x, interaction.transform.position.y);
                Vector2 thisPos2D = new Vector2(transform.position.x, transform.position.y);

                if (Vector2.Distance(otherPos2D, thisPos2D) < Vector2.Distance(previousPos2D, thisPos2D))
                {
                    objectToInteractWith = interaction;
                }


            }
            if (interactionIcon.activeSelf == false)
            {
                
                if(showNameOfInteractable == true)
                {
                    interactionIconText.text = objectToInteractWith.name;
                }
                interactionIcon.SetActive(true);

            }
        }
        else
        {
            if (interactionIcon.activeSelf == true)
            {
                interactionIcon.SetActive(false);
            }
            objectToInteractWith = null;
        }
    }
}
