using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva
{
    static partial class Game
    {
        /// <summary>
        /// Метод, создающий астероиды
        /// </summary>
        /// <param name="astcount">количество создаваемых астероидов</param>        public static void SpawnAsteroids(int astcount)
        {
            Random rand = new Random();
            int x, y, vx, vy;

            for (int i = 0; i < astcount; i++)
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
                _asteroids.Add(new Asteroid(new Point(x, y), new Point(vx, vy), new Size(20, 20)));
            }
        }
    }
}
