using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace csproject2024.src
{
    internal class Frame : UIElement
    {
        public Frame(float layerHeight, Vector2 position, string name, Color backgroundColor = new Color(), float backgroundTransparency = 0f, Color borderColor = new Color(), float borderTransparency = 0f, int borderWidth = 0, int width = 10, int height = 10): base (layerHeight, position, name, backgroundColor, backgroundTransparency, borderColor, borderTransparency, borderWidth, width, height)
        {
            
        }

    }
}
