using System;
using System.Drawing;
using System.Windows.Forms;

namespace GRBL.Controls
{
    public class JoggingKnob : Control
    {
        private Rectangle Knob;

        private const int ControlSize = 160;
        private const int KnobSize = 40;
        private int Middle { get { return ((ControlSize / 2) - (KnobSize / 2)); } }
        private const int DivKnobSize = KnobSize / 2;
        private const int DivControlSize = ControlSize / 2;

        private bool MouseOnKnob = false;
        private Point MouseLocation;

        public Action StartJogging, StopJogging;
        public float ValX, ValY;
        public bool LockX = false, LockY = false;

        public JoggingKnob()
        {
            Size = new Size(ControlSize, ControlSize);
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnResize(EventArgs e)
        {
            Knob = new Rectangle(Middle, Middle, KnobSize, KnobSize);

            if (Width != ControlSize)
                Width = ControlSize;

            if (Height != ControlSize)
                Height = ControlSize;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!Enabled)
                return;

            if (Knob.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                MouseOnKnob = true;

                if (StartJogging != null)
                    StartJogging.Invoke();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!Enabled)
                return;

            MouseOnKnob = false;

            if (StopJogging != null)
                StopJogging.Invoke();

            Knob = new Rectangle(Middle, Middle, KnobSize, KnobSize);
            ValX = 0;
            ValY = 0;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!Enabled)
                return;

            MouseLocation = e.Location;

            if (MouseOnKnob)
            {
                int knobX = MouseLocation.X - DivKnobSize;
                int knobY = MouseLocation.Y - DivKnobSize;

                if (LockX)
                {
                    knobX = Middle;
                }
                else
                {
                    if (MouseLocation.X - DivKnobSize < 0)
                        knobX = 0;
                    else if (MouseLocation.X + DivKnobSize > ClientRectangle.Width)
                        knobX = ClientRectangle.Width - KnobSize;
                }

                if(LockY)
                {
                    knobY = Middle;
                }
                else
                {
                    if (MouseLocation.Y - DivKnobSize < 0)
                        knobY = 0;
                    else if (MouseLocation.Y + DivKnobSize > ClientRectangle.Height)
                        knobY = ClientRectangle.Height - KnobSize;
                }

                float x = (knobX + DivKnobSize) - DivControlSize;
                float y = DivControlSize - (knobY + DivKnobSize);

                ValX = (x / (DivControlSize - DivKnobSize)) * 100;
                ValY = (y / (DivControlSize - DivKnobSize)) * 100;

                ValX = ValX / 100;
                ValY = ValY / 100;

                Knob = new Rectangle(knobX, knobY, KnobSize - 1, KnobSize - 1);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            using(Pen p = new Pen(new SolidBrush(Color.Black), 1))
            {
                e.Graphics.DrawRectangle(p, Knob);

                e.Graphics.DrawString("X:" + ValX.ToString("F1"), Font, new SolidBrush(Color.Black),
                    10, 145);
                e.Graphics.DrawString("Y:" + ValY.ToString("F1"), Font, new SolidBrush(Color.Black),
                    50, 145);
            }
        }
    }
}
