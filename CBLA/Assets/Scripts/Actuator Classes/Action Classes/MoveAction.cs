using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Move Action", menuName = "Actuators/Actions/Move Action")]
public class MoveAction : ActionBase  {
    public Vector3 MoveAmmount;
    public override void DoAction(GameObject actor)
    {
        CharacterController controller = actor.GetComponent<CharacterController>();
        base.DoAction(actor);
        Vector3 LocalMoveVector = ((actor.transform.right * MoveAmmount.x)+ (actor.transform.up * (MoveAmmount.y - 9.8f*Time.deltaTime)) + (actor.transform.forward * MoveAmmount.z));
        controller.Move(LocalMoveVector);
    }


}
