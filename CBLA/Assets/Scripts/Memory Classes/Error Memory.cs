﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMemory : Memory {

    public float error;

    public ErrorMemory (float newError)
    {
        error = newError;
    }

}
