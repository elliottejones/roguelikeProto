using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Policy;

namespace csproject2024.src
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 targetPosition, Viewport viewport)
        {
            var position = Matrix.CreateTranslation(-targetPosition.X, -targetPosition.Y, 0);
            var offset = Matrix.CreateTranslation(viewport.Width / 2, viewport.Height / 2, 0);
            var zoom = Matrix.CreateScale(7, 7, 1);
            Transform = position * zoom* offset;
        }
    }

}
