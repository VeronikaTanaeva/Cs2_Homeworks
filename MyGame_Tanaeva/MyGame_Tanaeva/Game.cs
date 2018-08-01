﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame_Tanaeva
{
    /// <summary>
    /// Исключение для проверки параметров, с которыми создаём астероиды.
    /// </summary>
    class GameObjectException : Exception
    {
        public GameObjectException(string s)
        {
            MessageBox.Show(s);            
        }
    }

    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;
        public static BaseObject[] _asteroids;
        public static Bullet _bullet;
        
        static Game()
        {
        }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;

            if (Width < 0 || Width > 1000 || Height < 0 || Height > 1000)
                throw new ArgumentOutOfRangeException();

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));


                Load();

                Timer timer = new Timer { Interval = 50 };
                timer.Start();
                timer.Tick += Timer_Tick;
            
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.FromArgb(10, 3, 31));
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
                a.Draw();
            _bullet.Draw();
            if (Program.Count()) //не перерисовываем окно с игрой, если оно закрыто
                Buffer.Render(); 
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    a.CollisionUpdate();
                    _bullet.CollisionUpdate();
                }
            }
            _bullet.Update();
        }


        public static void Load()
        {
            _objs = new BaseObject[60];
            int x, y, vx, vy;

                for (int i = 0; i < _objs.Length; i = i + 4)
                    _objs[i] = new SmallPlanet(new Point(600, i * 20), new Point(-i - 1, -i - 1), new Size(10, 10));
                for (int i = 1; i < _objs.Length; i = i + 4)
                    _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(10, 10));
                for (int i = 2; i < _objs.Length; i = i + 4)
                    _objs[i] = new PlanetImage(new Point(i * i, i * 20), new Point(-8, -9), new Size(30, 30));
                for (int i = 3; i < _objs.Length; i = i + 4)
                    _objs[i] = new Nebula(new Point(i * i, 10), new Point(-10, -i / 2), new Size(10, 10));


            Random rand = new Random();

            _asteroids = new BaseObject[10]; //создаю астероиды отдельно, чтобы при проверке столкновений не выискивать астероиды среди объектов фона
            for (int i = 0; i < _asteroids.Length; i++)
            {
                try //намеренно позволяю астероидам создаться с недопустимыми параметрами, чтобы отловить и исправить такую ситуацию
                {
                    x = rand.Next(-10, Game.Width - 40);
                    if (x < 0 || x > Game.Width - 50)
                        throw new GameObjectException("Недопустимые координаты!");
                    y = rand.Next(-10, Game.Height - 40);
                    if (y < 0 || y > Game.Height - 50)
                        throw new GameObjectException("Недопустимые координаты!");
                    vx = rand.Next(-21, 21);
                    if (vx < -20 || vx > 20)
                        throw new GameObjectException("Недопустимая скорость!");
                    vy = rand.Next(-21, 21);
                    if (vy < -20 || vy > 20)
                        throw new GameObjectException("Недопустимая скорость!");
                }
                catch
                {
                    x = rand.Next(0, Game.Width - 50);
                    y = rand.Next(0, Game.Height - 50);
                    vx = rand.Next(-20, 20);
                    vy = rand.Next(-20, 20);
                }
                _asteroids[i] = new Asteroid(new Point(x, y), new Point(vx, vy), new Size(50, 50));
            }

            _bullet = new Bullet(new Point(0, 300), new Point(30, 0), new Size(30, 10));

        }
    }
}