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
            UIElements.Add("position",new Frame(0.5f, new(0,0), "position", width: 200, height: 30, backgroundColor: Color.White, borderColor: Color.Black, borderWidth: 3, textColor:Color.Black));

            string today = DateTime.Today.ToString("dd/MM/yy");

            UIElements.Add("textTest", new Text(0.5f, new(1700, 0), "testText", text: "Ro-land v0.21dev " + today, width: 300, height: 300, textColor:Color.Black));
        }

        public void Draw()
        {
            foreach (KeyValuePair<string,UIElement> KeyValuePair in UIElements)
            {
                KeyValuePair.Value.Update();
                KeyValuePair.Value.Draw();
            }
        }

        public void UpdateAttribute(string attributeName, string elementName, string value) 
        {
            UIElement element = UIElements[elementName];
            
            switch (attributeName)
            {
                case "text": element.text = value; break;

                case "position":
                    {
                        string[] numbers = value.Split(',');
                        element.position = new Vector2(int.Parse(numbers[0]), int.Parse(numbers[1]));
                        break;
                    }
            }          
        }
    }
}
