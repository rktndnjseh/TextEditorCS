using System;
using System.IO;
using System.Windows.Forms;
using Google.Apis.Drive.v3;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace firstProject
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer autoSaveTimer;
        private string savedFilePath = null; // 저장된 파일 경로
        private bool isFileSaved = false;   // 파일 저장 여부 플래그

        public Form1()
        {
            InitializeComponent();
            InitializeLanguageMenu();
            // 기본 언어 설정 (시스템 언어 기준)
            string systemLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            ChangeLanguage(systemLanguage == "ko" ? "ko" : "en");
            InitializePluginMenu();
            InitializeCloudButtons();
            InitializeAutoSave();
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
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                savedFilePath = openFileDialog.FileName;
                isFileSaved = true; // 파일 열기 시 자동 저장 활성화
            }
        }


        // "저장" 메뉴 클릭 이벤트
        private void SaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(savedFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    savedFilePath = saveFileDialog.FileName;
                }
            }

            if (!string.IsNullOrEmpty(savedFilePath))
            {
                File.WriteAllText(savedFilePath, txtEditor.Text);
                isFileSaved = true; // 파일 저장 성공 시 플래그 업데이트
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
            foreach (Control control in Controls)
            {
                control.Text = GetResourceString(control.Name, control.Text);
            }

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

        // 자동 저장 타이머 초기화
        private void InitializeAutoSave()
        {
            autoSaveTimer = new System.Windows.Forms.Timer();
            autoSaveTimer.Interval = 5000; // 5초마다 저장
            autoSaveTimer.Tick += AutoSaveTimer_Tick;
            autoSaveTimer.Start();
        }
        // 자동 저장 타이머 이벤트
        private void AutoSaveTimer_Tick(object sender, EventArgs e)
        {
            if (isFileSaved && !string.IsNullOrEmpty(savedFilePath))
            {
                AutoSave();
            }
        }

        // 자동 저장 메서드
        private void AutoSave()
        {
            try
            {
                string dataToSave = GetDataToSave();
                File.WriteAllText(savedFilePath, dataToSave);
                Console.WriteLine($"[{DateTime.Now}] Auto-saved to {savedFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Auto-save failed: {ex.Message}");
            }
        }

        // 저장할 데이터 가져오기 (예: 텍스트박스 데이터)
        private string GetDataToSave()
        {
            return txtEditor.Text; // 예제: textBox1의 텍스트 저장
        }
        //플러그인 로드
        private void LoadPlugins()
        {
            string pluginDirectory = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Plugins");
            var loader = new PluginLoader(pluginDirectory);
            var plugins = loader.LoadPlugins();
            MessageBox.Show(pluginDirectory, "플러그인 로드", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(File.Exists(Path.Combine(pluginDirectory, "SamplePlugin.dll"))
    ? "SamplePlugin.dll exists."
    : "SamplePlugin.dll not found.", "플러그인 로드", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (plugins.Count == 0)
            {
                MessageBox.Show("로드할 플러그인이 없습니다.", "플러그인 로드", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var plugin in plugins)
            {
                ToolStripMenuItem pluginMenuItem = new ToolStripMenuItem
                {
                    Text = plugin.Name,
                    Tag = plugin
                };

                pluginMenuItem.Click += (sender, e) =>
                {
                    plugin.Execute();
                };

                플러그인ToolStripMenuItem.DropDownItems.Add(pluginMenuItem); // 플러그인 메뉴에 추가
            }

            MessageBox.Show($"{plugins.Count}개의 플러그인을 로드했습니다.", "플러그인 로드 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializePluginMenu()
        {
            ToolStripMenuItem pluginMenu = new ToolStripMenuItem
            {
                Name = "PluginMenu",
                Text = "플러그인"
            };

            ToolStripMenuItem loadPluginMenu = new ToolStripMenuItem
            {
                Name = "LoadPluginMenu",
                Text = "플러그인 로드"
            };
            loadPluginMenu.Click += (sender, e) => LoadPlugins();

            pluginMenu.DropDownItems.Add(loadPluginMenu);
            menuStrip1.Items.Add(pluginMenu); // 메인 메뉴에 추가
        }

        // 클라우드 버튼 초기화
        private void InitializeCloudButtons()
        {
            ToolStripMenuItem btnSaveToCloud = new ToolStripMenuItem
            {
                Name = "btnSaveToCloud",
                Text = "클라우드 저장"
            };
            // "Save to Cloud" 버튼 이벤트 연결
            btnSaveToCloud.Click += (sender, e) => SaveToCloud_Click(sender, e);

            ToolStripMenuItem btnLoadFromCloud = new ToolStripMenuItem
            {
                Name = "btnLoadFromCloud",
                Text = "클라우드 불러오기"
            };
            // "Load from Cloud" 버튼 이벤트 연결
            btnLoadFromCloud.Click += (sender, e) => LoadFromCloud_Click(sender, e);

            클라우드ToolStripMenuItem.DropDownItems.Clear();
            클라우드ToolStripMenuItem.DropDownItems.Add(btnSaveToCloud);
            클라우드ToolStripMenuItem.DropDownItems.Add(btnLoadFromCloud);
        }

        // "Save to Cloud" 버튼 클릭 이벤트
        private void SaveToCloud_Click(object sender, EventArgs e)
        {
            try
            {
                var service = GoogleDriveService.Initialize(); // Google Drive 인증 초기화
                string filePath = "local_file.txt"; // 업로드할 파일 경로
                File.WriteAllText(filePath, txtEditor.Text); // 텍스트 에디터 내용 저장
                string fileId = GoogleDriveService.UploadFile(service, filePath, "text/plain"); // 파일 업로드
                MessageBox.Show($"File uploaded successfully. File ID: {fileId}", "Upload Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading file: {ex.Message}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // "Load from Cloud" 버튼 클릭 이벤트
        private void LoadFromCloud_Click(object sender, EventArgs e)
        {
            try
            {
                var service = GoogleDriveService.Initialize(); // Google Drive 인증 초기화
                string fileId = Prompt.ShowDialog("Enter File ID:", "Download File"); // 다운로드할 파일 ID 입력
                if (string.IsNullOrEmpty(fileId))
                {
                    MessageBox.Show("File ID is required!", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string savePath = "downloaded_file.txt"; // 다운로드할 파일 경로
                GoogleDriveService.DownloadFile(service, fileId, savePath); // 파일 다운로드
                txtEditor.Text = File.ReadAllText(savePath); // 다운로드한 파일 내용을 텍스트 에디터에 로드
                MessageBox.Show("File downloaded and loaded successfully.", "Download Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox { Left = 10, Top = 40, Width = 360 };
            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button { Text = "확인", Left = 290, Width = 80, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };
            prompt.Controls.Add(label);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }
    }


}
