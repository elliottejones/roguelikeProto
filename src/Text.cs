using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace csproject2024.src
{
    internal class Text: UIElement
    {
        public Text(float layerHeight, Vector2 position, string name, Color backgroundColor = new Color(), float backgroundTransparency = 0f, Color borderColor = new Color(), float borderTransparency = 0f, int borderWidth = 0, int width = 10, int height = 10, string text = null, Color textColor = new Color()) : base(layerHeight, position, name, backgroundColor, backgroundTransparency, borderColor, borderTransparency, borderWidth, width, height, text, textColor)
        {
            
        }

        public override void Draw()
        {
            float yOffset = 0;

            foreach (string line in textLines)
            {
                Globals.UISpriteBatch.DrawString(arielFont, line, position + new Vector2(0, yOffset), textColor, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, layerHeight - 0.1f);
                yOffset += arielFont.LineSpacing + 5;
            }
        }
    }
}
