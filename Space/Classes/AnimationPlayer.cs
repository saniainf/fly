using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space.Classes
{
    class AnimationPlayer
    {
        private Animation currentAnimation;
        private int frameIndex;
        private float time;

        public int CurrentFrame
        {
            get { return frameIndex; }
        }

        public Animation Animation
        {
            get { return currentAnimation; }
        }

        public void PlayAnimation(Animation animation)
        {
            if (Animation == animation)
                return;
            else
            {
                this.currentAnimation = animation;
                this.frameIndex = 0;
                this.time = 0.0f;
            }
        }
    }
}
