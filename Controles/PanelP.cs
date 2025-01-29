using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsoDocs
{
    public class PanelP:Panel
    {
        // Propiedades para el borde
        private int borderSize = 2;
        private int borderRadius = 20;
        private Color borderColor = Color.Black;

        public int BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; this.Invalidate(); }
        }

        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; this.Invalidate(); }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        public PanelP()
        {
            // Habilitar doble buffer para evitar parpadeos
            this.DoubleBuffered = true;
            this.Resize += (s, e) => this.Invalidate(); // Redibuja el panel cuando se redimensiona
        }

        // Sobrescribir el método OnPaint para personalizar el dibujo
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2) // Panel con bordes redondeados
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Superficie del panel (fondo)
                    this.Region = new Region(pathSurface);

                    // Rellenar el fondo del panel con su color de fondo
                    pevent.Graphics.FillPath(new SolidBrush(this.BackColor), pathSurface);

                    // Dibujar borde interior para suavizar las esquinas
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    // Dibujar el borde del panel
                    if (borderSize >= 1)
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else // Panel normal sin bordes redondeados
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;

                // Superficie del panel (fondo)
                this.Region = new Region(rectSurface);

                // Dibujar el borde del panel
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }

        // Método para crear un GraphicsPath con esquinas redondeadas
        private GraphicsPath GetFigurePath(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2;

            path.StartFigure();
            path.AddArc(rectangle.X, rectangle.Y, diameter, diameter, 180, 90);
            path.AddArc(rectangle.Right - diameter, rectangle.Y, diameter, diameter, 270, 90);
            path.AddArc(rectangle.Right - diameter, rectangle.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rectangle.X, rectangle.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
