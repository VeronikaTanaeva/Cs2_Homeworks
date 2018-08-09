using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva//.GameObjects.ActingObjects
{
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

        public void CollisionUpdate(BaseObject a)
        {
            image.Dispose();
            a = null;
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
