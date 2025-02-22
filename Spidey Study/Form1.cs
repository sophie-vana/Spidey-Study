using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;


namespace SpiderManPomodoroApp
{
    public partial class Form1 : Form
    {
        private int timeLeft = 1500; // 25 minutes in seconds
        private Timer messageTimer; // Timer for changing messages
        private const int MessageChangeInterval = 60; // 5 minutes in seconds

        private readonly string[] motivationalMessages = new[]
        {
            "Writing this paper like Spider-Man spins webs - making it up as I go!",
            "My references list is longer than Doc Oc's arms",
            "My spider-sense tells me I forgot to cite a source",
            "Catching deadlines like Spidey catches bad guys",
            "With great productivity comes great need for snacks",
            "This project will be amazing... or at least spectacular",
            "My productivity is spectacular... when there's a deadline",
            "Time to be spectacular (or at least pass this project)",
            "Running late? Wish I could web-swing to campus right now...",
            "My spider-sense tells me it's coffee time... again",
            "My research method: throwing ideas at the wall and seeing what sticks",
            "Your spider-sense is that voice saying 'you've got this!'",
            "Heroes aren't born spectacular - they work for it every day",
            "if you see this, i love you <3"
        };

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const int PBM_SETBARCOLOR = 0x409;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            timer1 = new Timer();
            timer1.Interval = 1000; // 1 second interval
            timer1.Tick += Timer1_Tick;

            messageTimer = new Timer();
            messageTimer.Interval = MessageChangeInterval * 1000; // Convert to milliseconds
            messageTimer.Tick += MessageTimer_Tick;

            progressBar.Minimum = 0;
            progressBar.Maximum = timeLeft;
            progressBar.Value = timeLeft;

            labelTimer.Text = "25:00";
            labelMessage.Text = "Welcome!"; // Set initial message
            labelMessage.TextAlign = ContentAlignment.MiddleCenter; // Center the text
            labelMessage.AutoSize = false; // Disable auto sizing
            labelMessage.Size = new System.Drawing.Size(300, 50); // Set a fixed size
            labelMessage.Location = new Point((this.ClientSize.Width - labelMessage.Width) / 2, (this.ClientSize.Height - labelMessage.Height) / 2);

            SendMessage(progressBar.Handle, PBM_SETBARCOLOR, IntPtr.Zero, (IntPtr)System.Drawing.ColorTranslator.ToWin32(System.Drawing.Color.Red));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start the message timer
            messageTimer.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                progressBar.Value = timeLeft;
                int minutes = timeLeft / 60;
                int seconds = timeLeft % 60;
                labelTimer.Text = $"{minutes:D2}:{seconds:D2}";
            }
            else
            {
                timer1.Stop();
                labelMessage.Text = "Time's up!";
                messageTimer.Stop(); // Stop message timer when time's up
            }
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            // Select a random motivational message
            Random random = new Random();
            int index = random.Next(motivationalMessages.Length);
            labelMessage.Text = motivationalMessages[index];
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            messageTimer.Stop(); // Stop message timer on reset
            timeLeft = 1500;
            progressBar.Value = timeLeft;
            labelTimer.Text = "25:00";
            labelMessage.Text = "Welcome!"; // Reset message
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Spider-Man approves your productivity!");
        }

        private void labelTimer_Click(object sender, EventArgs e)
        {
            // Optional: Handle clicks on the timer label if needed
        }

        private void labelMessage_Click(object sender, EventArgs e)
        {
            // Optional: Handle clicks on the message label if needed
        }
    }
}
