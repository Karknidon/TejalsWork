namespace GUISocket
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = Devloper.Name1;
            label2.Text = Devloper.email;
            label3.Text = Devloper.Organization;
            label4.Text = Devloper.Adress;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}