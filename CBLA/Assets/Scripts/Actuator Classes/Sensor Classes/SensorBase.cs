using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Generic Sensor", menuName = "Actuators/Sensors")]
public class SensorBase : ScriptableObject
{
    public int SensorIndex;
    [HideInInspector]
    public GameObject actor;
    public virtual float DoSense()
    {
        //sense a thing;
        float sensorOutput =0;
        return sensorOutput;
    }
}
