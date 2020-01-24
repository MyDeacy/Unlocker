using System;
using System.Windows.Forms;
using Unlocker.util;

namespace Unlocker
{
    public partial class MainForm : Form
    {
        private FileProcessor fileProcessor;

        public MainForm()
        {
            fileProcessor = new FileProcessor();
            InitializeComponent();
        }

        private void InputSelectButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excelブック(*.xlsx;*.xlsm)|*.xlsx;*.xlsm";
            DialogResult fileDialog = openFileDialog1.ShowDialog();
            inputTextBox.Text = 
                fileDialog == DialogResult.OK ? openFileDialog1.FileName : "";

        }

        private void OutputSelectButton_Click(object sender, EventArgs e)
        {
            DialogResult folderDialog = folderBrowserDialog1.ShowDialog();
            outputTextBox.Text = 
                folderDialog == DialogResult.OK ? folderBrowserDialog1.SelectedPath : "";
        }

        private void UnlockButton_Click(object sender, EventArgs e)
        {
            string text;
            try
            {
                label4.Text = "解析中...";
                string[] files = fileProcessor.ExtractBook(inputTextBox.Text);
                int count = fileProcessor.UnLockProtect(files);
                fileProcessor.MakeExcelFile(outputTextBox.Text);
                text = "解析完了しました。\n(保護を解除した件数" + count + "件)";
            }catch(Exception exception)
            {
                text = "エラーが発生しました。";
            }
            MessageBox.Show(text);
            label4.ResetText();
        }
    }
}
