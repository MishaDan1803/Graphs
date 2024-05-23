// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static List<Vertex> vtxs = new List<Vertex>();
        static List<Edge> edges = new List<Edge>();

        string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public Graphics graphics;

        bool isFirstVtx = false;

        public Form1()
        {
            InitializeComponent();

            label1.Text = String.Empty;
            label2.Text = String.Empty;
        }


        private Vertex firstVertex, secondVertex;
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Vertex targetVertex = FindClickVertex(e);

                if (targetVertex != null) 
                    if (isFirstVtx == false)
                    {
                        isFirstVtx = true;
                        label1.Text = "Выбрана первая вершина - выберите вторую";
                        firstVertex = targetVertex;
                    }
                    else if (isFirstVtx == true)
                    {
                        isFirstVtx = false;
                        label1.Text = "Ребро построено";
                        secondVertex = targetVertex;

                        int weight = 0;
                        WeightForm weightForm = new WeightForm();
                        if (weightForm.ShowDialog() == DialogResult.OK) weight = weightForm.Weight;

                        Edge newEdge = new Edge(firstVertex, secondVertex, weight, alphabet[edges.Count].ToString());
                        newEdge.DrawEdge(this);
                        edges.Add(newEdge);

                        firstVertex.linkVertices.Add(secondVertex);
                        secondVertex.linkVertices.Add(firstVertex);
                    } 
            }
            else if (checkBox1.Checked == false)
            {
                int x, y;
                x = e.X;
                y = e.Y;


                Vertex newVertex = new Vertex(x, y, vtxs.Count);
                vtxs.Add(newVertex);
                newVertex.DrawVertex(graphics, Color.Red);
            }
        }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics = CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form newForm = new Form2(vtxs, edges);
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hamiltonian hamiltonian = new Hamiltonian(vtxs);
            label2.Text = hamiltonian.Find();
        }

        public Vertex FindClickVertex(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            for (int i = 0; i < vtxs.Count(); i++)
                if (Math.Pow((vtxs[i].X - x), 2) + Math.Pow((vtxs[i].Y - y), 2) <= 15 * 15) 
                    return vtxs[i];

            return null;
        }

        
    }
}
