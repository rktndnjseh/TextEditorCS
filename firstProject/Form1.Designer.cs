namespace firstProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtEditor = new RichTextBox();
            menuStrip1 = new MenuStrip();
            파일ToolStripMenuItem = new ToolStripMenuItem();
            새로만들기ToolStripMenuItem = new ToolStripMenuItem();
            열기ToolStripMenuItem = new ToolStripMenuItem();
            저장ToolStripMenuItem = new ToolStripMenuItem();
            종료ToolStripMenuItem = new ToolStripMenuItem();
            도움말ToolStripMenuItem = new ToolStripMenuItem();
            정보ToolStripMenuItem = new ToolStripMenuItem();
            서식ToolStripMenuItem = new ToolStripMenuItem();
            글꼴변경ToolStripMenuItem = new ToolStripMenuItem();
            찾기ToolStripMenuItem = new ToolStripMenuItem();
            도구ToolStripMenuItem = new ToolStripMenuItem();
            언어ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            플러그인ToolStripMenuItem = new ToolStripMenuItem();
            클라우드ToolStripMenuItem = new ToolStripMenuItem();
            클라우드저장ToolStripMenuItem = new ToolStripMenuItem();
            클라우드불러오기ToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtEditor
            // 
            txtEditor.Dock = DockStyle.Fill;
            txtEditor.Location = new Point(0, 24);
            txtEditor.Name = "txtEditor";
            txtEditor.Size = new Size(800, 426);
            txtEditor.TabIndex = 0;
            txtEditor.Text = "";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 파일ToolStripMenuItem, 도움말ToolStripMenuItem, 서식ToolStripMenuItem, 찾기ToolStripMenuItem, 도구ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            파일ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 새로만들기ToolStripMenuItem, 열기ToolStripMenuItem, 저장ToolStripMenuItem, 종료ToolStripMenuItem });
            파일ToolStripMenuItem.Name = "FileMenu";
            파일ToolStripMenuItem.Size = new Size(43, 20);
            파일ToolStripMenuItem.Text = "파일";
            // 
            // 새로만들기ToolStripMenuItem
            // 
            새로만들기ToolStripMenuItem.Name = "NewFileMenu";
            새로만들기ToolStripMenuItem.Size = new Size(138, 22);
            새로만들기ToolStripMenuItem.Text = "새로 만들기";
            새로만들기ToolStripMenuItem.Click += NewFile_Click;
            // 
            // 열기ToolStripMenuItem
            // 
            열기ToolStripMenuItem.Name = "OpenFileMenu";
            열기ToolStripMenuItem.Size = new Size(138, 22);
            열기ToolStripMenuItem.Text = "열기";
            열기ToolStripMenuItem.Click += OpenFile_Click;
            // 
            // 저장ToolStripMenuItem
            // 
            저장ToolStripMenuItem.Name = "SaveFileMenu";
            저장ToolStripMenuItem.Size = new Size(138, 22);
            저장ToolStripMenuItem.Text = "저장";
            저장ToolStripMenuItem.Click += SaveFile_Click;
            // 
            // 종료ToolStripMenuItem
            // 
            종료ToolStripMenuItem.Name = "ExitMenu";
            종료ToolStripMenuItem.Size = new Size(138, 22);
            종료ToolStripMenuItem.Text = "종료";
            종료ToolStripMenuItem.Click += Exit_Click;
            // 
            // 도움말ToolStripMenuItem
            // 
            도움말ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 정보ToolStripMenuItem });
            도움말ToolStripMenuItem.Name = "HelpMenu";
            도움말ToolStripMenuItem.Size = new Size(55, 20);
            도움말ToolStripMenuItem.Text = "도움말";
            // 
            // 정보ToolStripMenuItem
            // 
            정보ToolStripMenuItem.Name = "AboutMenu";
            정보ToolStripMenuItem.Size = new Size(98, 22);
            정보ToolStripMenuItem.Text = "정보";
            // 
            // 서식ToolStripMenuItem
            // 
            서식ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 글꼴변경ToolStripMenuItem });
            서식ToolStripMenuItem.Name = "FormatMenu";
            서식ToolStripMenuItem.Size = new Size(43, 20);
            서식ToolStripMenuItem.Text = "서식";
            // 
            // 글꼴변경ToolStripMenuItem
            // 
            글꼴변경ToolStripMenuItem.Name = "FontMenu";
            글꼴변경ToolStripMenuItem.Size = new Size(126, 22);
            글꼴변경ToolStripMenuItem.Text = "글꼴 변경";
            글꼴변경ToolStripMenuItem.Click += ChangeFont_Click;
            // 
            // 찾기ToolStripMenuItem
            // 
            찾기ToolStripMenuItem.Name = "SearchMenu";
            찾기ToolStripMenuItem.Size = new Size(43, 20);
            찾기ToolStripMenuItem.Text = "찾기";
            찾기ToolStripMenuItem.Click += FindText_Click;
            // 
            // 도구ToolStripMenuItem
            // 
            도구ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 언어ToolStripMenuItem, 플러그인ToolStripMenuItem, 클라우드ToolStripMenuItem });
            도구ToolStripMenuItem.Name = "ToolMenu";
            도구ToolStripMenuItem.Size = new Size(43, 20);
            도구ToolStripMenuItem.Text = "도구";
            // 
            // 언어ToolStripMenuItem
            // 
            언어ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3 });
            언어ToolStripMenuItem.Name = "LanguageMenu";
            언어ToolStripMenuItem.Size = new Size(180, 22);
            언어ToolStripMenuItem.Text = "언어";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "KoreanMenu";
            toolStripMenuItem2.Size = new Size(112, 22);
            toolStripMenuItem2.Text = "한국어";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "EnglishMenu";
            toolStripMenuItem3.Size = new Size(112, 22);
            toolStripMenuItem3.Text = "English";
            // 
            // 플러그인ToolStripMenuItem
            // 
            플러그인ToolStripMenuItem.Name = "PluginMenu";
            플러그인ToolStripMenuItem.Size = new Size(180, 22);
            플러그인ToolStripMenuItem.Text = "플러그인";
            // 
            // 클라우드ToolStripMenuItem
            // 
            클라우드ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 클라우드저장ToolStripMenuItem, 클라우드불러오기ToolStripMenuItem });
            클라우드ToolStripMenuItem.Name = "CloudMenu";
            클라우드ToolStripMenuItem.Size = new Size(180, 22);
            클라우드ToolStripMenuItem.Text = "클라우드";
            // 
            // 클라우드저장ToolStripMenuItem
            // 
            클라우드저장ToolStripMenuItem.Name = "btnSaveToCloud";
            클라우드저장ToolStripMenuItem.Size = new Size(180, 22);
            클라우드저장ToolStripMenuItem.Text = "클라우드 저장";
            클라우드저장ToolStripMenuItem.Click += SaveToCloud_Click;
            // 
            // 클라우드불러오기ToolStripMenuItem
            // 
            클라우드불러오기ToolStripMenuItem.Name = "btnLoadFromCloud";
            클라우드불러오기ToolStripMenuItem.Size = new Size(180, 22);
            클라우드불러오기ToolStripMenuItem.Text = "클라우드 불러오기";
            클라우드불러오기ToolStripMenuItem.Click += LoadFromCloud_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtEditor);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox txtEditor;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 파일ToolStripMenuItem;
        private ToolStripMenuItem 새로만들기ToolStripMenuItem;
        private ToolStripMenuItem 열기ToolStripMenuItem;
        private ToolStripMenuItem 저장ToolStripMenuItem;
        private ToolStripMenuItem 종료ToolStripMenuItem;
        private ToolStripMenuItem 도움말ToolStripMenuItem;
        private ToolStripMenuItem 정보ToolStripMenuItem;
        private ToolStripMenuItem 서식ToolStripMenuItem;
        private ToolStripMenuItem 글꼴변경ToolStripMenuItem;
        private ToolStripMenuItem 찾기ToolStripMenuItem;
        private ToolStripMenuItem 도구ToolStripMenuItem;
        private ToolStripMenuItem 언어ToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem 플러그인ToolStripMenuItem;
        private ToolStripMenuItem 클라우드ToolStripMenuItem;
        private ToolStripMenuItem 클라우드저장ToolStripMenuItem;
        private ToolStripMenuItem 클라우드불러오기ToolStripMenuItem;
    }
}
