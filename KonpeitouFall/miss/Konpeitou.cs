using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using oicf.math;

namespace KonpeitouFall2
{
    class Konpeitou : Form
    {
        private int colorNumber = 1;
        static public System.Windows.Forms.Screen screen;
        private int ColorNumber
        {
            get
            {
                return colorNumber;
            }
            set
            {
                if (value is > 0 and < 6) colorNumber = value;
            }
        }
        Vector2d defaltPos;
        Bitmap img;
        public Konpeitou(int num, Vector2d pos)
        {
            ColorNumber = num;
            defaltPos = pos;
            //画像を読み込む
            img = new Bitmap($"D:\\program\\app\\windowsUI\\KonpeitouFall\\KonpeitouFall\\img\\output{ColorNumber}.png");

            this.ShowInTaskbar = false;
            //フォームの境界線をなくす
            this.FormBorderStyle = FormBorderStyle.None;
            //フォームを背景色で透過させる
            this.TransparencyKey = BackColor;
            this.Top = (int)defaltPos.y;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //大きさを適当に変更
            this.Size = new Size(img.Width, img.Height);
            img.MakeTransparent();
            this.BackgroundImage = img;


            this.TopMost = true;
            animation();
        }
        private void animation()
        {
            this.Top = (int)defaltPos.y;
            this.Left = (int)defaltPos.x;
            Animator.Animate(5000, (frame, frequency) =>
            {
                if (!Visible || IsDisposed || this.Top>this.Height+ screen.Bounds.Height) return false;
                this.Top += (int)((screen.Bounds.Height + this.Height + 1400) / frequency);
                return true;
            },()=> { animation(); });
        }

    }
}
