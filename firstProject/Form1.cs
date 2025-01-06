using System;
using System.IO;
using System.Windows.Forms;

namespace firstProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeLanguageMenu();
            // �⺻ ��� ���� (�ý��� ��� ����)
            string systemLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            ChangeLanguage(systemLanguage == "ko" ? "ko" : "en");
        }

        // "���� �����" �޴� Ŭ�� �̺�Ʈ
        private void NewFile_Click(object sender, EventArgs e)
        {
            if (ConfirmSave())
            {
                txtEditor.Clear();
            }
        }

        // "����" �޴� Ŭ�� �̺�Ʈ
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "�ؽ�Ʈ ���� (*.txt)|*.txt|��� ���� (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        // "����" �޴� Ŭ�� �̺�Ʈ
        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "�ؽ�Ʈ ���� (*.txt)|*.txt|��� ���� (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
            }
        }

        // "����" �޴� Ŭ�� �̺�Ʈ
        private void Exit_Click(object sender, EventArgs e)
        {
            if (ConfirmSave())
            {
                Application.Exit();
            }
        }

        // "����" �޴� Ŭ�� �̺�Ʈ
        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("C#���� ���� ������ �޸����Դϴ�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ���� ���� Ȯ��
        private bool ConfirmSave()
        {
            var result = MessageBox.Show("���� ������ �����Ͻðڽ��ϱ�?", "���� Ȯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveFile_Click(null, null); // ����
                return true;
            }
            else if (result == DialogResult.No)
            {
                return true; // �������� �ʰ� ����
            }

            return false; // ���
        }
        //"�۲� ����" �޴� Ŭ�� �̺�Ʈ
        private void ChangeFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.Font = txtEditor.Font;
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    txtEditor.Font = fontDialog.Font;
                }
            }
        }
        // "ã��" �޴� Ŭ�� �̺�Ʈ
        private void FindText_Click(object sender, EventArgs e)
        {
            string searchText = Prompt.ShowDialog("�˻��� �ؽ�Ʈ�� �Է��ϼ���:", "ã��");
            if (!string.IsNullOrEmpty(searchText))
            {
                int startIndex = txtEditor.Find(searchText);
                if (startIndex >= 0)
                {
                    txtEditor.SelectionStart = startIndex;
                    txtEditor.SelectionLength = searchText.Length;
                    txtEditor.SelectionBackColor = Color.Yellow;
                }
                else
                {
                    MessageBox.Show("�˻� ����� �����ϴ�.", "ã��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void InitializeLanguageMenu()
        {
            ToolStripMenuItem koreanMenu = new ToolStripMenuItem
            {
                Name = "KoreanMenu",
                Text = "�ѱ���"
            };
            koreanMenu.Click += (sender, e) => ChangeLanguage("ko");

            ToolStripMenuItem englishMenu = new ToolStripMenuItem
            {
                Name = "EnglishMenu",
                Text = "English"
            };
            englishMenu.Click += (sender, e) => ChangeLanguage("en");

            ���ToolStripMenuItem.DropDownItems.Clear();
            ���ToolStripMenuItem.DropDownItems.Add(koreanMenu);
            ���ToolStripMenuItem.DropDownItems.Add(englishMenu);
        }

        private void ChangeLanguage(string cultureCode)
        {
            // ��� ����
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureCode);

            // ��� ��Ʈ�ѿ� ����� ���ҽ� ����
            ApplyLanguage();

            // �� �ؽ�Ʈ�� ������Ʈ
            Text = Properties.Resources.FormTitle;
        }

        private void ApplyLanguage()
        {
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Text = GetResourceString(menuItem.Name, menuItem.Text);
                    ApplyLanguageToMenu(menuItem);
                }
            }

            Text = GetResourceString("$this", Text); // �� ���� �⺻��
        }

        private void ApplyLanguageToMenu(ToolStripMenuItem menuItem)
        {
            foreach (ToolStripItem subItem in menuItem.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    subMenuItem.Text = GetResourceString(subMenuItem.Name, subMenuItem.Text);
                    ApplyLanguageToMenu(subMenuItem);
                }
            }
        }

        private string GetResourceString(string key, string defaultValue)
        {
            string? resourceValue = Properties.Resources.ResourceManager.GetString(key);
            return string.IsNullOrEmpty(resourceValue) ? defaultValue : resourceValue;
        }


    }
    // ������ �Է� â(Prompt) Ŭ����
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form
            {
                Width = 400,
                Height = 150,
                Text = caption
            };
            Label label = new Label { Left = 10, Top = 10, Text = text };
            TextBox textBox = new TextBox { Left = 10, Top = 40, Width = 360 };
            Button confirmation = new Button { Text = "Ȯ��", Left = 290, Width = 80, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };
            prompt.Controls.Add(label);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }
    }


}
