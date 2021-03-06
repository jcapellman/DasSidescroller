﻿using DasUberScroller.lib.Containers;
using DasUberScroller.lib.Managers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DasUberScroller.lib.Objects
{
    public class Player : BaseObject
    {
        private int column;
        private int row;
        private int _xPosition;

        private double _lastMs = 0;

        private const double Delay = 17;

        private const float Scale = 3.0f;
        private const int SpriteHeight = 100;
        private const int SpriteWidth = 80;
        private const int MovementX = 30;
        private int ScaledSpriteWidth => (int) (SpriteWidth * Scale * WindowContainer.ScaleResolutionX);

        private const string SpriteSheetName = "hero_spritesheet";

        public Player(GameContentManager contentManager, WindowContainer windowContainer) : base(windowContainer)
        {
            contentManager.LoadTexture(SpriteSheetName);
            
            column = 0;
            row = 1;

            _xPosition = 0;
        }

        public override void Render(SpriteBatch spriteBatch, GameContentManager contentManager)
        {
            Draw(
                SpriteSheetName, 
                new Rectangle(column * SpriteWidth, row * SpriteHeight, SpriteWidth, SpriteHeight), 
                new Vector2(_xPosition, WindowContainer.ResolutionY - ((Scale * WindowContainer.ScaleResolutionY) * SpriteHeight)),
                Scale,
                spriteBatch, 
                contentManager);
        }

        public override void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            _lastMs += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_lastMs < Delay)
            {
                return;
            }

            _lastMs = 0;

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (column == 5)
                {
                    column = 0;
                }
                else
                {
                    column++;
                }

                if (_xPosition + ScaledSpriteWidth < WindowContainer.ResolutionX)
                {
                    _xPosition += MovementX;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (column == 5)
                {
                    column = 0;
                }
                else
                {
                    column++;
                }

                if (_xPosition - MovementX > 0)
                {
                    _xPosition -= MovementX;
                }
            }
        }
    }
}