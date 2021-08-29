using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using oicf.math;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KonpeitouFall
{

    public partial class Form1 : Form
    {



        NotifyIcon notifyIcon;
        List<Konpeitou> konpeitous;

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private const int WEIGHT = 17;

        private void UpdatePanel(Graphics g, int width, int height)
        {

            g.Clear(BackColor);
            foreach(var elem in konpeitous)
            {
                elem.Move();
                elem.Draw(ref g);
            }
        }


        private void setComponents()
        {
            notifyIcon = new NotifyIcon();

            String str = System.IO.Path.GetFullPath($".\\img\\icon.ico");
            notifyIcon.Icon = new Icon(str);
            notifyIcon.Visible = true;
            notifyIcon.Text = "金平糖の降る頃に";


            // コンテキストメニュー
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "&終了";
            toolStripMenuItem.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem);
            notifyIcon.ContextMenuStrip = contextMenuStrip;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // アプリケーションの終了
            Application.Exit();
        }
        public Form1()
        {
            InitializeComponent();
            this.setComponents();
            //フォームの境界線をなくす
            this.FormBorderStyle = FormBorderStyle.None;

            //フォームを背景色で透過させる
            this.TransparencyKey = BackColor;
            Konpeitou.screen = Screen.FromControl(this);
            for (int i = 1; i < 6; i++)
            {
                String str = System.IO.Path.GetFullPath($".\\img\\output{i}.png");
                Konpeitou.imgs.Add(new Bitmap(str));
            }



            Random random = new Random();
            konpeitous = new List<Konpeitou>();
            for (int i = 0; i < 100; i++)
            {
                konpeitous.Add(new Konpeitou(random.Next(0, 5), new Vector2d(random.Next(0, 1920), -random.Next(40, 100))));
            }
            sw = new System.Diagnostics.Stopwatch();
            this.isAlive = true;
            this.ShowInTaskbar = false;
            size = Screen.GetBounds(this).Size;

            this.Top = -1;
            this.Left = 0;
            this.Size = size;
            this.panel1.Size = this.Size;
            
            Task t = Task.Run(this.MainThread);
        }
        protected override void OnShown(EventArgs e)
        {
            //base.OnShown(e);

            this.TopMost = true;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            this.isAlive = false;
            base.OnClosing(e);
        }

        private Stopwatch sw;
        private bool isAlive;
        private Bitmap backBuffer;
        private Size size;
        private async void MainThread()
        {

            while (this.isAlive)
            {
                // Taskの実行
                Task task1 = Task.Run(() => {
                    lock (this)
                    {
                        if (this.backBuffer == null)
                        {
                            this.backBuffer = new Bitmap(this.panel1.Width, this.panel1.Height);

                            using (Graphics gfx = Graphics.FromImage(this.backBuffer))
                            {
                                gfx.Clear(BackColor);
                            }
                        }
                        using (Graphics g = Graphics.FromImage(this.backBuffer))
                            this.UpdatePanel(g, this.panel1.Width, this.panel1.Height);
                    }
                    this.panel1.Invalidate();
                });

                Task task2 = Task.Delay(WEIGHT);
                // task1, task2が終わるまで待機
                await Task.WhenAll(task1, task2);
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            lock (this)
            {
                if (this.backBuffer != null)
                {
                    this.backBuffer.Dispose();
                    this.backBuffer = null;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            lock (this)
            {
                if (this.backBuffer != null)
                {
                    e.Graphics.DrawImageUnscaled(this.backBuffer, 0, 0);
                }
            }
        }

    }
}

