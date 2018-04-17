using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expert : MonoBehaviour {
    public GameObject CLAB;
    public float Error;
    public PredictionModel predictionModel;
    public List<RewardMemory> rMemory = new List<RewardMemory>();
    void Start () {
        CLAB = GameObject.Find("CLAB");
        predictionModel = gameObject.GetComponent<PredictionModel>();
	}

	

    public void AddToRewardMemory( float reward, int action)
    {
        rMemory.Add(new RewardMemory(reward,action));
    }

    public double MakePredition(double sensorAction)
    {
        predictionModel.UpdateModel();
        predictionModel.DebugModel();
        return predictionModel.MakePrediction(sensorAction);

    }

    public float PredictionError(double predictedSensorState,double currentSensorState)
    {   
        float errorMagnitude = (Mathf.Abs((float)currentSensorState - (float)predictedSensorState))/((float)currentSensorState+(float)predictedSensorState);

        return errorMagnitude;      
    }

    public int EvaluateAction(List<RewardMemory> rMemory, float threshold, int minSamples,List<ActionBase> availableActions)
    {
        if (rMemory.Count < minSamples)
        {
            Debug.Log("Initial Random Actions");
            return (Mathf.RoundToInt(Random.Range(0f, availableActions.Count - 1)));
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
                return (Mathf.RoundToInt(Random.Range(0f, availableActions.Count-1)));
        }
    }
}

