using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Match_
{
    class Searcher
    {
        List<Entry> entries = new List<Entry>();
        private int start;
        private int end;

        private List<int[]> edges = new List<int[]>();
        
        public Task _task;

        public Searcher(int _start, int _end)
        {
            start = _start;
            end = _end;
        }

        public Searcher(List<int[]> _edges)
        {
            edges = _edges;
        }

        public void Search()
        {
            if (edges.Count > 0)
            {
                foreach (var j in edges)
                {
                    foreach (var token in Main.tokens)
                    {
                        int ind = -1;
                        ind = Main.targetText.IndexOf(token, j[0], j[1] - j[0]);
                        if (ind > -1)
                        {
                            entries.Add(new Entry(token, 0, ind));
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            else
            {
                foreach (var token in Main.tokens)
                {
                    int ind = -1;
                    ind = Main.targetText.IndexOf(token, start, end - start);
                    if (ind > -1)
                    {
                        entries.Add(new Entry(token, 0, ind));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            
            Main.PullEntries(entries);
        }

        public void Start()
        {
            _task = new Task(Search);
            _task.Start();
        }
    }
}
