using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva//.GameObjects.Background
{
    /// <summary>
    /// Фоновый объект - планета
    /// </summary>
    class PlanetImage : BaseObject
    {
        Image image;

        public PlanetImage(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = Image.FromFile(@"Images\planet.png");
        }

        public override void Draw()
        {
            try
            {
                Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y);
            }
            catch
            {
                Game.Buffer.Graphics.FillEllipse(Brushes.DarkGoldenrod, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
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
