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
            // 기본 언어 설정 (시스템 언어 기준)
            string systemLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            ChangeLanguage(systemLanguage == "ko" ? "ko" : "en");
        }

        // "새로 만들기" 메뉴 클릭 이벤트
        private void NewFile_Click(object sender, EventArgs e)
        {
            if (ConfirmSave())
            {
                txtEditor.Clear();
            }
        }

        // "열기" 메뉴 클릭 이벤트
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        // "저장" 메뉴 클릭 이벤트
        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
            }
        }

        // "종료" 메뉴 클릭 이벤트
        private void Exit_Click(object sender, EventArgs e)
        {
            if (ConfirmSave())
            {
                Application.Exit();
            }
        }

        // "정보" 메뉴 클릭 이벤트
        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("C#으로 만든 간단한 메모장입니다!", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 저장 여부 확인
        private bool ConfirmSave()
        {
            var result = MessageBox.Show("현재 내용을 저장하시겠습니까?", "저장 확인", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveFile_Click(null, null); // 저장
                return true;
            }
            else if (result == DialogResult.No)
            {
                return true; // 저장하지 않고 진행
            }

            return false; // 취소
        }
        //"글꼴 변경" 메뉴 클릭 이벤트
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
        // "찾기" 메뉴 클릭 이벤트
        private void FindText_Click(object sender, EventArgs e)
        {
            string searchText = Prompt.ShowDialog("검색할 텍스트를 입력하세요:", "찾기");
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
                    MessageBox.Show("검색 결과가 없습니다.", "찾기", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void InitializeLanguageMenu()
        {
            ToolStripMenuItem koreanMenu = new ToolStripMenuItem
            {
                Name = "KoreanMenu",
                Text = "한국어"
            };
            koreanMenu.Click += (sender, e) => ChangeLanguage("ko");

            ToolStripMenuItem englishMenu = new ToolStripMenuItem
            {
                Name = "EnglishMenu",
                Text = "English"
            };
            englishMenu.Click += (sender, e) => ChangeLanguage("en");

            언어ToolStripMenuItem.DropDownItems.Clear();
            언어ToolStripMenuItem.DropDownItems.Add(koreanMenu);
            언어ToolStripMenuItem.DropDownItems.Add(englishMenu);
        }

        private void ChangeLanguage(string cultureCode)
        {
            // 언어 변경
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureCode);

            // 모든 컨트롤에 변경된 리소스 적용
            ApplyLanguage();

            // 폼 텍스트도 업데이트
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

            Text = GetResourceString("$this", Text); // 폼 제목 기본값
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
    // 간단한 입력 창(Prompt) 클래스
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
            Button confirmation = new Button { Text = "확인", Left = 290, Width = 80, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };
            prompt.Controls.Add(label);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }
    }


}
