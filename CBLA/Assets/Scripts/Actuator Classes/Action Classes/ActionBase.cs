using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Generic Action", menuName = "Actuators/Actions/BaseAction")]
public class ActionBase : ScriptableObject {

    public string ActionName;
    [HideInInspector]
    public GameObject actor;
    public virtual void DoAction(GameObject actor)
    {
        //do a thing
        Debug.Log("Basic Action: " + ActionName + " Called on Actor" + actor.name);
    }
}
