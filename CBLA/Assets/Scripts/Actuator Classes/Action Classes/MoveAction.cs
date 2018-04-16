using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Move Action", menuName = "Actuators/Actions/Move Action")]
public class MoveAction : ActionBase  {
    public Vector3 MoveAmmount;
    public override void DoAction(GameObject actor)
    {
        base.DoAction(actor);
        Vector3 LocalMoveVector = ((actor.transform.right * MoveAmmount.x)+ (actor.transform.up * MoveAmmount.y) + (actor.transform.forward * MoveAmmount.z));
        actor.transform.position += LocalMoveVector;
    }


}
