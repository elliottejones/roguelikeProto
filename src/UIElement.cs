using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.IO;

namespace csproject2024.src
{
    abstract class UIElement
    {
        SpriteFont arielFont;

        public event EventHandler clicked;
        public event EventHandler hovering;

        public string name;

        public bool active;

        public bool visible;

        public Color backgroundColor;
        public float backgroundTransparency;

        public int width;
        public int height;

        public Color borderColor;
        public float borderTransparency;
        public int borderWidth;

        public float layerHeight;

        public Vector2 position;

        public Rectangle bounds;

        public string text;

        public Texture uibit;

        private List<string> textLines;

        public UIElement(float layerHeight, Vector2 position, string name, Color backgroundColor, float backgroundTransparency = 0f, Color borderColor = new Color(), float borderTransparency = 0f, int borderWidth = 0, int width = 10, int height = 10, string text = null)
        {
            this.name = name;

            this.visible = true;
            this.active = true;

            this.backgroundColor = backgroundColor;
            this.backgroundTransparency = backgroundTransparency;

            this.borderColor = borderColor;
            this.borderTransparency = borderTransparency;
            this.borderWidth = borderWidth;

            this.layerHeight = layerHeight;
            this.position = position;

            this.width = width;
            this.height = height;

            this.text = text;

            uibit = new(Globals.Content.Load<Texture2D>("uibit"), Vector2.Zero, "uibit", false);

            bounds = new(new((int)position.X,(int)position.Y), new(width,height));

            arielFont = Globals.Content.Load<SpriteFont>("arielFont");

            if (text != null )
            {
                textLines = CalculateTextLines();
            }
        }

        public List<string> CalculateTextLines()
        {

            // line wrappign works. must incorporate scale next.

            List<string> lines = new List<string>();
            float lineWidth = 0;
            string[] words = text.Split(' ');
            float spaceWidth = arielFont.MeasureString(" ").X;
            string line = "";

            foreach (string word in words)
            {
                float wordWidth = arielFont.MeasureString(word).X;

                if (lineWidth < width - wordWidth - 3)
                {
                    line += " " + word;

                    Console.WriteLine("added " + word + " to line");

                    lineWidth += wordWidth + spaceWidth;

                    Console.WriteLine("linewidth now = " + lineWidth);

                }
                else
                {
                    lines.Add(line);

                    Console.WriteLine("first line = " + line);

                    line = "";
                    lineWidth = 0;
                }
            }

            if (line.Length > 0)
            {
                lines.Add(line);
            }

            return lines;
        }

        public void Update()
        {
            if (bounds.Contains(InputManager.MousePosition))
            {
                hovering?.Invoke(hovering, EventArgs.Empty);
                Console.WriteLine("hovered");

                if (InputManager.LeftMouseDown)
                {
                    clicked?.Invoke(clicked, EventArgs.Empty);
                }
            }
        }

        public void Draw()
        {
            if (text != null)
            {
                float yOffset = 0;

                foreach (string line in textLines)
                {
                    Globals.UISpriteBatch.DrawString(arielFont, line, position + new Vector2(0,yOffset), Color.Black, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, layerHeight - 0.1f);
                    yOffset += arielFont.LineSpacing + 5;
                }
                
            }
            
            Globals.UISpriteBatch.Draw(uibit.texture, position, null, backgroundColor, 0f, Vector2.Zero, new Vector2(width, height), SpriteEffects.None, layerHeight);

            //Drawing borders

            Globals.UISpriteBatch.Draw(uibit.texture, new Vector2(position.X, position.Y - borderWidth), null, borderColor, 0f, Vector2.Zero, new Vector2(width,borderWidth), SpriteEffects.None, layerHeight);
            Globals.UISpriteBatch.Draw(uibit.texture, new Vector2(position.X - borderWidth, position.Y - borderWidth), null, borderColor, 0f, Vector2.Zero, new Vector2(borderWidth,height + borderWidth), SpriteEffects.None, layerHeight);
            Globals.UISpriteBatch.Draw(uibit.texture, new Vector2(position.X - borderWidth, position.Y + height), null, borderColor, 0f, Vector2.Zero, new Vector2(width + borderWidth ,borderWidth), SpriteEffects.None, layerHeight);
            Globals.UISpriteBatch.Draw(uibit.texture, new Vector2(position.X + width, position.Y - borderWidth), null, borderColor, 0f, Vector2.Zero, new Vector2(borderWidth, height + 2 * borderWidth), SpriteEffects.None, layerHeight);
        }

    }
}
