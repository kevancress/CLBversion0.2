using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Generic Sensor", menuName = "Actuators/Sensors/Generic Sensor")]
public class SensorBase : ScriptableObject
{
    public string SensorName;
    [HideInInspector]
    public GameObject actor;
    
    public virtual float DoSense(GameObject actor)
    {
        float sensorOutput = 0;
        //sense a thing;
        return sensorOutput;

    }
}
