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
            ProgramSettings.settings.ShowOnlyNetworkDevices = checkBoxIGDOnly.Checked;

            ProgramSettings.settings.SkipSafetyChecks = checkBoxSkipSafety.Checked;

            ProgramSettings.settings.SelectedAdapterName = comboAdapters.SelectedItem.ToString();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            checkBoxIGDOnly.Checked = ProgramSettings.settings.ShowOnlyNetworkDevices;
            checkBoxSkipSafety.Checked = ProgramSettings.settings.SkipSafetyChecks;

            comboAdapters.Items.Clear();

            foreach (NetworkAdapterDetail adapterDetail in ProgramSettings.adapters)
            {
                comboAdapters.Items.Add(adapterDetail.name);
            }

            comboAdapters.SelectedIndex = comboAdapters.FindStringExact(ProgramSettings.settings.SelectedAdapterName);

            textCurrentIP.Select(0, 0);
        }

        private void comboAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            textCurrentIP.Text = ProgramSettings.GetSelectedAdapterDetails(comboAdapters.SelectedItem.ToString()).address.ToString();
        }
    }
}
