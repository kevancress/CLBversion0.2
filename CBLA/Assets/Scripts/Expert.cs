using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expert : MonoBehaviour {
    public GameObject CLAB;
    public float Error;
    public List<Memory> memory = new List<Memory>();
    public List<RewardMemory> rMemory = new List<RewardMemory>();
    void Start () {
        CLAB = GameObject.Find("CLAB");
	}

	
    public void AddToMemory (GameObject CLAB)
    {
        Vector3 PatT = CLAB.transform.position;
        Vector3 RatT = CLAB.transform.eulerAngles;
     
        memory.Add(new Memory (PatT, RatT));
    }

    public void AddToRewardMemory(GameObject CLAB, float reward, int action)
    {
        Vector3 PatT = CLAB.transform.position;
        Vector3 RatT = CLAB.transform.eulerAngles;

        rMemory.Add(new RewardMemory(PatT, RatT,reward,action));
    }

    public float PredictionError(List<Memory> memory, GameObject CLAB, int action)
    {
        CuriousLearningAgent cla = CLAB.GetComponent<CuriousLearningAgent>();
        float expectedMoveMultiplyer = cla.moveMultiplyer;
        Vector3 expectedRight = cla.right;
        Vector3 expectedLeft = cla.left;

        Vector3 currentPos = memory[memory.Count - 1].positionAtTime;
        Vector3 pastPos = memory[memory.Count - 2].positionAtTime;
        Vector3 currentRot = memory[memory.Count - 1].rotationAtTime;
        Vector3 pastRot = memory[memory.Count - 2].rotationAtTime;

        int action0Multiplyer=0;
        int action1Multiplyer=0;
        int action2Multiplyer=0;
        if (action == 0)
        {
            action0Multiplyer = 1;
        }

        if (action == 1)
        {
            action1Multiplyer = 1;
        }

        if (action == 2)
        {
            action2Multiplyer = 1;
        }
        Vector3 forwardXY = new Vector3(CLAB.transform.forward.x, 0f, CLAB.transform.forward.z);
        Vector3 expectedPos = pastPos + (forwardXY*expectedMoveMultiplyer*action0Multiplyer);
        Vector3 expectedRot = pastRot + (expectedRight * action1Multiplyer)+(expectedLeft*action2Multiplyer);
        Vector3 errorPos = currentPos - expectedPos;
        Vector3 errorRot = currentRot - expectedRot;
        float errorMagnitude = Vector3.Magnitude(errorPos);

        return errorMagnitude;      
    }

    public int EvaluateAction(List<RewardMemory> rMemory, float threshold, int minSamples)
    {
        if (rMemory.Count < minSamples)
        {
            Debug.Log("Initial Random Actions");
            return (Mathf.RoundToInt(Random.RandomRange(0f, 2f)));
        }

        else
        {
            Vector3 currentPos = CLAB.transform.position;
            Vector3 currentRot = CLAB.transform.eulerAngles;
            RewardMemory lastmemory = rMemory[rMemory.Count - 1];

            if (lastmemory.reward > threshold) 
            {
                return lastmemory.action;
                Debug.Log("Action Selected");
            }
                Debug.Log("Action Below Threshold");
                return (Mathf.RoundToInt(Random.RandomRange(0f, 2f)));
        }
    }
}

