using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netgametools_csharp
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            checkBoxIGDOnly.Checked = ProgramSettings.optionShowOnlyNetworkDevices;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxIGDOnly_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProgramSettings.optionShowOnlyNetworkDevices = checkBoxIGDOnly.Checked;
        }
    }
}
