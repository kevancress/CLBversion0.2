using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KGA: MonoBehaviour
{
    public List<ErrorMemory> ememory = new List<ErrorMemory>();

    public float calcError;
    public GameObject CLAB;


      
    void Start()
    {
        CLAB = GameObject.Find("CLAB");
    }

    public void AddToErrorMemory(float error)
    {
        ememory.Add(new ErrorMemory(error));
    }

    public float MeanError(List<ErrorMemory> ememory, int samples)
    {
        float meanError=0f;
        if (ememory.Count < samples)
        {
            foreach(ErrorMemory emem in ememory)
            {
                meanError += emem.error;
            }
            return (meanError / ememory.Count);

        }

        else
        {
            for (int i = 0; i < samples; i++)
            {
                meanError += ememory[ememory.Count - 1 - i].error;
            }

            meanError = meanError / samples;
            return meanError;
        }
    }

    public float MetaM(List<ErrorMemory> ememory,int samples, int offset)
    {
        float metaError = 0f;
        if (ememory.Count < (samples+offset))
        {
            foreach (ErrorMemory emem in ememory)
            {
                metaError += emem.error;
            }
            return (metaError / ememory.Count);

        }

        for (int i = 0; i < samples; i++)
        {
            metaError += ememory[ememory.Count - offset - i].error;
        }

        metaError = metaError / samples;
        return metaError;
    }

    public float Reward (float meanError, float metaError)
    {
        if (meanError == metaError)
        {
            return 0;
        }
        else
        {
            return Mathf.Abs(meanError-metaError);
        }
       
    }
}