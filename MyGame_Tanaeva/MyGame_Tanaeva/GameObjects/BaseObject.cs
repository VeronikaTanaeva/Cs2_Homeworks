using System;
using System.Drawing;

namespace MyGame_Tanaeva
{   
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
    /// Интерфейс столкновений взаимодействующих игровых объектов
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
        /// <summary>
        /// Метод для смены позиции объекта после столкновения: появиться в левой (пуля) или правой (астероид) части экрана на рандомной высоте
        /// </summary>
        void CollisionUpdate(BaseObject obj);
    }

 

 

  

 
}