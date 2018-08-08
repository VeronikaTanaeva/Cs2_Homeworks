using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva
{
    class SplashScreen
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static SSBaseObject[] _objs;

        static SplashScreen()
        {
        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            if (!Program.Count())
            {
                Load();
                Timer timer = new Timer { Interval = 50 };
                timer.Start();
                timer.Tick += Timer_Tick;
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {            
            Draw();
            Update();
        }

        public static void Draw()
        {     
            Buffer.Graphics.Clear(Color.FromArgb(Game.bgRed, Game.bgGreen, Game.bgBlue));
            foreach (SSBaseObject obj in _objs)
                obj.Draw();
            //не перерисовываем стартовый экран, если открыто окно с игрой
            if(!Program.Count())            
                Buffer.Render();
        }

        public static void Update()
        {
            foreach (SSBaseObject obj in _objs)
                obj.Update();
        }

        public static void Load()
        {
            _objs = new SSBaseObject[30];
            for (int i = 0; i < _objs.Length; i = i + 3)
                _objs[i] = new SSPlanet(new Point(600, i * 20), new Point(-i-1, -i-1), new Size(10, 10));
            for (int i = 1; i < _objs.Length; i = i + 3)
                _objs[i] = new SSNebula(new Point(i*50, 0), new Point(0, -2*i), new Size(10, 10));
            for (int i = 2; i < _objs.Length; i = i + 3)
                _objs[i] = new SSStar(new Point(600, i * 20), new Point(-i, 0), new Size(10, 10));
        }

    }
}
