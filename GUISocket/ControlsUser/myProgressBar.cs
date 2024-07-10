using System.ComponentModel;

namespace GUISocket.ControlsUser
{
    public enum TextPosition
    {
        Left,
        Right,
        Center,
        Sliding,
        None
    }

    internal class myProgressBar : ProgressBar
    {
        //Fields
        //-> Appearance
        private Color channelColor = Color.LightSteelBlue;
        private Color sliderColor = Color.RoyalBlue;
        private Color foreBackColor = Color.RoyalBlue;
        private int channelHeight = 20;
        private int sliderHeightoffset = 10;
        private TextPosition showValue = TextPosition.Right;
        private string symbolBefore = "";
        private string symbolAfter = "";
        private bool showMaximun = false;

        //-> Others
        private bool paintedBack = false;
        private bool stopPainting = false;

        //Constructor
        public myProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.ForeColor = Color.White;
            channelHeight = this.Height - 5;
        }
        [Category("HDR Properties")]
        public Color ChannelColor { get => channelColor; set { channelColor = value; this.Invalidate(); } }
        [Category("HDR Properties")]
        public Color SliderColor { get => sliderColor; set { sliderColor = value; this.Invalidate(); } }
        [Category("HDR Properties")]
        public Color ForeBackColor { get => foreBackColor; set { foreBackColor = value; this.Invalidate(); } }
        [Category("HDR Properties")]
        public int ChannelHeight
        {
            get => channelHeight;
            set
            {
                if (value < 20) value = 20;
                channelHeight = value;
                this.Invalidate();
            }
        }
        [Category("HDR Properties")]
        public int SliderHeightoffset
        {
            get => sliderHeightoffset;
            set
            {
                if (value < 10) value = 10;
                sliderHeightoffset = value;
                this.Invalidate();
            }
        }
        [Category("HDR Properties")]
        public TextPosition ShowValue { get => showValue; set { showValue = value; this.Invalidate(); } }
        [Category("HDR Properties")]
        public string SymbolBefore { get => symbolBefore; set { symbolBefore = value; this.Invalidate(); } }
        [Category("HDR Properties")]
        public string SymbolAfter { get => symbolAfter; set { symbolAfter = value; this.Invalidate(); } }
        [Category("HDR Properties")]
        public bool ShowMaximun { get => showMaximun; set { showMaximun = value; this.Invalidate(); } }


        [Category("HDR Properties")]
        [Browsable(true)] //A visual designer typically displays in the Properties window those members that either have no browsable attribute or are marked with the BrowsableAttribute constructor's browsable parameter set to true. 
        [EditorBrowsable(EditorBrowsableState.Always)] // helps intilisence to dispaly the property 
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
            }
        }

        [Category("HDR Properties")]
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

        // Windows erase request is handlled here
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (stopPainting == false)
            {
                if (paintedBack == false)
                {
                    int sliderHeight = channelHeight - sliderHeightoffset;
                    Graphics graph = pevent.Graphics;
                    Rectangle rectChannel = new Rectangle(0, 0, this.Width, ChannelHeight);
                    SolidBrush brushChannel = new SolidBrush(channelColor);
                    if (channelHeight >= sliderHeight)
                        rectChannel.Y = this.Height - channelHeight;
                    else rectChannel.Y = this.Height - ((channelHeight + sliderHeight) / 2);

                    //Painting
                    graph.Clear(this.Parent.BackColor);//Surface
                    graph.FillRectangle(brushChannel, rectChannel);//Channel

                    //Stop painting the back & Channel
                    if (this.DesignMode == false)
                        paintedBack = true;
                    brushChannel.Dispose();
                }
            }
            //Reset painting the back & channel
            if (this.Value == this.Maximum || this.Value == this.Minimum)
                paintedBack = false;
        }
        //-> Paint slider
        protected override void OnPaint(PaintEventArgs e)
        {
            if (stopPainting == false)
            {
                //Fields
                int sliderHeight = channelHeight - sliderHeightoffset;

                Graphics graph = e.Graphics;

                double scaleFactor = (((double)this.Value - this.Minimum) / ((double)this.Maximum - this.Minimum));
                int sliderWidth = (int)(this.Width * scaleFactor);
                Rectangle rectSlider = new Rectangle(0, 0, sliderWidth, sliderHeight);

                using (var brushSlider = new SolidBrush(sliderColor))
                {
                    if (sliderHeight >= channelHeight)
                        rectSlider.Y = this.Height - sliderHeight;
                    else rectSlider.Y = this.Height - ((sliderHeight + channelHeight) / 2);

                    //Painting
                    if (sliderWidth > 1) //Slider
                    {
                        graph.FillRectangle(brushSlider, rectSlider);
                    }

                    if (showValue != TextPosition.None) //Text
                        DrawValueText(graph, sliderWidth, rectSlider);
                }
            }
            if (this.Value == this.Maximum) stopPainting = true;//Stop painting
            else stopPainting = false; //Keep painting
        }

        private void DrawValueText(Graphics graph, int sliderWidth, Rectangle rectSlider)
        {
            //Fields
            string text = symbolBefore + this.Value.ToString() + symbolAfter;
            if (showMaximun) text = text + "/" + symbolBefore + this.Maximum.ToString() + symbolAfter;
            var textSize = TextRenderer.MeasureText(text, this.Font);
            var rectText = new Rectangle(0, 0, textSize.Width, textSize.Height + 2);
            using (var brushText = new SolidBrush(this.ForeColor))
            using (var brushTextBack = new SolidBrush(foreBackColor))
            using (var textFormat = new StringFormat())
            {
                switch (showValue)
                {
                    case TextPosition.Left:
                        rectText.X = 0;
                        textFormat.Alignment = StringAlignment.Near;
                        break;

                    case TextPosition.Right:
                        rectText.X = this.Width - textSize.Width;
                        textFormat.Alignment = StringAlignment.Far;
                        break;

                    case TextPosition.Center:
                        rectText.X = (this.Width - textSize.Width) / 2;
                        textFormat.Alignment = StringAlignment.Center;
                        break;

                    case TextPosition.Sliding:
                        rectText.X = sliderWidth - textSize.Width;
                        textFormat.Alignment = StringAlignment.Center;
                        //Clean previous text surface
                        using (var brushClear = new SolidBrush(this.Parent.BackColor))
                        {
                            var rect = rectSlider;
                            rect.Y = rectText.Y;
                            rect.Height = rectText.Height;
                            graph.FillRectangle(brushClear, rect);
                        }
                        break;
                }
                //Painting
                graph.FillRectangle(brushTextBack, rectText);
                graph.DrawString(text, this.Font, brushText, rectText, textFormat);
            }
        }

        protected override void OnResize(EventArgs e)
        {

            if (channelHeight < 20)
                channelHeight = 20;
            base.OnResize(e);
        }

    }

}
