namespace GUISocket
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public class DataEventArgs : EventArgs
        {
            public string Data { set; get; }

        }
        public event EventHandler<DataEventArgs> DataSent;
        protected virtual void OnDataSent(DataEventArgs e)
        {
            DataSent?.Invoke(this, e);
        }

        private void btnLoginOK_Click(object sender, EventArgs e)
        {
            DataEventArgs args = new DataEventArgs { Data = textBox1.Text };
            OnDataSent(args);
            this.Close();

        }
    }
}
