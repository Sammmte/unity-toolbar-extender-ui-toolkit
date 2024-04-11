using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;

namespace ElementsInNamespace.ElementsNamespace1
{
    [MainToolbarElement]
    public class ButtonInNamespace : EditorToolbarButton
    {
        public ButtonInNamespace()
        {
            text = "Button Namespace 1";
        }
    }
}

namespace ElementsInNamespace.ElementsNamespace2.AnotherSubNamespace
{
    [MainToolbarElement]
    public class ButtonInNamespace : EditorToolbarButton
    {
        public ButtonInNamespace()
        {
            text = "Button Namespace 2";
        }
    }
}

namespace ElementsInNamespace.ElementsNamespace3.AnotherSubNamespace
{
    [MainToolbarElement]
    public class ButtonInNamespace : EditorToolbarButton
    {
        public ButtonInNamespace()
        {
            text = "Button Namespace 3";
        }
    }
}
