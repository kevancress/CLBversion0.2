using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Raycast Sensor", menuName = "Actuators/Sensors/RaycastSensor")]
public class RaycastSensor : SensorBase
{
    public float range;
    public string raycastSourceName;
    Transform raycastSource;
    public Color debugRayColor;

    public override float DoSense(GameObject actor)
    {
        raycastSource = actor.transform.Find(raycastSourceName);
        float sensorOputput;
        RaycastHit hit;
        
        if (Physics.Raycast(raycastSource.position, raycastSource.forward, out hit, range))
        {
            sensorOputput = (hit.distance / range);
        }
        else
        {
            sensorOputput = 0;
        }
        Debug.DrawRay(raycastSource.position, raycastSource.forward * hit.distance, debugRayColor, Time.deltaTime, true);
        return sensorOputput;

    }
}
