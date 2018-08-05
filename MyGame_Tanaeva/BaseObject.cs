using System;
using System.Drawing;

namespace MyGame_Tanaeva
{
    /// <summary>
    /// Делегат для обработки смерти корабля
    /// </summary>
    public delegate void Message();  
    
    /// <summary>
    /// Интерфейс столкновений, наследуемый только  объектами, которые могут участвовать в столкновениях (пуля и астероид, впоследствии корабль)
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
        /// <summary>
        /// Метод для смены позиции объекта после столкновения: появиться в левой (пуля) или правой (астероид) части экрана на рандомной высоте
        /// </summary>
        void CollisionUpdate();
    }

    /// <summary>
    /// Абстрактный класс-родитель для всех объектов заставки и самой игры
    /// </summary>
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

    /// <summary>
    /// Класс "Астероиды". 
    /// Спавнятся где получится в пределах экрана, движутся как им больше нравится. При столкновении с пулей спавнятся на правой границе экрана.
    /// </summary>
    class Asteroid : BaseObject, ICollision
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
            Pos.X = Game.Width - Size.Width;
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
            if (Pos.X > Game.Width) 
            {
                Game._bullet = null; //улетая за пределы экрана, пуля перестаёт существовать
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

    /// <summary>
    /// Корабль
    /// Появляется в правой части игрового поля, может двигаться вверх-вниз по нажатию клавиш-стрелок и стрелять по ctrl
    /// </summary>
    class Ship : BaseObject, ICollision
    {
        private int _energy;
        Image image;
        public int Energy => _energy;
        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        public void EnergyAdd(int n)
        {
            int t = _energy + n;
            _energy = (t>100) ? 100 : t;
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _energy = 100;
            image = Image.FromFile(@"Images\Ship.png");
        }

        public override void Draw()
        {
            try
            {
                Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            catch
            {
                Game.Buffer.Graphics.FillRectangle(Brushes.Yellow, Pos.X, Pos.Y, Size.Width, Size.Height);
            };
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        /// <summary>
        /// Обработка смерти корабля
        /// </summary>
        public static event Message MessageDie;
        public void Die()
        {
            MessageDie?.Invoke();
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);

        public void CollisionUpdate()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Red, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }

    /// <summary>
    /// Аптечка
    /// </summary>
    class MedKit : BaseObject, ICollision
    {
        Image image;

        public MedKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = Image.FromFile(@"Images\medkit.png");
        }

        public override void Draw()
        {
            try
            {
                Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            catch
            {
                Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
        }

        public override void Update()
        {
            //аптечка висит на месте, пока             
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