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
        public bool debugColliders;

        public Dictionary<string, UIElement> UIElements = new Dictionary<string, UIElement>();

        public List<ScreenCollider> screenColliders = new List<ScreenCollider>();

        public ScreenGUI()
        {
            UIElements.Add("position",new Frame(0.5f, new(0,0), "position", width: 200, height: 30, backgroundColor: Color.White, borderColor: Color.Black, borderWidth: 3, textColor:Color.Black));
            UIElements.Add("vitalsBackground", new Frame(0.5f, new(1720, 980), "vitalsBackground", width: 200, height: 100, backgroundColor: Color.White, borderColor: Color.Black, borderWidth: 3, textColor: Color.Black));

            UIElements.Add("healthBar", new Frame(0.4f, new(1730, 990), "healthBar", width: 180, height: 45, backgroundColor: Color.Red, borderColor: Color.Transparent, borderWidth: 3, textColor: Color.Black));
            UIElements.Add("healthText", new Frame(0.4f, new(1739, 995), "healthText", width: 180, height: 45, backgroundColor: Color.Transparent, borderColor: Color.Transparent, borderWidth: 3, textColor: Color.Black, text: "Health: 100"));

            UIElements.Add("hotbarBackground", new Frame(0.4f, new(680, 960), "hotbarBackground", Color.White, 0f, Color.Black, 0f, 3, 560, 120, null, Color.Black));

            debugColliders = true;

            string today = DateTime.Today.ToString("dd/MM/yy");

            UIElements.Add("textTest", new Text(0.5f, new(1700, 0), "testText", text: "The Rec v0.21dev " + today, width: 300, height: 300, textColor:Color.Black));
        }

        public void Draw()
        {
            foreach (KeyValuePair<string,UIElement> KeyValuePair in UIElements)
            {
                KeyValuePair.Value.Update();
                KeyValuePair.Value.Draw();
            }

            if (debugColliders)
            {
                foreach (ScreenCollider collider in screenColliders)
                {
                    collider.DebugDraw();
                }
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
                case "width":
                    {
                        int width = int.Parse(value);
                        element.width = width;
                        break;
                    }
            }          
        }
    }
}
