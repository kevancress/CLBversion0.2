using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensoryMotorMemory:Memory {

    public double sensoryState;
    public double actionTaken;

    public SensoryMotorMemory(double newSensoryState, double newActionTaken )
    {
        sensoryState = newSensoryState;
        actionTaken = newActionTaken;
    }
}
