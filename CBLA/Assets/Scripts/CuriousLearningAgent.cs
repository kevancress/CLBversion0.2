using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuriousLearningAgent: MonoBehaviour {
    public GameObject CLAB;
    public float moveMultiplyer = 0.01f;
    public Vector3 right = new Vector3(0f,10f,0f);
    public Vector3 left = new Vector3(0f, -10f, 0f);
    public int actionSelected;
    public int samples = 100;
    public int offset = 25;
    public float threshold = 1f;
    Expert expert;
    KGA kga;
    Learner learner;
    List<Memory> memory;
    List<ErrorMemory> eMemory;
    List<RewardMemory> rMemory;
	// Use this for initialization

    void Start()
    {
        CLAB = gameObject;
        expert = CLAB.GetComponent<Expert>();
        kga = CLAB.GetComponent<KGA>();
        learner = CLAB.GetComponent<Learner>();
        memory = expert.memory;
        eMemory = kga.ememory;
        rMemory = expert.rMemory;
        expert.AddToMemory(CLAB);
    }

    void OnEnable()
    {
        Learner.OnAction1 += Move;
        Learner.OnAction2 += RotateRight;
        Learner.OnAction3 += RotateLeft;

    }

    void FixedUpdate()
    {
       
        int bestAction = expert.EvaluateAction(rMemory,threshold,samples);
        int lastAction = learner.CallAction(bestAction);
        expert.AddToMemory(CLAB);
        float predictionError = expert.PredictionError(memory, CLAB, lastAction);
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
	
	void OnDisabel()
    {
        Learner.OnAction1 -= Move;
        Learner.OnAction2 -= RotateRight;
        Learner.OnAction3 -= RotateLeft;
    }


    public void Move()
    {
        CLAB.transform.position += (transform.forward*moveMultiplyer);
    }

    public void RotateRight()
    {
        CLAB.transform.Rotate(right);
    }

    public void RotateLeft()
    {
        CLAB.transform.Rotate(left);
    }

}
