using Paps.UnityToolbarExtenderUIToolkit;

[MainToolbarElement(align: ToolbarAlign.Right, group: TestGroups.RightGroup)]
public class TestRightGroupToggle : MainToolbarToggle
{
    public TestRightGroupToggle() : base("1234")
    {
    }
}