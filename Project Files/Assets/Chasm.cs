using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasm : MonoBehaviour
{

    public enum FallDirection { UpDown, LeftRight }

    [SerializeField]
    protected FallDirection fallDirection = FallDirection.LeftRight;

    public FallDirection _fallDirection { get{return fallDirection; } protected set{fallDirection = value;}}

    
}
