// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MaxFlow
    {
        static int V;

        public MaxFlow(int v) 
        {
            V = v;
        }
        public bool Bfs(int[,] rGraph, int s, int t, int[] parent)
        {
            // Создайте посещенный массив и отметьте
            // все вершины как не посещенные
            bool[] visited = new bool[V];
            for (int i = 0; i < V; ++i)
                visited[i] = false;

            // Создайте очередь, поставьте исходную вершину в очередь и отметьте
            // исходную вершину как посещенную
            List<int> queue = new List<int>();
            queue.Add(s);
            visited[s] = true;
            parent[s] = -1;

            while (queue.Count != 0)
            {
                int u = queue[0];
                queue.RemoveAt(0);

                for (int v = 0; v < V; v++)
                {
                    if (visited[v] == false
                        && rGraph[u, v] > 0)
                    {
                        // Если мы найдем соединение с приемником
                        // узла, то в BFS нет смысла
                        // больше нам просто нужно установить его родительским
                        // и может возвращать значение true
                        if (v == t)
                        {
                            parent[v] = u;
                            return true;
                        }
                        queue.Add(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            // Мы не достигли нужного, поэтому вернем false
            return false;
        }

        public int FordFulkerson(int[,] graph, int s, int t)
        {
            int u, v;

            // Создайте остаточный график и заполните
            // остаточный график с заданным
            // мощности в исходном графике как
            // остаточные мощности в остаточном графике

            // Остаточный граф, где граф[i,j]
            // указывает остаточную емкость
            // ребра от i до j (если есть
            // ребро. Если график[i,j] равен 0, то
            // его нет)
            int[,] rGraph = new int[V, V];

            for (u = 0; u < V; u++)
                for (v = 0; v < V; v++)
                    rGraph[u, v] = graph[u, v];

            // Этот массив заполняется BFS и для хранения пути
            int[] parent = new int[V];

            int max_flow = 0;

            while (Bfs(rGraph, s, t, parent))
            {
                // Найти минимальную остаточную емкость ребер
                // по пути, заполненному BFS.
                int path_flow = int.MaxValue;
                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    path_flow
                        = Math.Min(path_flow, rGraph[u, v]);
                }

                // обновите остаточные емкости ребер и
                // переверните ребра вдоль пути
                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    rGraph[u, v] -= path_flow;
                    rGraph[v, u] += path_flow;
                }
                max_flow += path_flow;
            }

            return max_flow;
        }
    }
}
