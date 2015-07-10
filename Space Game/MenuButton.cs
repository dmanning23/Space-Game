using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceGame_Mono
{
    class MenuButton
    {
        private String Text;
        private Rectangle rec;

        public MenuButton(String text, Rectangle rec)
        {
            this.Text = text;
            this.rec = rec;
        }

        public String getText()
        {
            return Text;
        }

        public Rectangle getRectangle()
        {
            return rec;
        }

        public void setText(String text)
        {
            this.Text = text;
        }
    }
}
