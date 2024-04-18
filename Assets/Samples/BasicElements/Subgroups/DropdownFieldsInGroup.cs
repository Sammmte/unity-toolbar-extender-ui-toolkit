using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(nameof(DropdownFieldInGroup1))]
public class DropdownFieldInGroup1 : DropdownField
{
    public DropdownFieldInGroup1() : base(
        nameof(DropdownFieldInGroup1), 
        new List<string>() { "Option 1", "Option 2" }, 
        0)
    {

    }
}

[MainToolbarElement(nameof(DropdownFieldInGroup2))]
public class DropdownFieldInGroup2 : DropdownField
{
    public DropdownFieldInGroup2() : base(
        nameof(DropdownFieldInGroup2),
        new List<string>() { "Option 1", "Option 2" },
        0)
    {

    }
}

[MainToolbarElement(nameof(DropdownFieldInGroup3))]
public class DropdownFieldInGroup3 : DropdownField
{
    public DropdownFieldInGroup3() : base(
        nameof(DropdownFieldInGroup3),
        new List<string>() { "Option 1", "Option 2" },
        0)
    {

    }
}