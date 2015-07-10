using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame_Mono
{
    class HitManager
    {
        public static bool KollisionRechteck(Rectangle a, Rectangle b)
        {
            return (a.Right > b.Left && a.Left < b.Right && a.Bottom > b.Top && a.Top < b.Bottom);
        }

        //a -> erstes Objekt | aPos -> Position erstes Objekt | b -> zweites Objekt | bPos -> Position zweites Objekt
        public static bool KollisionSprite(Texture2D a, Vector2 aPos, Texture2D b, Vector2 bPos)
        {
            bool kollision = false;
            //kollisionsrechteck für beide objekte erzeugen
            Rectangle aRec = new Rectangle((int)aPos.X, (int)aPos.Y, a.Width, a.Height);
            Rectangle bRec = new Rectangle((int)bPos.X, (int)bPos.Y, b.Width, b.Height);

            //wenn die beiden rechtecke sich überlappen auf alpawerte prüfen
            if (KollisionRechteck(aRec, bRec))
            {
                uint[] bitsA = new uint[a.Width * a.Height];
                a.GetData<uint>(bitsA);
                uint[] bitsB = new uint[b.Width * b.Height];
                b.GetData<uint>(bitsB);

                int x1 = Math.Max((int)aPos.X, (int)bPos.X);
                int x2 = Math.Min((int)aPos.X + a.Width, (int)bPos.X + b.Width);
                int y1 = Math.Max((int)aPos.Y, (int)bPos.Y);
                int y2 = Math.Min((int)aPos.Y + a.Height, (int)bPos.Y + b.Height);

                for (int y = y1; y < y2; ++y)
                {
                    for (int x = x1; x < x2; ++x)
                    {
                        if (((bitsA[(x - (int)aPos.X) + (y - (int)aPos.Y) * a.Width] & 0xFF000000) >> 24) > 20 && ((bitsB[(x - (int)bPos.X) + (y - (int)bPos.Y) * b.Width] & 0xFF000000) >> 24) > 20)
                        {
                            kollision = true;
                        }
                    }
                }
            }

            return kollision;
        }
    }
}
