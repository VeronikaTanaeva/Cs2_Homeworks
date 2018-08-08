﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame_Tanaeva
{
    static partial class Game
    {
        /// <summary>
        /// Обновлении информации о местоположении объектов на экране игры
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            foreach (Bullet b in _bullets)
            {
                b.Update();
            }
            for (int i = 0; i < _asteroids.Count; i++)
            {
                _asteroids[i].Update();
                for (int j = 0; j < _bullets.Count; j++)
                {
                    if (_bullets[j].OutOfScreen())
                    {
                        _bullets[j] = null; //пуля при пересечении границы экрана исчезает
                        _bullets.RemoveAt(j);
                        continue;
                    }
                    if (_bullets[j] == null) continue;
                    if (_asteroids[i] != null && _bullets[j].Collision(_asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        actionTarget("Астероид сбит!", sw);
                        _asteroids[i].CollisionUpdate(_asteroids[i]);// = null; //при столкновении с пулей или кораблём астероид разрушается
                        _asteroids.RemoveAt(i);
                        _bullets[j] = null; //пуля при попадании исчезает
                        _bullets.RemoveAt(j);
                        j--;
                        score++; //при попадании в астероид начисляются очки
                        continue;
                    }
                }
                if (_asteroids.Count == 0 || !_ship.Collision(_asteroids[i])) continue; 
                var rnd = new Random();
                if (_ship.Collision(_asteroids[i])) //если астероид врезается в корабль, у корабля тратится энергия, а астероид пропадает
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    actionTarget("Корабль подбит!", sw);
                    _ship?.EnergyLow(rnd.Next(1, 10));
                    _asteroids[i].CollisionUpdate(_asteroids[i]); //при столкновении с пулей или кораблём астероид разрушается
                    _asteroids.RemoveAt(i);
                    _ship.CollisionUpdate(_ship);
                }
                if (_ship.Collision(_medkit)) //при столкновении корабля и аптечки у корабля восстанавливается энергия, аптечка тут же заново отрисовывается в другом месте вдоль левой части экрана
                {
                    actionTarget("Аптечка подобрана!", sw);
                    _ship.EnergyAdd(rnd.Next(1, 10));
                    _medkit.CollisionUpdate(_medkit);
                }

                if (_asteroids.Count == 0)//спавним новые астероиды, если старые закончились
                {
                    astcount++;
                    SpawnAsteroids(astcount);
                }

                if (_ship.Energy <= 0) _ship?.Die();
            }
        }
    }
}