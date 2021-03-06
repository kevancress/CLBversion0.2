﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet;

public class PredictionModel : MonoBehaviour {
    Expert expert;
    CuriousLearningAgent cla;
    List<SensoryMotorMemory> smMemory;
    [HideInInspector]
    public List<double> SensoryActionDataAtT = new List<double>();
    [HideInInspector]
    public List<double> SensoryDataAtTPlus1 = new List<double>();
    double intercept;
    double slope;
    Vector3 debugDataPoint;
    public Color FitLineColour;
    public Color DataPointColour;

    void Start()
    {
        expert = gameObject.GetComponent<Expert>();
        cla = gameObject.GetComponent < CuriousLearningAgent>();
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

    public void DebugModel()
    {
        float scaleFactor = .1f;
        for (int i = 0; i < cla.sensors.Count*2; i++)
        {
            scaleFactor *= 10;
        }
        Vector3 FitLineStart = new Vector3(0, (float)intercept/scaleFactor, 0);
        Vector3 FitLineEnd = new Vector3(100, (100 * (float)slope) + (float)intercept/ scaleFactor, 0);
        Debug.DrawLine(FitLineStart, FitLineEnd,FitLineColour, .1f, false);
        for (int i = 0; i < (SensoryActionDataAtT.Count-1); i++)
        {
            if (i < (SensoryDataAtTPlus1.Count - 1))
            {
                debugDataPoint = new Vector3((float)SensoryActionDataAtT[i]/scaleFactor, (float)SensoryDataAtTPlus1[i]/scaleFactor, 0);
                Debug.DrawLine(debugDataPoint,debugDataPoint+(Vector3.forward*1),DataPointColour,.1f,false);
            }
        }
    }

}
