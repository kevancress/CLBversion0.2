using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jump Action", menuName = "Actuators/Actions/Jump Action")]
public class JumpAction : ActionBase {
    public Vector3 JumpVector;
    CharacterController controller;
    public override void DoAction(GameObject actor)
    {
        controller = actor.GetComponent<CharacterController>();
        base.DoAction(actor);
        
        Vector3 gravityVector = new Vector3(0, (-9.8f), 0);
        if (controller.isGrounded)
        {
            Debug.Log("controller Grounded");
            controller.Move(JumpVector*Time.deltaTime);
        }
        else
        {
            controller.Move(gravityVector * Time.deltaTime);
            Debug.Log("controller not Grounded");
        }
    }
}
