using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame_Tanaeva
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;

        static Game()
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
            //не перерисовываем окно с игрой, если оно закрыто
            if (Program.Count())
                Buffer.Render(); 
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }


        public static void Load()
        {
            _objs = new BaseObject[60];
            for (int i = 0; i < _objs.Length; i=i+4)
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i-1, -i-1), new Size(10, 10));
            for (int i = 1; i < _objs.Length; i=i+4)
                _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(10, 10));
            for (int i = 2; i < _objs.Length; i = i + 4)
                _objs[i] = new PlanetImage(new Point(i*i, i*20), new Point(-8, -9), new Size(30, 30));
            for (int i = 3; i < _objs.Length; i = i + 4)
                _objs[i] = new Nebula(new Point(i*i, 10), new Point(-10, -i/2), new Size(10, 10));

        }
    }
}