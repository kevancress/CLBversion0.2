using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardMemory : Memory  {

    public float reward;
    public int action;

    public RewardMemory(float newReward, int newAction)
    {
        reward = newReward;
        action = newAction;
    }
}

