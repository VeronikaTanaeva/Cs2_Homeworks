using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace MyGame_Tanaeva
{

    static partial class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;
        private static Ship _ship;
        public static List<Bullet> _bullets;
        public static MedKit _medkit;
        public static int score, acount;
        public static List<Asteroid> _asteroids;
        
        private static Timer _timer = new Timer();
        public static Random Rnd = new Random();

        static Game()
        {
        }

        /// <summary>
        /// Инициализация новой игры
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            score = 0;            
            sw = new StreamWriter(@"Journal\log.txt"); //при создании игры перезаписывается файл с логом игры
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;

            if (Width < 0 || Width > 1000 || Height < 0 || Height > 1000)
                throw new ArgumentOutOfRangeException();

            _bullets = new List<Bullet>(bMax);
            _asteroids = new List<Asteroid>(astcount);


            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            form.KeyDown += Form_KeyDown;

            Load();

            Ship.MessageDie += Finish;

            _timer.Interval = interval;
            _timer.Start();
            _timer.Tick += Timer_Tick;
            
        }

        /// <summary>
        /// Первичная загрузка игры
        /// </summary>
        public static void Load()
        {
            _objs = new BaseObject[60];

            for (int i = 0; i < _objs.Length; i = i + 4) //эти четыре цикла создаём фон
                _objs[i] = new SmallPlanet(new Point(600, i * 20), new Point(-i - 1, -i - 1), new Size(10, 10));
            for (int i = 1; i < _objs.Length; i = i + 4)
                _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(10, 10));
            for (int i = 2; i < _objs.Length; i = i + 4)
                _objs[i] = new PlanetImage(new Point(i * i, i * 20), new Point(-8, -9), new Size(30, 30));
            for (int i = 3; i < _objs.Length; i = i + 4)
                _objs[i] = new Nebula(new Point(i * i, 10), new Point(-10, -i / 2), new Size(10, 10));

            _ship = new Ship(new Point(10, 400), new Point(10, 10), new Size(50, 50)); //создали кораблик

            _medkit = new MedKit(new Point(Game.Width / 2, Game.Height / 2), new Point(0, 0), new Size(40, 40)); //создали аптечку

            SpawnAsteroids(astcount);

        }

        /// <summary>
        /// Таймер, по которому вызывается перерисовка экрана игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Timer_Tick(object sender, EventArgs e)
        {
            if (Program.Count()) //проверка, открыто ли приложение; если нет - нужно оставить таймер и закрыть поток, пишуший лог
            {
                Draw();
                acount = astcount;
                Update();
            }
            else
            {
                _timer?.Stop();
                sw?.Close();
            }
        }
        
        /// <summary>
        /// Выгрузка игры
        /// </summary>
        public static void Finish()
        {
            _timer.Stop(); //останавливаем таймер, вызывающий рисование на форме и обновление состояния объектов
            sw.Close(); //закрываем поток логирования
            StreamWriter records = new StreamWriter(@"Docs\records.txt", true);
            records.WriteLine(score);
            records.Flush();
            records.Close();
            Buffer.Graphics.DrawString("Конец!\nВы набрали " + score + " очков,\nсбивая астероиды!", new Font(FontFamily.GenericSansSerif, 30, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
    }
}