using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space.Classes
{
    class Camera
    {
        public Vector2 Origin;
        public float Zoom;
        public float Rotation;
        private Viewport viewport;
        private Rectangle limits;
        private Vector2 position;

        public Vector2 Position
        {
            get
            { return position; }
            set
            {
                position = value;
                position.X = MathHelper.Clamp(position.X, Limits.X, Limits.X + Limits.Width - viewport.Width);
                position.Y = MathHelper.Clamp(position.Y, Limits.Y, Limits.Y + Limits.Height - viewport.Height);

            }
        }

        public Rectangle Limits
        {
            get
            { return limits; }
            set
            {
                limits = new Rectangle(
                    value.X,
                    value.Y,
                    System.Math.Max(viewport.Width, value.Width),
                    System.Math.Max(viewport.Height, value.Height));
                Position = Position;
            }
        }

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
            Zoom = 1.0f;
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(Zoom, Zoom, 1.0f) *
                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void Move(Vector2 displacement, bool respectRotation = false)
        {
            if (respectRotation)
                displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(-Rotation));

            Position += displacement;
        }
    }
}
