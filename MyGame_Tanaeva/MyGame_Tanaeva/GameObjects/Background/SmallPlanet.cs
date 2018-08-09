using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva//.GameObjects.Background
{
    /// <summary>
    /// Фоновый объект: маленькая планета
    /// </summary>
    class SmallPlanet : BaseObject
    {
        public SmallPlanet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }


        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.SkyBlue, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width - Size.Width)
                Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height - Size.Height) Dir.Y = -Dir.Y;
        }

    }
}
