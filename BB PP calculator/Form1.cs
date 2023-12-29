using System.Runtime.InteropServices;
using ULControls;

namespace BB_PP_calculator
{
    public partial class Bbppc : Form
    {
        private int temp;
        private int total;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        bool sidebarExpand;

        public Bbppc()
        {
            InitializeComponent();
        }

        private void calcButton_Click(object sender, EventArgs e)
        {
            int smallAmount = 0, middleAmount = 0, bigAmount = 0;

            if (resultPanel.Height != resultPanel.MaximumSize.Height) // Moving bottom panel only when it's necessary
                sidebarTimer.Start();

            smallAmount += Calculate(gs); // Calculations block
            middleAmount += Calculate(gm);
            bigAmount += Calculate(gl);
            smallAmount += Calculate(bs);
            middleAmount += Calculate(bm);
            bigAmount += Calculate(bl);
            smallAmount += Calculate(rs);
            middleAmount += Calculate(rm);
            bigAmount += Calculate(rl);
            smallAmount += Calculate(ps);
            middleAmount += Calculate(pm);
            bigAmount += Calculate(pl);

            total = smallAmount + middleAmount * 7 + bigAmount * 49;
            ppTotal.Text = (total / 49).ToString();
            timeTotal.Text = (((smallAmount / 7 + middleAmount) / 7) + bigAmount / 7 * 10).ToString() + " Hours";
            total = 0;
        }
        private int Calculate(object sender)
        {
            var obj = sender as ULTextBox;
            bool result = int.TryParse(obj.Texts, out temp);
            if (temp < 0)
            {
                temp = 0;
                obj.Texts = String.Empty;
            }
            return result ? temp : 0;
        }
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                resultPanel.Height -= 10;
                if (resultPanel.Height == resultPanel.MinimumSize.Height)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                resultPanel.Height += 10;
                if (resultPanel.Height == resultPanel.MaximumSize.Height)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void resultPanel_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}