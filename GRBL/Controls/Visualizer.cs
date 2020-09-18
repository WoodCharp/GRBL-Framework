using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRBL.Controls
{
    public class Visualizer : Control
    {
        public Color BackgroundColor { get; set; } = Color.WhiteSmoke;
        public Color DrawColor { get; set; } = Color.Black;
        public Color DimensionColor { get; set; } = Color.Gray;
        public Color ToolColor { get; set; } = Color.DarkRed;
        public Color PathColor { get; set; } = Color.Blue;

        public List<string> FileLines { get; set; } = null;
        private List<G_CODE> GCODE_LINES;
        private List<Point> drawPoints;

        private int _VisualizerScale = 2;
        public int VisualizerScale { get { return _VisualizerScale; } set{ _VisualizerScale = value; Invalidate(); } }

        private Rectangle ToolPositionRect;
        public int ToolDiameter { get; set; } = 6;
        private int ToolSize { get { return ToolDiameter * VisualizerScale; } }
        private int ZeroPointOffset { get { return 80; } }

        private int MeasureGap { get { return 10 * VisualizerScale; } }

        public Visualizer()
        {
            Size = new Size(500, 500);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                //Up
                if(_VisualizerScale > 1)
                    _VisualizerScale--;
            }
            else
            {
                //Down
                _VisualizerScale++;
            }

            Invalidate();
        }

        public void ReadLines()
        {
            if (FileLines == null)
                return;

            if (FileLines.Count <= 0)
                return;

            GCODE_LINES = new List<G_CODE>();
            drawPoints = new List<Point>();

            int i = 1;
            int lastX = 0, lastY = 0;
            foreach(string line in FileLines)
            {
                if(!string.IsNullOrEmpty(line))
                {
                    if(line[0] != '(')
                    {
                        Regex Gcode = new Regex("[gxyzfijk][+-]?[0-9]*\\.?[0-9]*", RegexOptions.IgnoreCase);
                        MatchCollection m = Gcode.Matches(line);

                        G_CODE g_CODE = new G_CODE();
                        g_CODE.N = i;
                        foreach (Match ma in m)
                        {
                            if(ma.Value[0] == 'G')
                            {
                                g_CODE.G.Add(int.Parse(ma.Value.Remove(0, 1)));
                            }

                            if (ma.Value[0] == 'X')
                            {
                                g_CODE.X = float.Parse(ma.Value.Remove(0, 1).Replace('.',','));
                            }

                            if (ma.Value[0] == 'Y')
                            {
                                g_CODE.Y = float.Parse(ma.Value.Remove(0, 1).Replace('.', ','));
                            }

                            if (ma.Value[0] == 'Z')
                            {
                                g_CODE.Z = float.Parse(ma.Value.Remove(0, 1).Replace('.', ','));
                            }

                            if (ma.Value[0] == 'F')
                            {
                                g_CODE.F = int.Parse(ma.Value.Remove(0, 1));
                            }

                            if (ma.Value[0] == 'I')
                            {
                                g_CODE.I = float.Parse(ma.Value.Remove(0, 1).Replace('.', ','));
                            }

                            if (ma.Value[0] == 'J')
                            {
                                g_CODE.J = float.Parse(ma.Value.Remove(0, 1).Replace('.', ','));
                            }
                            
                            if (ma.Value[0] == 'K')
                            {
                                g_CODE.K = float.Parse(ma.Value.Remove(0, 1).Replace('.', ','));
                            }
                        }

                        GCODE_LINES.Add(g_CODE);

                        drawPoints.Add(
                                new Point(GCODE_LINES[0].X != null ? (int)GCODE_LINES[0].X : lastX,
                                GCODE_LINES[0].Y != null ? (int)GCODE_LINES[0].Y : lastY));


                        if(g_CODE.X != null)
                            lastX = (int)g_CODE.X;
                        if(g_CODE.Y != null)
                            lastY = (int)g_CODE.Y;
                    }
                }

                i++;
            }

            Invalidate();
        }

        public void UpdatePosition(Point position)
        {
            position = new Point(position.X * VisualizerScale, position.Y * VisualizerScale);

            ToolPositionRect = new Rectangle(
                ZeroPointOffset + ((position.X - (ToolSize / 2))),
                -(position.Y + (ToolSize / 2)) + (ClientRectangle.Height - ZeroPointOffset),
                ToolSize, ToolSize);

            Invalidate();
        }

        private Point DrawNewPoint(Point old)
        {
            return new Point(
                ZeroPointOffset + (old.X * VisualizerScale),
                -((old.Y * VisualizerScale)) + (ClientRectangle.Height - ZeroPointOffset));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackgroundColor);

            using(Pen p = new Pen(DrawColor, 1))
            {
                e.Graphics.DrawRectangle(p, 0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            }

            using(Pen dPen = new Pen(DimensionColor, 1))
            {
                e.Graphics.DrawLine(dPen, ZeroPointOffset, 0, ZeroPointOffset, ClientRectangle.Height - ZeroPointOffset);
                e.Graphics.DrawLine(dPen, ZeroPointOffset, ClientRectangle.Height - ZeroPointOffset,
                    ClientRectangle.Width, ClientRectangle.Height - ZeroPointOffset);

                int pointsX = ClientRectangle.Width / MeasureGap;
                int pointsY = ClientRectangle.Height / MeasureGap;

                for (int x = 0; x < pointsX + 1; x++)
                {
                    e.Graphics.DrawLine(dPen,
                        ZeroPointOffset + (MeasureGap * x),
                        ClientRectangle.Height - ZeroPointOffset,
                        ZeroPointOffset + (MeasureGap * x),
                        (ClientRectangle.Height - ZeroPointOffset) + 5);

                    e.Graphics.DrawString(((MeasureGap * x) / VisualizerScale).ToString(),
                        new Font("Arial", 10), new SolidBrush(DimensionColor),
                        ZeroPointOffset + (MeasureGap * x),
                        ClientRectangle.Height - ZeroPointOffset);
                }

                for (int y = 0; y < pointsY + 1; y++)
                {
                    e.Graphics.DrawLine(dPen,
                        ZeroPointOffset,
                        (ClientRectangle.Height - ZeroPointOffset) - (MeasureGap * y),
                        ZeroPointOffset - 5,
                        (ClientRectangle.Height - ZeroPointOffset) - (MeasureGap * y));

                    if (y == 0)
                    {
                        e.Graphics.DrawString("  " + ((MeasureGap * y) / VisualizerScale).ToString(),
                            new Font("Arial", 10), new SolidBrush(DimensionColor),
                            ZeroPointOffset - 18,
                            (ClientRectangle.Height - (ZeroPointOffset + 18)) - (MeasureGap * y));
                    }
                    else
                    {
                        e.Graphics.DrawString(((MeasureGap * y) / VisualizerScale).ToString(),
                            new Font("Arial", 10), new SolidBrush(DimensionColor),
                            ZeroPointOffset - 18,
                            (ClientRectangle.Height - (ZeroPointOffset + 18)) - (MeasureGap * y));
                    }
                }
            }

            if(drawPoints != null)
            {
                if(drawPoints.Count > 0)
                {
                    using(Pen pathPen = new Pen(PathColor, 1))
                    {
                        for (int i = 0; i < drawPoints.Count; i++)
                        {
                            if(i > 0)
                            {
                                e.Graphics.DrawLine(pathPen,
                                DrawNewPoint(drawPoints[i - 1]), DrawNewPoint(drawPoints[i]));
                            }
                        }
                    }
                }
            }

            if (ToolPositionRect != null)
            {
                using (Pen toolPen = new Pen(ToolColor, 1))
                {
                    e.Graphics.DrawEllipse(toolPen, ToolPositionRect);
                }
            }           
        }
    }
}
