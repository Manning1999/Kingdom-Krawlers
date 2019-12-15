using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasm : MonoBehaviour
{

    public enum FallDirection { UpDown, LeftRight }

    [SerializeField]
    [Tooltip("This is the direction that the player will move in when they fall into the chasm")]
    protected FallDirection fallDirection = FallDirection.LeftRight;

    public FallDirection _fallDirection { get{return fallDirection; } protected set{fallDirection = value;}}

    
}
