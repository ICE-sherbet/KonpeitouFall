using System.Windows.Forms;


namespace KonpeitouFall
{
    class DoubleBufferingPanel : Panel
    {

        public DoubleBufferingPanel()
        {
            //ダブルバッファリングを有効にする
            this.SetStyle(
               ControlStyles.DoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
        }
    }
}
