using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuriousLearningAgent: MonoBehaviour {
    public GameObject CLAB;
    public List<ActionBase> availableAcions;
    public List<SensorBase> sensors;
    public PredictionModel PredictionModel;
    public int actionSelected;
    public int samples = 100;
    public int offset = 25;
    public float threshold = 1f;
    Expert expert;
    KGA kga;
    PredictionModel predictionModel;
    public List <SensoryMotorMemory> smMemory;
    List<Memory> memory;
    List<ErrorMemory> eMemory;
    List<RewardMemory> rMemory;
    double sensorReading;
	// Use this for initialization

    void Start()
    {
        CLAB = gameObject;
        expert = CLAB.GetComponent<Expert>();
        kga = CLAB.GetComponent<KGA>();
        memory = expert.memory;
        eMemory = kga.ememory;
        rMemory = expert.rMemory;
        expert.AddToMemory(CLAB);
        predictionModel = GetComponent<PredictionModel>();
        sensorReading = ReadSensors(sensors);
        predictionModel.SensoryActionDataAtT.Add(double.Parse(sensorReading.ToString() + "0"));
    }


    void FixedUpdate()
    {
       
        int bestAction = expert.EvaluateAction(rMemory,threshold,samples);
        sensorReading = ReadSensors(sensors);
        predictionModel.SensoryActionDataAtT.Add(double.Parse(sensorReading.ToString() + bestAction.ToString()));
        double prediction = expert.MakePredition(double.Parse(sensorReading.ToString() + bestAction.ToString()));
        availableAcions[bestAction].DoAction(CLAB);

        //now at t+1
        sensorReading = ReadSensors(sensors);
        predictionModel.SensoryDataAtTPlus1.Add(sensorReading);
        int lastAction = bestAction;
        expert.AddToMemory(CLAB);
        
        float predictionError = expert.PredictionError(prediction,sensorReading);
        Debug.Log("prediction Error:" + predictionError);
        //Debug.Log("prediction Error" + predictionError);
        kga.AddToErrorMemory(predictionError);
        float meanError = kga.MeanError(eMemory, samples);
        //Debug.Log("mean Error" + meanError);
        float metaError = kga.MetaM(eMemory, samples, offset);
        //Debug.Log("meta Error" + meanError);
        float reward = kga.Reward(meanError, metaError);
        Debug.Log("reward" + reward*10);
        expert.AddToRewardMemory(CLAB, reward*10, lastAction);  

    }

    private double ReadSensors(List<SensorBase> sensors)
    {
        int multiplyer = 100;
        double concatenatedResult=0;

        foreach (SensorBase sensor in sensors)
        {
            
            float sensorOutput = sensor.DoSense(CLAB);

            int intSensorOutput = Mathf.FloorToInt(sensorOutput * multiplyer);
            string stringSensorOutput;
            //insure concatinated value has two 0 places if result is 0
            if (intSensorOutput == 0)
            {
                stringSensorOutput = "00";
            }
            else if (intSensorOutput < 10)
            {
                stringSensorOutput = "0" + intSensorOutput.ToString();
            }
            else
            {
                stringSensorOutput = intSensorOutput.ToString();
            }

            //concatinate results
            if (concatenatedResult == 0)
            {
                concatenatedResult = intSensorOutput;
            }
            else
            {
                concatenatedResult = double.Parse(concatenatedResult.ToString() + stringSensorOutput);
            }
            
        }
        Debug.Log("concatinated Sensor result:" + concatenatedResult);
        return concatenatedResult;
    }
}
