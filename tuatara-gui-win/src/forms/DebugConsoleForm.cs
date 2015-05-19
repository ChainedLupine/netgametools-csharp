using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using chainedlupine.tuatara;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace tuatara_gui
{
    public partial class DebugConsoleForm : Form
    {
        public DebugConsoleForm()
        {
            InitializeComponent();
        }

        public void LinkLogger()
        {
            Logger.srcWriteLine = new Logger.DelegateLineWriter(normalLineWriter);
            Logger.srcWriteLineWarn = new Logger.DelegateLineWriter(warningLineWriter);
            Logger.srcWriteLineError = new Logger.DelegateLineWriter(errorLineWriter);
        }

        public void AppendText(string text, Color color)
        {
            Debug.WriteLine(text);
            this.UIThread(delegate
            {
                richTextBoxConsole.SelectionStart = richTextBoxConsole.TextLength;
                richTextBoxConsole.SelectionLength = 0;

                richTextBoxConsole.SelectionColor = color;
                richTextBoxConsole.AppendText(text + "\r\n");
                richTextBoxConsole.SelectionColor = richTextBoxConsole.ForeColor;
            });
        }

        /*public void ClearDevices()
        {
            treeViewXml.Nodes.Clear();
        }

        public void LoadDeviceXml (Device device)
        {
            TreeNode tNode = treeViewXml.Nodes.Add("Device " + device.uuid);

            ConvertXmlNodeToTreeNode(device.debugRawXml.ToXmlDocument(), tNode.Nodes);

            // Add services
            TreeNode servicesNode = tNode.Nodes.Add("Services");

            foreach (Service service in device.services)
            {
                TreeNode serviceNode = servicesNode.Nodes.Add("Service Type " + service.serviceType);
                ConvertXmlNodeToTreeNode(service.debugRawXml.ToXmlDocument(), serviceNode.Nodes);
                
            }
            //treeViewXml.Nodes[0].ExpandAll();
        }

        private void ConvertXmlNodeToTreeNode(XmlNode xmlNode, TreeNodeCollection treeNodes)
        {

            TreeNode newTreeNode = treeNodes.Add(xmlNode.Name);

            switch (xmlNode.NodeType)
            {
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    newTreeNode.Text = "<?" + xmlNode.Name + " " +
                      xmlNode.Value + "?>";
                    break;
                case XmlNodeType.Element:
                    newTreeNode.Text = "<" + xmlNode.Name + ">";
                    break;
                case XmlNodeType.Attribute:
                    newTreeNode.Text = "ATTRIBUTE: " + xmlNode.Name;
                    break;
                case XmlNodeType.Text:
                case XmlNodeType.CDATA:
                    newTreeNode.Text = xmlNode.Value;
                    break;
                case XmlNodeType.Comment:
                    newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
                }
            }
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
            }
        }
         * */

        private void normalLineWriter(string text)
        {
            AppendText(text, Control.DefaultForeColor);
        }

        private void warningLineWriter(string text)
        {
            AppendText(text, Color.Green);
        }

        private void errorLineWriter(string text)
        {
            AppendText(text, Color.Red);
        }

        private void DebugConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown
                || e.CloseReason == CloseReason.ApplicationExitCall
                || e.CloseReason == CloseReason.TaskManagerClosing)
            {
                return;
            }
            e.Cancel = true;
            //assuming you want the close-button to only hide the form, 
            //and are overriding the form's OnFormClosing method:
            this.Hide();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBoxConsole.Text);
        }

        /*private void btnDumpXml_Click(object sender, EventArgs e)
        {
            ClearDevices();

            if (ProgramSettings.controlPoint.knownDeviceList == null)
                return;

            foreach (Device device in ProgramSettings.controlPoint.knownDeviceList)
                LoadDeviceXml(device);

        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (treeViewXml.SelectedNode == null)
                return;

            treeViewXml.SelectedNode.ExpandAll();
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            if (treeViewXml.SelectedNode == null)
                return;

            treeViewXml.SelectedNode.Collapse();
        }

        private void treeViewXml_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnCollapse.Enabled = btnExpand.Enabled = treeViewXml.SelectedNode != null;
        }
        */
    }
}
