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
    public partial class PortCollectionsForm : Form
    {
        private BindingSource pcBindingSource = new BindingSource();


        public PortCollectionsForm()
        {
            InitializeComponent();

            pcBindingSource.DataSource = typeof(PortCollectionDetail);
            pcBindingSource.AllowNew = true;

            DataGridViewComboBoxColumn col = dataGridViewCollection.Columns[0] as DataGridViewComboBoxColumn;
            col.ValueType = typeof(ePortCollectionProtocol);
            col.DataSource = Enum.GetValues(typeof(ePortCollectionProtocol));

            dataGridViewCollection.AutoGenerateColumns = false;
            dataGridViewCollection.DataSource = pcBindingSource;

        }

        private void comboPortCollections_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox control = (ComboBox)sender;

            if (control.SelectedIndex >= 0)
            {
                btnPortCollectionRemove.Enabled = true;
                grpPortDetails.Enabled = true;
                textCollectionTitle.Text = comboPortCollections.Text;

                // Load datagrid
                // Protocol (TCP/UDP), Port
                pcBindingSource.Clear();
                pcBindingSource.Add(new PortCollectionDetail(ePortCollectionProtocol.TCP, 1000));
                pcBindingSource.Add(new PortCollectionDetail(ePortCollectionProtocol.UDP, 5000));

                dataGridViewCollection.Visible = true;
            }
            else
            {
                grpPortDetails.Enabled = false;
                btnPortCollectionRemove.Enabled = false;
                dataGridViewCollection.Visible = false;
            }

        }

        private void btnPortCollectionAdd_Click(object sender, EventArgs e)
        {
            comboPortCollections.Items.Add("New Collection");
            comboPortCollections.SelectedIndex = comboPortCollections.Items.Count - 1;
        }

        private void btnPortCollectionRemove_Click(object sender, EventArgs e)
        {
            comboPortCollections.Items.RemoveAt(comboPortCollections.SelectedIndex);

            if (comboPortCollections.Items.Count > 0)
                comboPortCollections.SelectedIndex = 0;
            else
            {
                grpPortDetails.Enabled = false;
                dataGridViewCollection.Visible = false;
                comboPortCollections.Text = "";
            }
        }

        private void textCollectionTitle_TextChanged(object sender, EventArgs e)
        {
            if (textCollectionTitle.Text.Length > 0)

                comboPortCollections.Items[comboPortCollections.SelectedIndex] = textCollectionTitle.Text;
            else
                textCollectionTitle.Text = comboPortCollections.Text;
        }


    }
}
