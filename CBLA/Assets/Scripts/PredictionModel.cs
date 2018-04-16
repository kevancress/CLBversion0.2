using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet;

public class PredictionModel : MonoBehaviour {
    Expert expert;
    CuriousLearningAgent cla;
    List<SensoryMotorMemory> smMemory;
    public List<double> SensoryActionDataAtT = new List<double>();
    public List<double> SensoryDataAtTPlus1 = new List<double>();
    double intercept;
    double slope;

    void Start()
    {
        expert = gameObject.GetComponent<Expert>();
        cla = gameObject.GetComponent < CuriousLearningAgent>();
        smMemory = cla.smMemory;
    }

    public void UpdateModel()
    {
        double[] inputArray = new double[SensoryActionDataAtT.Count - 1];
        double[] outputArray = new double[SensoryActionDataAtT.Count - 1];

        for (int i = 0; i < SensoryActionDataAtT.Count; i++)
        {
            if(i < inputArray.Length)
            {
                inputArray[i] = SensoryActionDataAtT[i];
            }
            
        }

        for (int i = 0; i < SensoryDataAtTPlus1.Count; i++)
        {
            if(i<outputArray.Length)
            {
                outputArray[i] = SensoryDataAtTPlus1[i];
            }

        }
        MathNet.Numerics.Tuple<double,double> fit =  MathNet.Numerics.Fit.Line(inputArray, outputArray);
        intercept = fit.Item1;
        slope = fit.Item2;
    }

    public double MakePrediction(double SensorAction)
    {
        double prediction = (slope * SensorAction) + intercept;
        return prediction;
    }

}
