using System;
using System.Collections.Generic;
using System.Data;

namespace CBClient.Library
{
    public class Dijkstra
    {        
        public const double VOCUNG = double.PositiveInfinity;
        public int SoNut;        
        Dictionary<int, string> _dicGaID = new Dictionary<int, string>();
        Dictionary<string, int> _dicNodeID = new Dictionary<string, int>();
        List<string> _gaList = new List<string>();
        double[,] _weight;

        public Dijkstra()
        {
            Initialize();
        }
        void Initialize()
        {
            DataView dvNetwork = AppGlobal.LookupDS.Tables["Network"].Copy().DefaultView;
            SoNut = dvNetwork.Count;
            _weight = new double[SoNut, SoNut];
            for (int i = 0; i < SoNut; i++)
                for (int j = 0; j < SoNut; j++)
                {
                    if (i == j)
                        _weight[i, j] = 0; // duong cheo
                    else
                        _weight[i, j] = double.PositiveInfinity;
                }
            // Khởi tạo hai dictionary đển map GaID<->NodeID
            foreach (DataRowView Row in dvNetwork)
            {
                
                _dicGaID.Add(int.Parse(Row["NodeID"].ToString()), Row["GaID"].ToString());
                _dicNodeID.Add(Row["GaID"].ToString(), int.Parse(Row["NodeID"].ToString()));
                _gaList.Add(Row["GaID"].ToString());
            }
            //Khoi tao mang weight                
            foreach (DataRowView Row in dvNetwork)
            {
                int _rowidx = int.Parse(Row["NodeID"].ToString());
                int _colidx = 0;
                double _cost = 0;
                if (!string.IsNullOrWhiteSpace(Row["Link1"].ToString()))
                {
                    _cost = double.Parse(Row["Cost1"].ToString());
                    _colidx = _dicNodeID[Row["Link1"].ToString()];
                    _weight[_rowidx, _colidx] = _cost;
                }
                if (!string.IsNullOrWhiteSpace(Row["Link2"].ToString()))
                {
                    _cost = double.Parse(Row["Cost2"].ToString());
                    _colidx = _dicNodeID[Row["Link2"].ToString()];
                    _weight[_rowidx, _colidx] = _cost;
                }
                if (!string.IsNullOrWhiteSpace(Row["Link3"].ToString()))
                {
                    _cost = double.Parse(Row["Cost3"].ToString());
                    _colidx = _dicNodeID[Row["Link3"].ToString()];
                    _weight[_rowidx, _colidx] = _cost;
                }
                if (!string.IsNullOrWhiteSpace(Row["Link4"].ToString()))
                {
                    _cost = double.Parse(Row["Cost4"].ToString());
                    _colidx = _dicNodeID[Row["Link4"].ToString()];
                    _weight[_rowidx, _colidx] = _cost;
                }
            }           
        }

        public List<string> GaList
        {
            get { return _gaList; }
        }

        public void TinhToan(string gaDi, string gaDen, out double length, out string strPath)
        {
            try
            {
                // Tap cac nut da xet
                int start = _dicNodeID[gaDi];
                int goal = _dicNodeID[gaDen];
                int[] path = new int[SoNut];
                length = 0;
                int currentNode, k = 0;
                double min, dist, newdist;
                bool[] Visited = new bool[SoNut];
                double[] TotalCost = new double[SoNut];//kcachngan1

                for (int i = 0; i < SoNut; i++)
                {
                    Visited[i] = false;
                    TotalCost[i] = double.PositiveInfinity;                    
                }

                Visited[start] = true;
                TotalCost[start] = 0;
                currentNode = start;

                while (currentNode != goal)
                {
                    min = double.PositiveInfinity;
                    dist = TotalCost[currentNode];
                    for (int i = 0; i < SoNut; i++)
                    {
                        if (Visited[i] == false)
                        {
                            newdist = dist + _weight[currentNode, i];
                            if (newdist < TotalCost[i])
                            {
                                TotalCost[i] = newdist;
                                path[i] = currentNode;
                            }
                            if (TotalCost[i] < min)
                            {
                                min = TotalCost[i];
                                k = i;
                            }
                        }
                    }
                    currentNode = k;
                    Visited[currentNode] = true;
                }
                length = TotalCost[k];
                int n = goal;
                strPath = "";
                Stack<int> stackPath = new Stack<int>();
                while (n != start)
                {                    
                    stackPath.Push(n);
                    n = path[n];
                }                
                while (stackPath.Count > 0)
                {
                    int _nodeID = stackPath.Pop();
                    strPath += "," + _dicGaID[_nodeID];
                }
                strPath = gaDi + strPath;
            }
            catch (Exception)
            {
                throw new Exception("Lỗi tính km chạy");
            }
        }

        public string GetPath(string gaDi, string gaDen, bool boGaDi)
        {
            // Tap cac nut da xet
            string _path = "";
            int start = _dicNodeID[gaDi];
            int goal = _dicNodeID[gaDen];
            int[] path = new int[SoNut];
            int currentNode, k = 0;
            double min, dist, newdist;
            bool[] Visited = new bool[SoNut];
            double[] TotalCost = new double[SoNut];//kcachngan1

            for (int i = 0; i < SoNut; i++)
            {
                Visited[i] = false;
                TotalCost[i] = double.PositiveInfinity;                
            }

            Visited[start] = true;
            TotalCost[start] = 0;
            currentNode = start;

            while (currentNode != goal)
            {
                min = double.PositiveInfinity;
                dist = TotalCost[currentNode];
                for (int i = 0; i < SoNut; i++)
                {
                    if (Visited[i] == false)
                    {
                        newdist = dist + _weight[currentNode, i];
                        if (newdist < TotalCost[i])
                        {
                            TotalCost[i] = newdist;
                            path[i] = currentNode;
                        }
                        if (TotalCost[i] < min)
                        {
                            min = TotalCost[i];
                            k = i;
                        }
                    }
                }
                currentNode = k;
                Visited[currentNode] = true;
            }
            int n = goal;
            Stack<int> stackPath = new Stack<int>();
            while (n != start)
            {                
                stackPath.Push(n);
                n = path[n];
            }            
            while (stackPath.Count > 0)
            {
                int _nodeID = stackPath.Pop();
                _path += ", " + _dicGaID[_nodeID];
            }
            if (boGaDi)
                return _path;
            _path = gaDi + _path;
            return _path;
        }

        public bool checkvongBacHong(string gaDi, string gaDen)
        {
            bool checkVong = false;            
            //string cacGa = "vandien, hadong, phudien, kimno, bachong";
            string cacGa = "hadong,phudien,kimno";
            if(cacGa.Contains(gaDi)||cacGa.Contains(gaDen))
                checkVong = true;
            return checkVong;
        }
    }
    public struct GaLink
    {        
        public GaLink(string gaID, decimal linkCost)
        {
            this.GaID = gaID;
            this.LinkCost = linkCost;
        }
        public string GaID;
        public decimal LinkCost;
    }
}
