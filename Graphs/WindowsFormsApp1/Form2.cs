namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        List<Vertex> vertices = new List<Vertex>();
        List<Edge>   edges = new List<Edge>();

        public Form2(List<Vertex> vertices, List<Edge> edges)
        {
            InitializeComponent();
            this.vertices = vertices;
            this.edges = edges;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            label1.Visible = false;

            DataGridView dgv =  dataGridView1;

            dgv.ColumnCount = vertices.Count;
            dgv.RowCount = vertices.Count;

            for (int i = 0; i < vertices.Count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = vertices[i].Index.ToString();
                dgv.Columns[i].HeaderText = vertices[i].Index.ToString();
            }

            for (int i = 0; i < vertices.Count; i++)
                for (int j = 0; j < vertices.Count; j++)
                    dgv.Rows[i].Cells[j].Value = 0;

            for (int i = 0; i < edges.Count; i++)
            {
                dgv.Rows[edges[i].Vertex1.Index].Cells[edges[i].Vertex2.Index].Value = edges[i].Weight;
                dgv.Rows[edges[i].Vertex1.Index].Cells[edges[i].Vertex2.Index].Style.BackColor = Color.LightGreen;
            }              

            DataGridView dgv2 = dataGridView2;

            dgv2.ColumnCount = vertices.Count;
            dgv2.RowCount = edges.Count;

            for (int i = 0; i < vertices.Count; i++) dgv2.Columns[i].HeaderText = vertices[i].Index.ToString();
            for (int i = 0; i < edges.Count; i++) dgv2.Rows[i].HeaderCell.Value = edges[i].Name;

            for (int i = 0; i < edges.Count; i++)
                for (int j = 0; j < vertices.Count; j++)
                    if (edges[i].Vertices.Contains(vertices[j]))
                    {
                        dgv2.Rows[i].Cells[j].Value = 1;
                        dgv2.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        dgv2.Rows[i].Cells[j].Value = 0;
                    }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,] matrixVtxs = new int[vertices.Count, vertices.Count];

            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    matrixVtxs[i, j] = int.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString());
                }

            MaxFlow maxFlowClass = new MaxFlow(vertices.Count);
            int firstVtx = int.Parse(comboBox1.SelectedItem.ToString());
            int lastVtx = int.Parse(comboBox2.SelectedItem.ToString());
            int maxFlow = maxFlowClass.FordFulkerson(matrixVtxs, firstVtx, lastVtx);

            label1.Visible = true;
            label1.Text = ": " + maxFlow.ToString();
        }

        private void InitializeComboBox()
        {
            foreach (var vtx in vertices)
            {
                comboBox1.Items.Add(vtx.Index);
                comboBox2.Items.Add(vtx.Index);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
