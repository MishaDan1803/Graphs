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
    public class Vertex 
    {
        public List<Vertex> linkVertices = new List<Vertex>();

        public int Index;

        Graphics g;

        public int X , Y;

        public Vertex(int x, int y, int index)
        {
            X = x;
            Y = y;
            Index = index;
        } 
        
        public void DrawVertex(Graphics formGraph, Color color)
        {
            Bitmap bmp = new Bitmap(30, 30);
            
            g = Graphics.FromImage(bmp);
            Pen pen = new Pen(color, 2f);
            g.DrawEllipse(pen, 0, 0, 28, 28);

            if (Index.ToString().Length < 2) g.DrawString(Index.ToString(), new Font("Tahoma", 15), Brushes.Black, 6, 1);
            else g.DrawString(Index.ToString(), new Font("Tahoma", 15), Brushes.Black, 1, 0);

            formGraph.DrawImage(bmp, X - 15, Y - 15);
        }
    }
}
