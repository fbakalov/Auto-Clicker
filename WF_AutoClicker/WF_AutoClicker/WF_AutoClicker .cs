using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Utilities;

namespace WF_AutoClicker
{
    public partial class Form1 : Form
    {      
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private int X = Cursor.Position.X;
        private int Y = Cursor.Position.Y;

        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void StartClicking(Object obj, EventArgs args)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)X, (uint)Y, 0, 0);
        }


        globalKeyboardHook start = new globalKeyboardHook();
        globalKeyboardHook stop = new globalKeyboardHook();

        private void Form1_Load(object sender, EventArgs e)
        {
            start.HookedKeys.Add(Keys.O);
            stop.HookedKeys.Add(Keys.P);
            start.KeyDown += new KeyEventHandler(gkh_KeyDownStart);
            stop.KeyDown += new KeyEventHandler(gkh_KeyDownStop);
        }

        void gkh_KeyDownStart(object sender, KeyEventArgs e)
        {
            Start();
            
            e.Handled = true;
        }
        void gkh_KeyDownStop(object sender, KeyEventArgs e)
        {
            Stop();
            
            e.Handled = true;
        }

        private void startClickingButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            int interval = int.Parse(textBox1.Text);
            stopButton.Enabled = true;


            // disable the group box that's holds the interval option so that the user cannot change it at runtime
            timerGroupBox.Enabled = false;

            // calls the event, assign the interval value and start the timer
            timer.Tick += new EventHandler(StartClicking);
            timer.Interval = interval;
            timer.Start();

            // the label value is changed to "clicking" to let the user knows the application current status
            status.Text = String.Format("Clicking with {0} milliseconds interval", interval);
            startClickingButton.Enabled = false;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }
        private void Stop()
        {
            startClickingButton.Enabled = true;
            timerGroupBox.Enabled = true;
            timer.Stop();

            // change the current status to "not clicking"
            status.Text = String.Format("Not clicking");
            stopButton.Enabled = false;
        }

        private void status_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
