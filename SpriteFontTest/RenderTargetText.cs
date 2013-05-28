using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpriteFontTest
{
    class RenderTargetText
    {
        private GraphicsDevice gd;
        private SpriteFont font;
        private string text;
        private RenderTarget2D renderTarget;
        private SpriteBatch sb;
        public Texture2D Texture { get; private set; }

        public RenderTargetText(GraphicsDevice gd, SpriteFont font, string text)
        {
            this.gd = gd;
            this.font = font;
            this.text = text;
            renderTarget = new RenderTarget2D(gd, 1024, 1024, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
            renderTarget.ContentLost += new EventHandler<EventArgs>(renderTarget_ContentLost);
        }

        void renderTarget_ContentLost(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Begin()
        {
            gd.SetRenderTarget(renderTarget);
            gd.DepthStencilState = DepthStencilState.None;
            gd.RasterizerState = RasterizerState.CullNone;

            gd.Clear(ClearOptions.Target, new Color(16, 16, 16, 16), 1f, 0);
            sb = new SpriteBatch(gd);
            sb.Begin();
        }

        public void End()
        {
            sb.End();
            gd.SetRenderTarget(null);
            Texture = renderTarget;
        }

        public void Draw()
        {
            sb.DrawString(font, text, new Vector2(), Color.Green);
        }
    }
}
