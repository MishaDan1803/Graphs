// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Edge
    { 
        public int Weight;
        public string Name;
        public int Flow;
        
        public Vertex Vertex1, Vertex2;
        public List<Vertex> Vertices = new List<Vertex>();

        public Edge(Vertex vertex1, Vertex vertex2, int weight, string name)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;

            Vertices.Add(vertex1);
            Vertices.Add(vertex2);

            Weight = weight;
            Name = name;
        }   

        public void DrawEdge(Form1 form1)
        {
            Pen pen = new Pen(Color.Gray, 2f);

            Bitmap bmp = new Bitmap(50, 20);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawString($"( {Name} ; {Weight.ToString()})", new Font("Tahoma", 10), Brushes.Black, 0, 0);

            int x, y;
            if (Vertex1 != Vertex2)
            {
                Pen p = new Pen(Color.DarkOrange, 4f);
                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                form1.graphics.DrawLine(p, Vertex1.X, Vertex1.Y, Vertex2.X, Vertex2.Y);

                x = (Vertex1.X + Vertex2.X) / 2;
                y = (Vertex1.Y + Vertex2.Y) / 2;
            }
            else
            {
                form1.graphics.DrawEllipse(pen, Vertex1.X - 40, Vertex1.Y - 80, 80, 80);

                x = Vertex1.X;
                y = Vertex1.Y - 40;
            }

            form1.graphics.DrawImage(bmp, x - 25, y - 10);
        }
    }
}
