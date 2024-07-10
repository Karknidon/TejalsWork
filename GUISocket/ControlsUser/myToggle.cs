using System.ComponentModel;
using System.Drawing.Drawing2D;


namespace GUISocket.ControlsUser
{
    internal class myToggle : CheckBox
    {
        // Fields
        private Color onBackColor = Color.MediumSlateBlue;
        private Color onToggelColor = Color.WhiteSmoke;
        private Color offBackColor = Color.DarkGray;
        private Color offToggleColor = Color.Gainsboro;



        [Category("Custom Control")]
        public Color OnBackColor { get => onBackColor; set { onBackColor = value; this.Invalidate(); } }
        [Category("Custom Control")]
        public Color OnToggelColor { get => onToggelColor; set { onToggelColor = value; this.Invalidate(); } }
        [Category("Custom Control")]
        public Color OffBackColor { get => offBackColor; set { offBackColor = value; this.Invalidate(); } }
        [Category("Custom Control")]
        public Color OffToggleColor { get => offToggleColor; set { offToggleColor = value; this.Invalidate(); } }


        public override string Text { get => base.Text; }



        // Constructor
        public myToggle()
        {
            this.MinimumSize = new Size(45, 22);

        }

        //Methods
        private GraphicsPath GetFigurePath()
        {

            int archSize = this.Height - 1;
            Rectangle leftArch = new Rectangle(0, 0, archSize, archSize);
            Rectangle rightArch = new Rectangle(this.Width - archSize - 2, 0, archSize, archSize);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftArch, 90, 180);
            path.AddArc(rightArch, 270, 180);
            path.CloseFigure();
            return path;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {


            int toggleSize = this.Height - 5;
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(this.Parent.BackColor); // clear the graphics

            if (this.Checked)// true
            {
                g.FillPath(new SolidBrush(onBackColor), GetFigurePath());// Create the slot
                g.FillEllipse(new SolidBrush(onToggelColor),
                    new Rectangle(this.Width - this.Height - 2, 2, toggleSize, toggleSize));// create the inner circle

            }
            else
            {
                g.FillPath(new SolidBrush(offBackColor), GetFigurePath());// Create the slot
                g.FillEllipse(new SolidBrush(offToggleColor),
                    new Rectangle(2, 2, toggleSize, toggleSize));// create the inner circle
            }




        }
    }
}
