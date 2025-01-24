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
        private Vector2 position; // Store the current position of the camera
        public Matrix Transform { get; private set; }

        public float cameraZoom;

        public Camera()
        {
            position = Vector2.Zero;
        }

        public void Follow(Vector2 targetPosition, Viewport viewport, float lerpFactor = 0.1f)
        {
            // Interpolate between the current position and the target position
            position = Vector2.Lerp(position, targetPosition, lerpFactor);

            // Create the camera's transformation matrix
            var translation = Matrix.CreateTranslation(-position.X, -position.Y, 0);
            var offset = Matrix.CreateTranslation(viewport.Width / 2, viewport.Height / 2, 0);
            var zoom = Matrix.CreateScale(cameraZoom, cameraZoom, 1); // Adjust the scale factor as needed

            Transform = translation * zoom * offset;
        }
    }


}
