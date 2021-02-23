using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace test1
{
    public partial class Form1 : Form
    {
        //Controls.
        private Label[] driveLabels = new Label[26];
        private Label[] sizeLabels = new Label[26];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer driveTimer = new Timer();
            driveTimer.Interval = 1000;
            driveTimer.Start();
            driveTimer.Tick += new EventHandler(driveTimer_Tick);
            int startY = 0;            
            for (int i = 0; i < 26; i++)
            {
                driveLabels[i] = new Label();
                driveLabels[i].Text = ((char)(i+65)).ToString()+":\\";
                this.driveLabels[i].Location = new System.Drawing.Point(0, startY);
                this.driveLabels[i].Size = new System.Drawing.Size(90, 30);
                float currentSize = driveLabels[i].Font.Size;
                currentSize += 2.0F;
                driveLabels[i].Font = new Font(driveLabels[i].Font.Name, currentSize, driveLabels[i].Font.Style, driveLabels[i].Font.Unit);
                this.Controls.Add(driveLabels[i]);

                sizeLabels[i] = new Label();
                sizeLabels[i].Text = "---";
                this.sizeLabels[i].Location = new System.Drawing.Point(90, startY);
                this.sizeLabels[i].Size = new System.Drawing.Size(130, 30);
                this.Controls.Add(sizeLabels[i]);
                currentSize = sizeLabels[i].Font.Size;
                currentSize += 1.0F;
                sizeLabels[i].Font = new Font(sizeLabels[i].Font.Name, currentSize, sizeLabels[i].Font.Style, sizeLabels[i].Font.Unit);
                startY += 30;
            }
            this.Size = new Size(236, startY+39);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void driveTimer_Tick(object sender, EventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            Dictionary<string, DriveInfo> drivesByNames = new Dictionary<string, DriveInfo>();
            for (int i = 0; i < allDrives.Length; i++)
            {
                drivesByNames.Add(allDrives[i].Name, allDrives[i]);
            }
            for (int i = 0; i < sizeLabels.Length; i++)
            {
                if (drivesByNames.ContainsKey(driveLabels[i].Text)){
                    driveLabels[i].BackColor = Color.LightGreen;
                    sizeLabels[i].BackColor = Color.LightBlue;
                    sizeLabels[i].Text = (drivesByNames[driveLabels[i].Text].AvailableFreeSpace/1024).ToString()+" MBs";
                }
                else
                {
                    driveLabels[i].BackColor = Color.LightGray;
                    sizeLabels[i].BackColor = Color.LightGray;
                    sizeLabels[i].Text = "---";
                }
            }
        }
    }
}
