using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva//.GameObjects.ActingObjects
{
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
            //аптечка висит на месте, пока корабль её не подберёт, поэтому перерисовка случается только при коллизии    
        }

        public void CollisionUpdate(BaseObject m)
        {
            Random brand = new Random();
            Pos.X = brand.Next(0, Game.Width);
            Pos.Y = brand.Next(0, Game.Height);
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
