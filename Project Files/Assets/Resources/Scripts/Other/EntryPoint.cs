using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{

    public enum SpawnDirection { Above, Below, Left, Right };

    [SerializeField]
    [Tooltip("When the player travels to this location, where should they spawn relative to the EntryPoint")]
    public SpawnDirection spawnDirection = SpawnDirection.Below;

    [SerializeField]
    [Tooltip("How far away from the EntryPoint should the player spawn")]
    protected float spawnDistance = 1.2f;


    [SerializeField]
    [Tooltip("This is the name of the scene that the player should travel to")]
    protected string linkedScene = "";


    [SerializeField]
    [Tooltip("This is the name for the specific entrance in the linked scene where the player should travel to")]
    protected string linkedEntrance = "";


    public string _linkedEntrance { get { return linkedEntrance; } protected set { linkedEntrance = value; } }
    public string _linkedScene { get { return linkedScene; } protected set { linkedScene = value; } }
    public float _spawnDistance { get { return spawnDistance; } protected set { spawnDistance = value; } }
}
