using System;
using System.Drawing;

namespace MyGame_Tanaeva
{
    class SSBaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public SSBaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public virtual void Draw()
        {
            SplashScreen.Buffer.Graphics.FillEllipse(Brushes.DarkBlue, Pos.X, Pos.Y, Size.Width, Size.Height);            
        }

        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X <= 0) Dir.X = -Dir.X;
            if (Pos.X > SplashScreen.Width - Size.Width)
                Dir.X = -Dir.X;
            if (Pos.Y <= 0) Dir.Y = -Dir.Y;
            if (Pos.Y >= SplashScreen.Height - Size.Height) Dir.Y = -Dir.Y;
        }
    }

    class SSStar : SSBaseObject
    {
        public SSStar(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            SplashScreen.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            SplashScreen.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
            SplashScreen.Buffer.Graphics.DrawLine(Pens.WhiteSmoke, Pos.X + Size.Width / 2, Pos.Y - Size.Height / 2, Pos.X + Size.Width / 2, Pos.Y + Size.Height + Size.Height / 2);
            SplashScreen.Buffer.Graphics.DrawLine(Pens.WhiteSmoke, Pos.X - Size.Width / 2, Pos.Y + Size.Height / 2, Pos.X + Size.Width + Size.Width / 2, Pos.Y + Size.Height / 2);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X+Size.Width >= SplashScreen.Width || Pos.X <=0)
                Dir.X=-Dir.X;
        }

    }

    class SSNebula : SSBaseObject
    {
        public SSNebula(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            SplashScreen.Buffer.Graphics.FillEllipse(Brushes.Violet, Pos.X, Pos.Y, Size.Width * 2, Size.Height);
        }

        public override void Update()
        {
            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.Y+Size.Height >= SplashScreen.Height || Pos.Y <= 0 )
                Dir.Y=-Dir.Y;

        }
    }
}