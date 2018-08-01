using System;
using System.Drawing;

namespace MyGame_Tanaeva
{
    /// <summary>
    /// Интерфейс столкновений, наследуемый только  объектами, которые могут участвовать в столкновениях (пуля и астероид, впоследствии корабль)
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
        /// <summary>
        /// Смена позиции объекта после столкновения: появиться в левой (пуля) или правой (астероид) части экрана на рандомной высоте
        /// </summary>
        void CollisionUpdate();
    }

    abstract class BaseObject
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

        public abstract void Draw();

        public abstract void Update();
    }

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

    class Asteroid : BaseObject,  ICollision
    {
        Image image;


        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = Image.FromFile(@"Images\asteroid.png");
        }

        public override void Draw()
        {
            try
            {
                Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y);
            }
            catch
            {
                Game.Buffer.Graphics.FillEllipse(Brushes.PaleVioletRed, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
        }

        public override void Update()
        {
            Random arand = new Random();
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width - Size.Width)
                Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height - Size.Height) Dir.Y = -Dir.Y;
        }

        public void CollisionUpdate()
        {
            Random brand = new Random();
            Pos.X = Game.Width-Size.Width;
            Pos.Y = brand.Next(0, Game.Height);
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);
    }
    /// <summary>
    /// Класс "Пуля"
    /// Пуля существует единственная. Движется строго слева направо по прямой.
    /// Когда пуля уходит за границу игры или сталкивается с астероидом, она снова появляется в координате х=0 в рандомной координате у в пределах игрового поля
    /// </summary>
    class Bullet : BaseObject, ICollision
    {
        Image image;
        
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = Image.FromFile(@"Images\bullet.png");
        }

        public override void Draw()
        {
            try
            {
                Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y);
            }
            catch
            {
                Game.Buffer.Graphics.FillEllipse(Brushes.PaleVioletRed, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
        }

        public override void Update()
        {
            Random brand = new Random();
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width - Size.Width)
            {
                Pos.X = 0;
                Pos.Y = brand.Next(0, Game.Height);
            }
            
        }

        public void CollisionUpdate()
        {
            Random brand = new Random();
            Pos.X = 0;
            Pos.Y = brand.Next(0, Game.Height);
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}