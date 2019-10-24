using MouseDev_Server_Socket.Core;
using System.Windows.Forms;

namespace MouseDev_Server_Socket.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            RealtimeConfig realtimeConfig = new RealtimeConfig();
        }
    }
}
