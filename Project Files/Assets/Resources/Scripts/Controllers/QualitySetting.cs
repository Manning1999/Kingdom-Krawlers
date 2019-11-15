using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySetting : MonoBehaviour
{
    [SerializeField]
    private int settingValue;

    public int _settingValue { get { return settingValue; } private set { settingValue = value; } }

}
