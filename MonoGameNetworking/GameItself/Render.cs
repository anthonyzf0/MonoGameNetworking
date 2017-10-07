using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameNetworking.GameItself
{
    class Render
    {
        //Base texture
        Texture2D rect;
        SpriteBatch s;

        public Render(SpriteBatch graphics, GraphicsDevice g)
        {
            rect = new Texture2D(g, 1, 1);
            rect.SetData(new[] { Color.White });
            s = graphics;
        }

        public void start()
        {
            s.Begin();
        }
        public void end()
        {
            s.End();
        }

        public void draw(int x, int y, int w, int h, Color c)
        {
            s.Draw(rect, new Rectangle(x, y, w, h), c);
        }

    }
}
