using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WinFormsStopwatch
{
    public class StopwatchForm : Form
    {
        private Timer timer;
        private int secondsElapsed;
        
        private Label lblTime;
        private Button btnStartStop;
        private Button btnReset;
        private Button btnLap;
        private Button btnSave;
        private ListBox lstLaps;

        public StopwatchForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Stopwatch";
            this.Size = new Size(350, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Step 1: Timer that will tick every second
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;

            // Time Label
            lblTime = new Label();
            lblTime.Text = "00:00:00";
            lblTime.Font = new Font("Arial", 36, FontStyle.Bold);
            lblTime.AutoSize = true;
            lblTime.Location = new Point(50, 20);

            // Step 2: Start/Stop Button
            btnStartStop = new Button();
            btnStartStop.Text = "Start";
            btnStartStop.Location = new Point(20, 100);
            btnStartStop.Click += BtnStartStop_Click;

            // Step 3: Reset Button
            btnReset = new Button();
            btnReset.Text = "Reset";
            btnReset.Location = new Point(120, 100);
            btnReset.Click += BtnReset_Click;

            // Step 4: Lap Button
            btnLap = new Button();
            btnLap.Text = "Lap";
            btnLap.Location = new Point(220, 100);
            btnLap.Click += BtnLap_Click;

            // Step 4: Lap ListBox (to record the current time)
            lstLaps = new ListBox();
            lstLaps.Location = new Point(20, 150);
            lstLaps.Size = new Size(275, 150);

            // Step 5: Save Button (to save lap times to a file)
            btnSave = new Button();
            btnSave.Text = "Save Laps";
            btnSave.Location = new Point(20, 320);
            btnSave.Size = new Size(275, 30);
            btnSave.Click += BtnSave_Click;

            // Add controls to form
            this.Controls.Add(lblTime);
            this.Controls.Add(btnStartStop);
            this.Controls.Add(btnReset);
            this.Controls.Add(btnLap);
            this.Controls.Add(lstLaps);
            this.Controls.Add(btnSave);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            TimeSpan time = TimeSpan.FromSeconds(secondsElapsed);
            lblTime.Text = time.ToString(@"hh\:mm\:ss");
        }

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                // Stop the timer and change text to Start
                timer.Stop();
                btnStartStop.Text = "Start";
            }
            else
            {
                // Start the timer and change text to Stop
                timer.Start();
                btnStartStop.Text = "Stop";
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset the timer and change labels back to 00:00:00
            timer.Stop();
            secondsElapsed = 0;
            lblTime.Text = "00:00:00";
            btnStartStop.Text = "Start";
            lstLaps.Items.Clear();
        }

        private void BtnLap_Click(object sender, EventArgs e)
        {
            // Record the current time and display it in a listbox
            lstLaps.Items.Add($"Lap {lstLaps.Items.Count + 1}: {lblTime.Text}");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Save the lap times to a file
            if (lstLaps.Items.Count == 0)
            {
                MessageBox.Show("No laps to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter("LapTimes.txt"))
                {
                    writer.WriteLine("--- Saved Lap Times ---");
                    writer.WriteLine($"Saved on: {DateTime.Now}");
                    writer.WriteLine("-----------------------");
                    foreach (var item in lstLaps.Items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
                MessageBox.Show("Lap times saved successfully to LapTimes.txt!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StopwatchForm());
        }
    }
}
