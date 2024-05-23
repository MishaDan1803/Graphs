// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Hamiltonian
    {
        private List<Vertex> vertices;
        public Hamiltonian(List<Vertex> vertices)
        {
            this.vertices = vertices;
        }

        private bool[] visitedVtx;
        public string Find()
        {
            string paths = "";

            visitedVtx = new bool[vertices.Count];
            
            
            foreach (var vertex in vertices)
            {
                for (int i = 0; i < visitedVtx.Length; i++) visitedVtx[i] = false;
                visitedVtx[vertex.Index] = true;

                List<Vertex> path = new List<Vertex>();
                path.Add(vertex);

                bool isEndPaths = false;
                Vertex currentVtx = vertex;
                //0 - есть связь, но не просмотрен
                //1 - есть связь, но просмотрена
                //2 - связи нет
                int[,] levels = new int[vertices.Count, vertices.Count];
                for (int i = 0; i < vertices.Count; i++)
                    for (int j = 0; j < vertices.Count; j++) levels[i, j] = 2;

                while (!isEndPaths)
                {
                    foreach (var linkVtx in currentVtx.linkVertices)
                    {
                        if (levels[currentVtx.Index, linkVtx.Index] == 2) levels[currentVtx.Index, linkVtx.Index] = 0;
                        if (path.Contains(linkVtx)) levels[currentVtx.Index, linkVtx.Index] = 1;
                    }
                    bool isBack = true;
                    foreach (var linkVtx in currentVtx.linkVertices)
                    {
                        
                        if (levels[currentVtx.Index, linkVtx.Index] == 0)
                        {
                            levels[currentVtx.Index, linkVtx.Index] = 1;
                            levels[linkVtx.Index, currentVtx.Index] = 1;
                            currentVtx = linkVtx;

                            isBack = false;
                            path.Add(currentVtx);
                            break;
                        }

                        if (path.Count == vertices.Count && vertex.linkVertices.Contains(path[path.Count - 1]))
                        {
                            foreach (var vertexPath in path) paths += vertexPath.Index + " - ";

                            paths += vertex.Index;
                            paths += "\n";

                            isBack = false;
                            isEndPaths = true;
                            break;
                        }
                    }

                    if (isBack == true)
                    {
                        if (path.Count > 1)
                        {
                            path.RemoveAt(path.Count - 1);
                            for (int i = 0; i < vertices.Count; i++) levels[currentVtx.Index, i] = 2;
                            currentVtx = path.Last();
                        }
                        else isEndPaths = true;
                    }
                }
            }

            if (paths == "") return "Не найдено";
            else return paths;
        }
    }
}
