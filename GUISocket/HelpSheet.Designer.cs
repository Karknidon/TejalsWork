namespace GUISocket
{
    partial class HelpSheet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lblHelpCmd = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 31);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // lblHelpCmd
            // 
            lblHelpCmd.AutoSize = true;
            lblHelpCmd.Location = new Point(39, 55);
            lblHelpCmd.Name = "lblHelpCmd";
            lblHelpCmd.Size = new Size(38, 15);
            lblHelpCmd.TabIndex = 1;
            lblHelpCmd.Text = "label2";
            // 
            // HelpSheet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblHelpCmd);
            Controls.Add(label1);
            Name = "HelpSheet";
            Text = "HelpSheet";
            Load += HelpSheet_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblHelpCmd;
    }
}