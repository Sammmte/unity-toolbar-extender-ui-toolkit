using Paps.UnityToolbarExtenderUIToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(nameof(SliderInGroup1))]
public class SliderInGroup1 : Slider
{
    public SliderInGroup1() : base(nameof(SliderInGroup1), 0, 100)
    {

    }
}

[MainToolbarElement(nameof(SliderInGroup2))]
public class SliderInGroup2 : Slider
{
    public SliderInGroup2() : base(nameof(SliderInGroup2), 0, 100)
    {

    }
}

[MainToolbarElement(nameof(SliderInGroup3))]
public class SliderInGroup3 : Slider
{
    public SliderInGroup3() : base(nameof(SliderInGroup3), 0, 100)
    {

    }
}