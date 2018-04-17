using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RotateAction", menuName = "Actuators/Actions/Rotate Action")]
public class RotateAction : ActionBase {
    public Vector3 rotationVector;
    public override void DoAction(GameObject actor)
    {
        Vector3 LocalMoveVector = new Vector3(0, -9.8f * Time.deltaTime, 0);
        base.DoAction(actor);
        CharacterController controller= actor.GetComponent<CharacterController>();
        controller.transform.Rotate(rotationVector);
        controller.Move(LocalMoveVector);
    }
}
