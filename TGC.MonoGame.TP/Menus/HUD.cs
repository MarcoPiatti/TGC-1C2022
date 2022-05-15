using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TGC.MonoGame.TP.Menus
{
    public class HUD : Menu
    {
        public HUD(SpriteFont spriteFont, SpriteBatch SpriteBatch, ContentManager content, Player player) : base(spriteFont, SpriteBatch, content)
        {


        }
        public override void Draw(GraphicsDevice graphicsDevice)
        {
            //this.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
            //SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            //MenuHelper();
            //SpriteBatch.End();

        }


    }
}
