using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUISocket
{
    public partial class HelpSheet : Form
    {
        public HelpSheet()
        {
            InitializeComponent();
        }

        private void HelpSheet_Load(object sender, EventArgs e)
        {

            lblHelpCmd.Text = "OS: Source sent to Origin Home\nOI: Indexer sent to Origin Home\n";
            lblHelpCmd.Text = lblHelpCmd.Text + "Ex: Erase Encoder no x: x=I,S, D\n";
            lblHelpCmd.Text = lblHelpCmd.Text + "MSFn: Move Source Foward by n counts\n";
            lblHelpCmd.Text = lblHelpCmd.Text + "MSRn: Move Source Reverse by n counts\n";
            lblHelpCmd.Text = lblHelpCmd.Text + "MIFn: Move Indexer Foward by n counts\n";
            lblHelpCmd.Text = lblHelpCmd.Text + "MIRn: Move Indexer Reverse by n counts\n";
            lblHelpCmd.Text = lblHelpCmd.Text + "AS: Seek Start  adapter position\n";
            lblHelpCmd.Text = lblHelpCmd.Text + "AE: Seek End   adapter position \n";
            lblHelpCmd.Text = lblHelpCmd.Text + "I Initilize the Indeser encoder\n";
            
        }
    }
}
