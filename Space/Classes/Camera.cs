using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space.Classes
{
    public static class Camera
    {
        public static Vector2 Origin;
        public static float Zoom;
        public static float Rotation;
        public static Viewport Viewport;
        private static Rectangle limits;
        private static Vector2 position;

        public static Vector2 Position
        {
            get
            { return position; }
            set
            {
                position = value;
                position.X = MathHelper.Clamp(position.X, Limits.X, Limits.X + Limits.Width - Viewport.Width);
                position.Y = MathHelper.Clamp(position.Y, Limits.Y, Limits.Y + Limits.Height - Viewport.Height);

            }
        }

        public static Rectangle Limits
        {
            get
            { return limits; }
            set
            {
                limits = new Rectangle(
                    value.X,
                    value.Y,
                    System.Math.Max(Viewport.Width, value.Width),
                    System.Math.Max(Viewport.Height, value.Height));
                Position = Position;
            }
        }

        public static void CreateCamera(Viewport viewport)
        {
            Viewport = viewport;
            Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
            Zoom = 1.0f;
        }

        public static Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(Zoom, Zoom, 1.0f) *
                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public static void Move(Vector2 displacement, bool respectRotation = false)
        {
            if (respectRotation)
                displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(-Rotation));

            Position += displacement;
        }
    }
}
