using Raylib_cs;
using System.Numerics;

namespace Teaspot.Core.Components
{
    public class AnimatedSprite : Sprite
    {
        private int frameCounter = 0;
        private int frameSpeed = 8;
        private bool isRunning = true;
        private int currentFrame = 0;

        public int CurrentFrame
        {
            get => currentFrame;
            set => currentFrame = value - 1;
        }
        public int FrameCount { get; set; } = 1;
        public int FrameSpeed
        {
            get
            {
                return isRunning ? frameSpeed : 0;
            }
            set
            {
                frameSpeed = value;
            }
        }


        internal override Rectangle SourceRectangle
        {
            get
            {
                frameCounter++;
                if(FrameSpeed != 0 && frameCounter >= GameObject.scene.WindowTargetFPS / FrameSpeed)
                {
                    frameCounter = 0;
                    currentFrame++;

                    if (currentFrame > FrameCount - 1)
                    {
                        currentFrame = 0;
                    }
                }
                
                return new Rectangle((float)currentFrame * texture.width/FrameCount, 0f, texture.width/ FrameCount * Math.Sign(objTransform.Scale.X), texture.height * Math.Sign(objTransform.Scale.Y));
            }
        }
        internal override Rectangle DestRectangle
        {
            get => IsActive ? new(objTransform.Position.X, objTransform.Position.Y, Math.Abs(texture.width / FrameCount * objTransform.Scale.X), Math.Abs(texture.height * objTransform.Scale.Y)) : new(0f, 0f, 0f, 0f);
        }
        internal override Vector2 Origin => new(texture.width / FrameCount, texture.height);


        public AnimatedSprite()
        {
        }
        public AnimatedSprite(AnimatedSprite another)
        {
            SpritePath = another.SpritePath;
        }

        public override AnimatedSprite Clone()
        {
            return new AnimatedSprite(this);
        }

        public void Start()
        {
            isRunning = true;
        }
        public void Stop()
        {
            isRunning = false;
        }
    }
}
