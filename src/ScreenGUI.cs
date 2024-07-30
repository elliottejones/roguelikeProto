using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace csproject2024.src
{
    internal class ScreenGUI
    {
        public bool active;
        public bool visible;

        Dictionary<string, UIElement> UIElements = new Dictionary<string, UIElement>();

        public ScreenGUI()
        {
            UIElements.Add("test",new Frame(0.5f, new(300,300), "test", width: 100, height: 300, backgroundColor: Color.White, borderColor: Color.Green, borderWidth: 3));
            UIElements.Add("test2", new Frame(0.5f, new(600, 300), "test", width: 300, height: 100, backgroundColor: Color.Red, borderColor: Color.BlanchedAlmond, borderWidth: 20));
        }

        public void Draw()
        {
            foreach (KeyValuePair<string,UIElement> KeyValuePair in UIElements)
            {
                KeyValuePair.Value.Draw();
            }
        }
    }
}
