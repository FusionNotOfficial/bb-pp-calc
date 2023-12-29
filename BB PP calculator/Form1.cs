using ULControls;

namespace BB_PP_calculator
{
    public partial class Bbppc : Form
    {
        private int temp;
        private int total;

        public Bbppc()
        {
            InitializeComponent();
        }

        private void calcButton_Click(object sender, EventArgs e)
        {
            int smallAmount = 0, middleAmount = 0, bigAmount = 0;
            smallAmount += Calculate(gs);
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
            resultPanel.Visible = true;
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
    }
}