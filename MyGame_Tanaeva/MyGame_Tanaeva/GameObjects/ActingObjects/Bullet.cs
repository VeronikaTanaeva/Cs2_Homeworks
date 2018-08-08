using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva//.GameObjects.ActingObjects
{
    /// <summary>
    /// Пуля
    /// Движется строго слева направо по прямой.
    /// Когда пуля уходит за границу игры или сталкивается с астероидом, она исчезает
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
            Pos.X = Pos.X + Dir.X;


        }

        public void CollisionUpdate(BaseObject b)
        {
            image.Dispose();
            b = null;
        }

        public bool OutOfScreen()
        {
            return (Pos.X > Game.Width);
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
