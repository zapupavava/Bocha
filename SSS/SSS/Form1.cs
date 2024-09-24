using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SSS
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(400, 400);
            this.Text = "Розовая звезда";
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;
            SetFormToStar();
            this.Paint += new PaintEventHandler(MainForm_Paint);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        private void SetFormToStar()
        {
            GraphicsPath path = new GraphicsPath();
            PointF[] points = GetStarPoints(5, 200, 100);
            path.AddPolygon(points);
            this.Region = new Region(path);
        }

        private PointF[] GetStarPoints(int num_points, float outer_radius, float inner_radius)
        {
            PointF[] points = new PointF[num_points * 2];
            double angle_step = Math.PI / num_points;
            double angle_offset = -Math.PI / 2;

            for (int i = 0; i < num_points * 2; i++)
            {
                double angle = i * angle_step + angle_offset;
                float radius = (i % 2 == 0) ? outer_radius : inner_radius;
                points[i] = new PointF(
                    (float)(ClientSize.Width / 2 + radius * Math.Cos(angle)),
                    (float)(ClientSize.Height / 2 + radius * Math.Sin(angle))
                );
            }
            return points;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush pinkBrush = Brushes.Pink;
            PointF[] starPoints = GetStarPoints(5, 200, 100);
            g.FillPolygon(pinkBrush, starPoints);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
