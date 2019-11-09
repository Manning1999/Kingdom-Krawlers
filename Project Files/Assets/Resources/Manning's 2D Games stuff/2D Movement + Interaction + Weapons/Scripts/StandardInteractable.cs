using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StandardInteractable : MonoBehaviour, IInteractable
{


    public UnityEvent OnInteract;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }


    public virtual void InteractWith()
    {
        OnInteract.Invoke();
        Debug.Log("Interacted With");
    }
}
