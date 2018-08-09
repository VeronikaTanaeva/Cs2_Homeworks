using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva//.GameObjects.Background
{
    /// <summary>
    /// Фоновый объект - "туманность"
    /// </summary>
    class Nebula : BaseObject
    {
        public Nebula(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.DarkViolet, Pos.X, Pos.Y, Size.Width * 2, Size.Height);
        }

        public override void Update()
        {
            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.Y > Game.Height)
                Pos.Y = 0;
        }
    }
}
