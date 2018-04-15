using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Memory {

    public Vector3 positionAtTime;
    public Vector3 rotationAtTime;

    public Memory(Vector3 newPositionAtTime, Vector3 newRotationAtTime)
    {
        positionAtTime = newPositionAtTime;
        rotationAtTime = newRotationAtTime;
    }
}
