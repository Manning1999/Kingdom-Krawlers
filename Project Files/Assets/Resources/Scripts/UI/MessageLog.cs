using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageLog : MonoBehaviour
{


    //create singleton
    public static MessageLog instance;
    private static MessageLog _instance;

    public static MessageLog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MessageLog>();
            }

            return _instance;
        }
    }


    private TextMeshProUGUI textComponent;
    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        textComponent = transform.GetComponent<TextMeshProUGUI>();
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateLog(string text)
    {
        textComponent.text = text;
        anim.SetTrigger("ShowMessage");
    }
}
