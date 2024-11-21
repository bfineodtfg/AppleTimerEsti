using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppleTimer
{
    public partial class Form1 : Form
    {
        Timer walkTimer = new Timer();
        public Form1()
        {
            InitializeComponent();
            Start();
        }
        void Start() {
            StartTimers();
        }
        void StartTimers() { 
            
        }
        void WalkEvent(object s, EventArgs e) { 
            
        }
    }
}
