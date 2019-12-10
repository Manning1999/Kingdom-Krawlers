using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiFollowMouse : MonoBehaviour
{
    public RectTransform movingObject;
    public Vector3 offset;
    public RectTransform basisObject;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = 0;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }
}
