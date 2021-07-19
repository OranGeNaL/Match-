using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Match_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "В качестве разделителя необходимо использовать перенос строки!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Main.searchFile = openFileDialog.FileName;
                    textBox1.Text = Main.searchFile;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Main.sourceFile = openFileDialog.FileName;
                    textBox2.Text = Main.sourceFile;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BackTask();
            //Task backTask = new Task(BackTask);
            //backTask.Start();
        }

        private void BackTask()
        {
            Main.tokens.Clear();
            ReadTokens();
            ReadText();

            List<Searcher> searchers = new List<Searcher>();
            List<int[]> edges = new List<int[]>();

            for (int i = 0; (i ) * Main.partSize < Main.targetText.Length; i++)
            {
                if ((i + 1) * Main.partSize > Main.targetText.Length)
                {
                    searchers.Add(new Searcher(i * Main.partSize, Main.targetText.Length - 1));
                    continue;
                }
                
                int rightEdge = (i + 1) * Main.partSize - 1;
                searchers.Add(new Searcher(i * Main.partSize, rightEdge));
                edges.Add(new []{rightEdge - 15, rightEdge + 15});
            }
            
            searchers.Add(new Searcher(edges));

            foreach (var i in searchers)
            {
                i.Start();
            }
            
            foreach (var i in searchers)
            {
                i._task.Wait();
            }
            
            WriteEntries();
            Thread.Sleep(1000);
        }

        private void WriteEntries()
        {
            string fileName = /*Directory.GetCurrentDirectory() + "\\" +*/ DateTime.Now.ToString("yy-MM-dd") + "_result.txt";
            using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.Default))
            {
                foreach (var i in Main.publicEntries)
                {
                    sw.WriteLine(i.ToString());
                }
            }

            Main.lastResult = fileName;
        }
        private void ReadTokens()
        {
            using (StreamReader sr = new StreamReader(Main.searchFile, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Main.tokens.Add(line);
                }
            }
        }

        private void ReadText()
        {
            using (StreamReader sr = new StreamReader(Main.sourceFile))
            {
                Main.targetText = sr.ReadToEnd();
            }
        }
    }
}
