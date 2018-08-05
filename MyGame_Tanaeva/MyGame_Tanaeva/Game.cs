using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

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
        public static Asteroid[] _asteroids;
        private static Ship _ship;
        //public static Bullet[] _bullet;
        public static Bullet _bullet;
        public static int bcount;
        public static MedKit _medkit;
        public static int score;
        
        private static Timer _timer = new Timer();
        public static Random Rnd = new Random();

        /// <summary>
        /// Ведение журнала событий
        /// </summary>
        public static StreamWriter sw;
        public static Action<string, StreamWriter> actionTarget = new Action<string, StreamWriter>(JournalMessage);

        static Game()
        {
        }

        static void JournalMessage(string msg, StreamWriter sw)
        {
            sw.WriteLine(msg);
            sw.Flush();
        }

        public static void Init(Form form)
        {
            score = bcount = 0;            
            sw = new StreamWriter(@"Journal\log.txt"); //при создании игры перезаписывается файл с логом игры
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;

            if (Width < 0 || Width > 1000 || Height < 0 || Height > 1000)
                throw new ArgumentOutOfRangeException();

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            form.KeyDown += Form_KeyDown;

            Load();

            Ship.MessageDie += Finish;

            // Timer timer = new Timer { Interval = 50 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
            
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (Program.Count())
            {
                Draw();
                Update();
            }
            else
            {
                _timer?.Stop();
                sw?.Close();
            }
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            _bullet?.Draw();
            _ship?.Draw();
            _medkit.Draw();
            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy + "\tScore:" + score, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                //Buffer.Graphics.DrawString("Score:" + score, SystemFonts.DefaultFont, Brushes.White, 0, 1);
            }
            if (Program.Count()) //не перерисовываем окно с игрой, если оно закрыто
                Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            _bullet?.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    actionTarget("Астероид сбит!", sw);
                    //_asteroids[i] = null;
                    _asteroids[i].CollisionUpdate();
                    _bullet = null;
                    score++;
                    continue;
                }
                var rnd = new Random();
                if (_ship.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    actionTarget("Корабль подбит!", sw);
                    _ship?.EnergyLow(rnd.Next(1, 10));
                    _asteroids[i].CollisionUpdate();
                    _ship.CollisionUpdate();
                    //continue;
                }
                if(_ship.Collision(_medkit))
                {
                    actionTarget("Аптечка подобрана!", sw);
                    _ship.EnergyAdd(rnd.Next(1, 10));
                    _medkit.CollisionUpdate();
                }
                if ((_bullet!=null)&& (_medkit.Collision(_asteroids[i]) || _medkit.Collision(_bullet))) continue; //если аптечка сталкивается с чем-то, кроме корабля, игнорируем это событие
                if (_ship.Energy <= 0) _ship?.Die();
            }
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

            _asteroids = new Asteroid[10]; //создаю астероиды отдельно, чтобы при проверке столкновений не выискивать астероиды среди объектов фона
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
                _asteroids[i] = new Asteroid(new Point(x, y), new Point(vx, vy), new Size(20, 20));
            }
            
            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(50, 50));

            _medkit = new MedKit(new Point(0, Game.Height/2), new Point(0, 0), new Size(20, 20));

        }        
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(30, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }
        public static void Finish()
        {
            _timer.Stop();
            sw.Close();
            //foreach (Form f in Application.OpenForms)
            //{
            //    if (f.Name == "Form2")
            //        f.Close();
            //}
            //string message = @"Конец!/nВы набрали " + score + " очков,/nсбивая астероиды!";
            Buffer.Graphics.DrawString("Конец!\nВы набрали " + score + " очков,\nсбивая астероиды!", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
    }
}