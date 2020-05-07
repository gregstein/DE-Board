using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DEBoarded;
using Newtonsoft.Json;

namespace DEBoard
{

    public partial class Form1 : KryptonForm
    {
        public int _lgtimeOUT = 5000;
        public bool netCHECK = true;
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size((this.Width - kryptonDataGridView1.Width) + kryptonDataGridView1.Width, this.Height);
            timer1.Start();
            timer2.Start();
        }

        public int pageBEGIN = 1;
        public async Task<string> DownloadStringAsync(Uri uri, int timeOut = 60000)
        {
            string output = null;
            bool cancelledOrError = false;
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Proxy = null;
                client.DownloadStringCompleted += (sender, e) =>
                {
                    if (e.Error != null || e.Cancelled)
                    {
                        cancelledOrError = true;
                    }
                    else
                    {
                        output = e.Result;
                    }
                };
                client.DownloadStringAsync(uri);
                
                var n = DateTime.Now;
                while (output == null && !cancelledOrError && DateTime.Now.Subtract(n).TotalMilliseconds < timeOut)
                {
                    
                    await Task.Delay(100); // wait for respsonse
                }
                
            }
           
            return await Task.FromResult(output);
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public static string GetTypeLadder(string num)
        {
            switch (num)
            {
                case "0":
                    return "Random Map";
                case "1":
                    return "Regicide";
                case "2":
                    return "Deathmatch";
                case "13":
                    return "Empire Wars";
                default:
                    return "Custom";
            }


        }
        public string _playerPROFILES(DEBoarded.Player[] list)
        {


            foreach (var pl in list)
            {
                if (pl != null)
                {

                    return pl.Name;

                }
            }

            return "";


        }
        private  void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = ColorTranslator.FromHtml("#717171");
            richTextBox1.SelectedRtf = DEBoarded.Properties.Resources.Readme;

        }

        private void kryptonDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void kryptonDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (kryptonDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && kryptonDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Join")
            {
                //MessageBox.Show(kryptonDataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                Process.Start("explorer.exe", kryptonDataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
        }

        private void Lobbies_ParentChanged(object sender, EventArgs e)
        {

        }

        private void Lobbies_Click(object sender, EventArgs e)
        {
            this.Size = new Size((this.Width - kryptonDataGridView1.Width) + kryptonDataGridView1.Width, this.Height);
        }

        private void Lobbies_Initialized(object sender, EventArgs e)
        {
            this.Size = new Size((this.Width - kryptonDataGridView1.Width) + kryptonDataGridView1.Width, this.Height);
        }

        private void kryptonPage2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonPage2_Load(object sender, EventArgs e)
        {

        }

        private void kryptonPage2_Enter(object sender, EventArgs e)
        {

        }
        public static string streaker(long streak)
        {
            if (streak > 0)
                return @"+" + streak;
            else
                return streak.ToString();

        }
        public async Task FetchLeaderboard(int page)
        {
            kryptonDataGridView2.Rows.Clear();
            Uri apiLOB = new Uri(@"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=3&start=" + page + "&count=10");
            string jsonLOBBIES = await DownloadStringAsync(apiLOB);
            //MessageBox.Show(jsonLOBBIES.Substring(0, 8));
            var ldb = LeaderBoard.FromJson(jsonLOBBIES);

            int i = 1;
            foreach (var l in ldb.Leaderboard)
            {
                i++;
                LinkLabel ln = new LinkLabel();
                Button btn = new Button();
                ln.Text = "View";
                ln.Name = "view" + i.ToString();
                btn.Name = "btn" + i.ToString();
                btn.Text = "Join";
                kryptonDataGridView2.Rows.Add(l.Rank, l.Rating, l.Name, l.Games, streaker(l.Streak), l.Wins, l.Drops + @"%", "View", @"https://www.ageofempires.com/stats/?profileId=" + l.ProfileId + @"&game=age2");


            }
        }
        private async void kryptonPage2_EnabledChanged(object sender, EventArgs e)
        {
            await FetchLeaderboard(1);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {


        }

        private async void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                kryptonDataGridView2.DataSource = null;

                await Task.Delay(1000);
                //await FetchLeaderboard(trackBar1.Value * 10);
            }
            catch (SystemException)
            {

            }
        }
        private string filterPage = String.Empty;
        private async void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            await Task.Delay(200);
            if(filterPage != kryptonTextBox1.Text)
            {
                filterPage = kryptonTextBox1.Text;
            int n;

            if (int.TryParse(kryptonTextBox1.Text, out n))
            {
                await FetchLeaderboard(Int32.Parse(kryptonTextBox1.Text));

            }
            else
            {
                MessageBox.Show("Please Enter a number!");
            }
            }
        }

        private async void nextPAGE_Click(object sender, EventArgs e)
        {
            await Task.Delay(100);
            int n;
            if (int.TryParse(kryptonTextBox1.Text, out n))
                if (Int32.Parse(kryptonTextBox1.Text) >= 10)
                {
                    kryptonDataGridView2.Rows.Clear();
                    kryptonTextBox1.Text = (Int32.Parse(kryptonTextBox1.Text) + 10).ToString();


                }

        }

        private async void prevPAGE_Click(object sender, EventArgs e)
        {
            await Task.Delay(100);
            int n;
            if (int.TryParse(kryptonTextBox1.Text, out n))
                if (Int32.Parse(kryptonTextBox1.Text) >= 10)
                {
                    kryptonDataGridView2.Rows.Clear();
                    kryptonTextBox1.Text = (Int32.Parse(kryptonTextBox1.Text) - 10).ToString();
                }

        }

        private void kryptonDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView2.SelectedCells[8].Value.ToString());

                }
            }
            catch (SystemException)
            {
            }
        }
        private string filterText = String.Empty;
        private async void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
            kryptonDataGridView3.Rows.Clear();
            await Task.Delay(500);
            
            if (filterText != kryptonTextBox2.Text)
            {
                filterText = kryptonTextBox2.Text;
            Uri apiLOB = new Uri(@"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=3&start=1&search=" + kryptonTextBox2.Text);
            string jsonLOBBIES = await DownloadStringAsync(apiLOB);
            //MessageBox.Show(jsonLOBBIES.Substring(0, 8));
            var ldb = QueryPlayer.FromJson(jsonLOBBIES);

            int i = 1;
            foreach (var l in ldb.Leaderboard)
            {
                i++;
                LinkLabel ln = new LinkLabel();
                Button btn = new Button();
                ln.Text = "View";
                ln.Name = "view" + i.ToString();
                btn.Name = "btn" + i.ToString();
                btn.Text = "Join";
                kryptonDataGridView3.Rows.Add(l.Rank, l.Name, l.Rating, l.Games, "View", "View", @"https://www.ageofempires.com/stats/?profileId=" + l.ProfileId + @"&game=age2", @"http://steamcommunity.com/profiles/" + l.SteamId);
            }
                RemoveDuplicate(kryptonDataGridView3);
            }
            if(kryptonTextBox2.Text == null)
            {
                kryptonDataGridView3.Rows.Clear();
            }
        }
        public void RemoveDuplicate(DataGridView grv)
        {
            for (int currentRow = 0; currentRow < grv.Rows.Count - 1; currentRow++)
            {
                DataGridViewRow rowToCompare = grv.Rows[currentRow];

                for (int otherRow = currentRow + 1; otherRow < grv.Rows.Count; otherRow++)
                {
                    DataGridViewRow row = grv.Rows[otherRow];

                    bool duplicateRow = true;

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        if (!rowToCompare.Cells[cellIndex].Value.Equals(row.Cells[cellIndex].Value))
                        {
                            duplicateRow = false;
                            break;
                        }
                    }

                    if (duplicateRow)
                    {
                        grv.Rows.Remove(row);
                        otherRow--;
                    }
                }
            }
        }
        private void kryptonDataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView3.SelectedCells[6].Value.ToString());

                }
                if (e.ColumnIndex == 5)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView3.SelectedCells[7].Value.ToString());

                }
            }
            catch (SystemException)
            {
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            //Customize timeout
            timer1.Interval = _lgtimeOUT;

            Uri apiLOB = new Uri(@"https://aoe2.net/api/lobbies?game=aoe2de");
            string jsonLOBBIES = await DownloadStringAsync(apiLOB);
            if(jsonLOBBIES != null)
            {
                var lobBrowser = LobBrowser.FromJson(jsonLOBBIES);
                kryptonDataGridView1.Rows.Clear();
                int i = 1;
                foreach (var l in lobBrowser)
                {
                    i++;

                    Button btn = new Button();
                    btn.Name = "btn" + i.ToString();
                    btn.Text = "Join";
                    kryptonDataGridView1.Rows.Add(btn, GetTypeLadder(l.GameType.ToString()), l.Name, l.AverageRating, "steam://joinlobby/813780/" + l.LobbyId + "/" + l.LeaderboardId, l.NumPlayers + "/" + l.NumSlots, _playerPROFILES(l.Players));


                }
                pictureBox1.Visible = false;
                if (kryptonPage2.Enabled == false)
                    kryptonPage2.Enabled = true;

                //Connect Status
                if (CheckForInternetConnection())
                {
                    kryptonHeader1.Values.Image = DEBoarded.Properties.Resources.online;
                    kryptonHeader1.Values.Description = "(Online)";
                }
            }
            else
            {
                kryptonHeader1.Values.Image = DEBoarded.Properties.Resources.offline;
                kryptonHeader1.Values.Description = "(Offline)";
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 60000;
            if (CheckForInternetConnection())
            {
                kryptonHeader1.Values.Image = DEBoarded.Properties.Resources.online;
                kryptonHeader1.Values.Description = "(Online)";
            }
            else
            {
                kryptonHeader1.Values.Image = DEBoarded.Properties.Resources.offline;
                kryptonHeader1.Values.Description = "(Offline)";

            }
                
        }

        private void LRinterval_SelectedItemChanged(object sender, EventArgs e)
        {
           
        }

        private void LRinterval_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void cbINTER_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lgtimeOUT = int.Parse(cbINTER.Text) * 1000;
        }

        private void intCHECK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (intCHECK.Text == "Yes")
                netCHECK = false;
            else
                netCHECK = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}