using System;
using System.IO;
using System.Windows.Forms;

namespace JSONLocalesGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool CheckInputValidity()
        {
            if (string.IsNullOrEmpty(tbInput.Text) || !File.Exists(tbInput.Text))
            {
                MessageBox.Show("Invalid input file", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tbOutput.Text) || !Directory.Exists(tbOutput.Text))
            {
                MessageBox.Show("Invalid destination folder", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInputValidity())
                {
                    Generator.Generate(tbInput.Text, tbOutput.Text);
                    MessageBox.Show("Success, the JSON data have been succesfuly generated!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failure, the following exception was thrown: " + exception.Message);
            }
        }


        private void btBrowseInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel Files|*.xlsx";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                tbInput.Text = fileDialog.FileName;
            }
        }


        private void btBrowseOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbOutput.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
