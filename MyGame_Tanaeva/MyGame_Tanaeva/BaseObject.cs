using System;
using System.Drawing;

namespace MyGame_Tanaeva
{
    class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.SkyBlue, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public virtual void Update()
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

    class PlanetImage : BaseObject
    {
        public PlanetImage(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            try
            {
                Image image = Image.FromFile("planet.png");
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