using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;

namespace ElementsInNamespace.ElementsNamespace1
{
    [MainToolbarElement("Button In Namespace 1")]
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
    [MainToolbarElement("Button In Namespace 2")]
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
    [MainToolbarElement("Button In Namespace 3")]
    public class ButtonInNamespace : EditorToolbarButton
    {
        public ButtonInNamespace()
        {
            text = "Button Namespace 3";
        }
    }
}
