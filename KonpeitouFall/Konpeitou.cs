using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using oicf.math;

namespace KonpeitouFall
{
    class Konpeitou
    {
        private int colorNumber = 1;
        static public System.Windows.Forms.Screen screen;
        static public List<Bitmap> imgs = new List<Bitmap>();
        private int ColorNumber
        {
            get{return colorNumber;}
            set{if (value is > 0 and < 6) colorNumber = value;}
        }
        Vector2d defaltPos;
        Vector2d Position;
        Bitmap img;
        int waitFrame = 20;
        int waitCounter = 0;
        bool isWait = true;
        static Random random = new System.Random();
        public Konpeitou(int num, Vector2d pos)
        {
            waitFrame = random.Next(1, 300);
            ColorNumber = num;
            defaltPos = pos;
            //画像を読み込む
            img = imgs[ColorNumber];
            Position = new Vector2d(defaltPos);
            img.MakeTransparent();
        }
        public void Move()
        {
            if (!isWait)
            {
                Position.y+=5;
            }
            else
            {
                waitCounter++;
                if(waitFrame== waitCounter)
                {
                    waitFrame = random.Next(1, 300);
                    isWait = false;
                    waitCounter = 0;
                }
            }
            if (Position.y > screen.Bounds.Height + img.Height)
            {
                Position = new Vector2d(defaltPos);
                isWait = true;
            }
        }

        public void Draw(ref Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            gr.DrawImage(img, new Rectangle((int)Position.x, (int)Position.y, img.Width, img.Height));

        }
    }
}
