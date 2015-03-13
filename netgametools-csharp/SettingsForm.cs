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

            ProgramSettings.selectedAdapter = comboAdapters.SelectedIndex;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            checkBoxIGDOnly.Checked = ProgramSettings.optionShowOnlyNetworkDevices;

            comboAdapters.Items.Clear();

            foreach (NetworkAdapterDetail adapterDetail in ProgramSettings.adapters)
            {
                comboAdapters.Items.Add(adapterDetail.name);
            }

            comboAdapters.SelectedIndex = ProgramSettings.selectedAdapter;

            textCurrentIP.Select(0, 0);
        }

        private void comboAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            textCurrentIP.Text = ProgramSettings.adapters[ProgramSettings.selectedAdapter].address.ToString();
        }
    }
}
