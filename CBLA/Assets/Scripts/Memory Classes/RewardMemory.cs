using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardMemory  {

    public Vector3 positionAtTime;
    public Vector3 rotationAtTime;
    public float reward;
    public int action;

    public RewardMemory(Vector3 newPositionAtTime, Vector3 newRotationAtTime, float newReward, int newAction)
    {
        positionAtTime = newPositionAtTime;
        rotationAtTime = newRotationAtTime;
        reward = newReward;
        action = newAction;
    }
}

