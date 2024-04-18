using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine.UIElements;

[MainToolbarElement(nameof(ButtonInGroup1))]
public class ButtonInGroup1 : Button
{
    public ButtonInGroup1()
    {
        text = GetType().Name;
    }
}

[MainToolbarElement(nameof(ButtonInGroup2))]
public class ButtonInGroup2 : Button
{
    public ButtonInGroup2()
    {
        text = GetType().Name;
    }
}

[MainToolbarElement(nameof(ButtonInGroup3))]
public class ButtonInGroup3 : Button
{
    public ButtonInGroup3()
    {
        text = GetType().Name;
    }
}