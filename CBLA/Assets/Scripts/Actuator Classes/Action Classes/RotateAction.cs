using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RotateAction", menuName = "Actuators/Actions/Rotate Action")]
public class RotateAction : ActionBase {
    public Vector3 rotationVector;
    public override void DoAction(GameObject actor)
    {
        base.DoAction(actor);
        actor.transform.Rotate(rotationVector);
    }
}
