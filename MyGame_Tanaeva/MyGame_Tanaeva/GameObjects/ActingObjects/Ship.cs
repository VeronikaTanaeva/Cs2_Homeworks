using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva//.GameObjects
{
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
            _energy = (t > 100) ? 100 : t;
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
            if (Pos.Y < Game.Height-Size.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        public void Right()
        {
            if (Pos.X < Game.Width-Size.Width) Pos.X = Pos.X + Dir.X;
        }

        public void Left()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
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

        public void CollisionUpdate(BaseObject s)
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Red, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }
}
