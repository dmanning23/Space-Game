using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame_Mono
{
    class EnemyManager
    {
        
        public EnemyManager()
        {

        }

        public void spawnEnemy(Enemy enemy)
        {
            bool succes = true;
            if (Main.instance.EnemyList.Count >= 1)
            {
                for (int j = 0; j < Main.instance.EnemyList.Count; j++)
                {
                    Enemy xEnemy = Main.instance.EnemyList[j];
                    if (HitManager.KollisionSprite(enemy.getSkin(), enemy.getLocation(), xEnemy.getSkin(), xEnemy.getLocation()))
                    {
                        succes = false;
                        break;

                    }
                }
                if (succes == true)
                {
                    Main.instance.EnemyList.Add(enemy);
                }

            }
            else
            {
                succes = true; //unötig
                Main.instance.EnemyList.Add(enemy);
            }

        }
    }
}
