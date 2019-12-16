using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiFollowMouse : MonoBehaviour
{
   // [SerializeField]
   // private RectTransform movingObject;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private GameObject basisObject;

    [SerializeField]
    private Canvas canvas = null;


    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

   

    public void MoveObject()
    {
         Vector2 pos;
         RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
          basisObject.transform.position = canvas.transform.TransformPoint(pos);

       // basisObject.transform.position = Camera.main.WorldToScreenPoint(Input.mousePosition);
    }

   
}
