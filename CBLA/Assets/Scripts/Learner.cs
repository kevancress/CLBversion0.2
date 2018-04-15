using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Learner : MonoBehaviour {

    public GameObject CLAB;

    public delegate void Action1();
    public static event Action1 OnAction1;

    public delegate void Action2();
    public static event Action2 OnAction2;

    public delegate void Action3();
    public static event Action3 OnAction3;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame

   
	public int CallAction (int actionNumber) {

        switch (actionNumber)
        {
            case 0:
                if (OnAction1 != null)
                {
                    OnAction1();
                }
                return 0;

            case 1:
                if (OnAction2 != null)
                {
                    OnAction2();
                }
                return 1;
            case 2:
                if (OnAction3 != null)
                {
                    OnAction3();
                }
                return 2;
            default:
                return 3;
        }
		
	}
}
