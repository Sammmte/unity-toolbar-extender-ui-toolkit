﻿using Paps.UnityToolbarExtenderUIToolkit;
using System;
using UnityEditor.UIElements;

[MainToolbarElement("SampleEnumFlagsField")]
public class SampleEnumFlagsField : EnumFlagsField
{
    public SampleEnumFlagsField() : base("Sample Enum Flags", MovementStates.None)
    {

    }
}

[Flags]
public enum MovementStates
{
    None = 0,
    Idle = 1,
    Sitting = 2,
    Walking = 4,
    Running = 8,
    Crouching = 16,
    Crawling = 32
}