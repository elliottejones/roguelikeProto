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

        public Dictionary<string, UIElement> UIElements = new Dictionary<string, UIElement>();

        public ScreenGUI()
        {
            UIElements.Add("position",new Frame(0.5f, new(0,0), "position", width: 300, height: 100, backgroundColor: Color.White, borderColor: Color.Black, borderWidth: 3));
        }

        public void Draw()
        {
            foreach (KeyValuePair<string,UIElement> KeyValuePair in UIElements)
            {
                KeyValuePair.Value.Update();
                KeyValuePair.Value.Draw();
            }
        }

        public void UpdateText(string elementName, string text)
        {
            UIElement element = UIElements[elementName];
            element.text = text;
        }
    }
}
