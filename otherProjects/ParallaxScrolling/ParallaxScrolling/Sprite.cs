namespace ParallaxScrolling
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
        float rotation = 0.0f;



        public Vector2 Center
        {
            get { return Position + new Vector2(Texture.Width / 2, Texture.Height / 2); }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value % MathHelper.TwoPi; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Center, null, Color.White, rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
