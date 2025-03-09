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

        public float cameraZoom = 7f;

        private float lerpZoom;

        public Camera()
        {
            position = Vector2.Zero;
        }

        public void Follow(Vector2 targetPosition, Viewport viewport, float lerpFactor = 0.03f)
        {
            // interpolate between the current position and the target position
            cameraZoom = Math.Clamp(cameraZoom, 3, 8);
            position = Vector2.Lerp(position, targetPosition, lerpFactor);
            lerpZoom = Vector2.Lerp(new(lerpZoom, 0), new(cameraZoom, 0), 0.05f).X;

            // create the cameras transformation matrix
            var translation = Matrix.CreateTranslation(-position.X, -position.Y, 0);
            var offset = Matrix.CreateTranslation(viewport.Width / 2, viewport.Height / 2, 0);
            var zoom = Matrix.CreateScale(lerpZoom, lerpZoom, 1);

            Transform = translation * zoom * offset;
        }
    }


}
