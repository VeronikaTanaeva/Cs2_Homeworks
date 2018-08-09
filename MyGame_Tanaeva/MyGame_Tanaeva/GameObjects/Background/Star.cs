using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva//.GameObjects.Background
{
    /// <summary>
    /// Фоновый объект: звезда
    /// </summary>
    class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.WhiteSmoke, Pos.X + Size.Width / 2, Pos.Y - Size.Height / 2, Pos.X + Size.Width / 2, Pos.Y + Size.Height + Size.Height / 2);
            Game.Buffer.Graphics.DrawLine(Pens.WhiteSmoke, Pos.X - Size.Width / 2, Pos.Y + Size.Height / 2, Pos.X + Size.Width + Size.Width / 2, Pos.Y + Size.Height / 2);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X > Game.Width)
                Pos.X = 0;
        }
    }
}
