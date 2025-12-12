using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Windows.Forms;

namespace CommandLord
{
    partial class CommandLord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandLord));
            buttonListen = new Button();
            buttonAddListBox = new Button();
            textBoxListen = new TextBox();
            listenListBox = new ListBox();
            checkBoxAllApps = new CheckBox();
            checkBoxKill = new CheckBox();
            listBoxFound = new ListBox();
            buttonAllClear = new Button();
            checkBoxKillAndRun = new CheckBox();
            buttonExit = new Button();
            checkBoxHideNullValue = new CheckBox();
            listBoxCmd = new ListBox();
            buttonAddCmd = new Button();
            textBoxSearch = new TextBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            buttonAddListFound = new Button();
            label6 = new Label();
            buttonHide = new Button();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonListen
            // 
            buttonListen.BackColor = Color.FromArgb(54, 54, 54);
            buttonListen.FlatStyle = FlatStyle.Flat;
            buttonListen.Font = new System.Drawing.Font("Segoe UI", 21.75F, FontStyle.Bold);
            buttonListen.ForeColor = Color.White;
            buttonListen.Location = new Point(640, 407);
            buttonListen.Name = "buttonListen";
            buttonListen.Size = new Size(385, 75);
            buttonListen.TabIndex = 0;
            buttonListen.Text = "Listen";
            buttonListen.UseVisualStyleBackColor = false;
            buttonListen.Click += buttonListen_Click;
            // 
            // buttonAddListBox
            // 
            buttonAddListBox.BackColor = Color.FromArgb(54, 54, 54);
            buttonAddListBox.FlatStyle = FlatStyle.Flat;
            buttonAddListBox.ForeColor = Color.Chartreuse;
            buttonAddListBox.Location = new Point(689, 194);
            buttonAddListBox.Name = "buttonAddListBox";
            buttonAddListBox.Size = new Size(108, 48);
            buttonAddListBox.TabIndex = 1;
            buttonAddListBox.Text = "Add List Box";
            buttonAddListBox.UseVisualStyleBackColor = false;
            buttonAddListBox.Click += buttonAdd_Click;
            // 
            // textBoxListen
            // 
            textBoxListen.BackColor = Color.FromArgb(54, 54, 54);
            textBoxListen.ForeColor = Color.White;
            textBoxListen.Location = new Point(12, 194);
            textBoxListen.Name = "textBoxListen";
            textBoxListen.Size = new Size(671, 23);
            textBoxListen.TabIndex = 2;
            // 
            // listenListBox
            // 
            listenListBox.BackColor = Color.FromArgb(54, 54, 54);
            listenListBox.ForeColor = Color.Chartreuse;
            listenListBox.FormattingEnabled = true;
            listenListBox.Location = new Point(12, 277);
            listenListBox.Name = "listenListBox";
            listenListBox.Size = new Size(500, 124);
            listenListBox.TabIndex = 3;
            // 
            // checkBoxAllApps
            // 
            checkBoxAllApps.AutoSize = true;
            checkBoxAllApps.ForeColor = Color.White;
            checkBoxAllApps.Location = new Point(12, 223);
            checkBoxAllApps.Name = "checkBoxAllApps";
            checkBoxAllApps.Size = new Size(70, 19);
            checkBoxAllApps.TabIndex = 4;
            checkBoxAllApps.Text = "All Apps";
            checkBoxAllApps.UseVisualStyleBackColor = true;
            checkBoxAllApps.CheckedChanged += checkBoxAllApps_CheckedChanged;
            // 
            // checkBoxKill
            // 
            checkBoxKill.AutoSize = true;
            checkBoxKill.ForeColor = Color.White;
            checkBoxKill.Location = new Point(88, 223);
            checkBoxKill.Name = "checkBoxKill";
            checkBoxKill.Size = new Size(42, 19);
            checkBoxKill.TabIndex = 5;
            checkBoxKill.Text = "Kill";
            checkBoxKill.UseVisualStyleBackColor = true;
            checkBoxKill.CheckedChanged += checkBoxKill_CheckedChanged;
            // 
            // listBoxFound
            // 
            listBoxFound.BackColor = Color.FromArgb(54, 54, 54);
            listBoxFound.ForeColor = Color.White;
            listBoxFound.FormattingEnabled = true;
            listBoxFound.Location = new Point(12, 100);
            listBoxFound.Name = "listBoxFound";
            listBoxFound.Size = new Size(1013, 79);
            listBoxFound.TabIndex = 6;
            // 
            // buttonAllClear
            // 
            buttonAllClear.BackColor = Color.FromArgb(54, 54, 54);
            buttonAllClear.FlatStyle = FlatStyle.Flat;
            buttonAllClear.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonAllClear.ForeColor = Color.White;
            buttonAllClear.Location = new Point(525, 407);
            buttonAllClear.Name = "buttonAllClear";
            buttonAllClear.Size = new Size(109, 75);
            buttonAllClear.TabIndex = 7;
            buttonAllClear.Text = "Clear";
            buttonAllClear.UseVisualStyleBackColor = false;
            buttonAllClear.Click += buttonAllClear_Click;
            // 
            // checkBoxKillAndRun
            // 
            checkBoxKillAndRun.AutoSize = true;
            checkBoxKillAndRun.ForeColor = Color.White;
            checkBoxKillAndRun.Location = new Point(136, 223);
            checkBoxKillAndRun.Name = "checkBoxKillAndRun";
            checkBoxKillAndRun.Size = new Size(89, 19);
            checkBoxKillAndRun.TabIndex = 8;
            checkBoxKillAndRun.Text = "Kill and Run";
            checkBoxKillAndRun.UseVisualStyleBackColor = true;
            checkBoxKillAndRun.CheckedChanged += checkBoxKillAndRun_CheckedChanged;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.Red;
            buttonExit.FlatStyle = FlatStyle.Popup;
            buttonExit.Font = new System.Drawing.Font("Arial Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonExit.ForeColor = Color.White;
            buttonExit.Location = new Point(984, 23);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(41, 32);
            buttonExit.TabIndex = 9;
            buttonExit.Text = "X";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // checkBoxHideNullValue
            // 
            checkBoxHideNullValue.AutoSize = true;
            checkBoxHideNullValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            checkBoxHideNullValue.ForeColor = Color.White;
            checkBoxHideNullValue.Location = new Point(12, 60);
            checkBoxHideNullValue.Name = "checkBoxHideNullValue";
            checkBoxHideNullValue.Size = new Size(112, 19);
            checkBoxHideNullValue.TabIndex = 10;
            checkBoxHideNullValue.Text = "Hide Null Values";
            checkBoxHideNullValue.UseVisualStyleBackColor = true;
            checkBoxHideNullValue.CheckedChanged += checkBoxHideNullValue_CheckedChanged;
            // 
            // listBoxCmd
            // 
            listBoxCmd.BackColor = Color.FromArgb(54, 54, 54);
            listBoxCmd.ForeColor = Color.Cyan;
            listBoxCmd.FormattingEnabled = true;
            listBoxCmd.Location = new Point(525, 277);
            listBoxCmd.Name = "listBoxCmd";
            listBoxCmd.Size = new Size(500, 124);
            listBoxCmd.TabIndex = 11;
            listBoxCmd.SelectedIndexChanged += listBoxCmd_SelectedIndexChanged;
            // 
            // buttonAddCmd
            // 
            buttonAddCmd.BackColor = Color.FromArgb(54, 54, 54);
            buttonAddCmd.FlatStyle = FlatStyle.Flat;
            buttonAddCmd.ForeColor = Color.Cyan;
            buttonAddCmd.Location = new Point(803, 194);
            buttonAddCmd.Name = "buttonAddCmd";
            buttonAddCmd.Size = new Size(108, 48);
            buttonAddCmd.TabIndex = 12;
            buttonAddCmd.Text = "Add Cmd Box";
            buttonAddCmd.UseVisualStyleBackColor = false;
            buttonAddCmd.Click += buttonAddCmd_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.BackColor = Color.FromArgb(54, 54, 54);
            textBoxSearch.ForeColor = Color.White;
            textBoxSearch.Location = new Point(525, 71);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(500, 23);
            textBoxSearch.TabIndex = 13;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(459, 71);
            label1.Name = "label1";
            label1.Size = new Size(60, 21);
            label1.TabIndex = 14;
            label1.Text = "Search:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.ForeColor = Color.White;
            label2.Location = new Point(50, 23);
            label2.Name = "label2";
            label2.Size = new Size(127, 21);
            label2.TabIndex = 16;
            label2.Text = "Command Lord";
            // 
            // label3
            // 
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Chartreuse;
            label4.Location = new Point(12, 259);
            label4.Name = "label4";
            label4.Size = new Size(81, 15);
            label4.TabIndex = 17;
            label4.Text = "Listen List Box";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Cyan;
            label5.Location = new Point(525, 259);
            label5.Name = "label5";
            label5.Size = new Size(76, 15);
            label5.TabIndex = 18;
            label5.Text = "Cmd List Box";
            // 
            // buttonAddListFound
            // 
            buttonAddListFound.BackColor = Color.FromArgb(54, 54, 54);
            buttonAddListFound.FlatStyle = FlatStyle.Flat;
            buttonAddListFound.ForeColor = Color.Orange;
            buttonAddListFound.Location = new Point(917, 194);
            buttonAddListFound.Name = "buttonAddListFound";
            buttonAddListFound.Size = new Size(108, 48);
            buttonAddListFound.TabIndex = 19;
            buttonAddListFound.Text = "Add List Found";
            buttonAddListFound.UseVisualStyleBackColor = false;
            buttonAddListFound.Click += buttonAddListFound_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Orange;
            label6.Location = new Point(12, 82);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 20;
            label6.Text = "List Box Found";
            // 
            // buttonHide
            // 
            buttonHide.BackColor = Color.Orange;
            buttonHide.FlatStyle = FlatStyle.Popup;
            buttonHide.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonHide.ForeColor = Color.White;
            buttonHide.Location = new Point(937, 23);
            buttonHide.Name = "buttonHide";
            buttonHide.Size = new Size(41, 32);
            buttonHide.TabIndex = 21;
            buttonHide.Text = "-";
            buttonHide.UseVisualStyleBackColor = false;
            buttonHide.Click += buttonHide_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 7F);
            label7.ForeColor = Color.White;
            label7.Location = new Point(12, 500);
            label7.Name = "label7";
            label7.Size = new Size(1011, 12);
            label7.TabIndex = 22;
            label7.Text = resources.GetString("label7.Text");
            // 
            // CommandLord
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(54, 54, 54);
            ClientSize = new Size(1037, 531);
            Controls.Add(label7);
            Controls.Add(buttonHide);
            Controls.Add(label6);
            Controls.Add(buttonAddListFound);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(textBoxSearch);
            Controls.Add(buttonAddCmd);
            Controls.Add(listBoxCmd);
            Controls.Add(checkBoxHideNullValue);
            Controls.Add(buttonExit);
            Controls.Add(checkBoxKillAndRun);
            Controls.Add(buttonAllClear);
            Controls.Add(listBoxFound);
            Controls.Add(checkBoxKill);
            Controls.Add(checkBoxAllApps);
            Controls.Add(listenListBox);
            Controls.Add(textBoxListen);
            Controls.Add(buttonAddListBox);
            Controls.Add(buttonListen);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CommandLord";
            StartPosition = FormStartPosition.CenterParent;
            Text = "CommandLord";
            Load += CommandLord_Load;
            KeyDown += CommandLord_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonListen;
        private Button buttonAddListBox;
        private TextBox textBoxListen;
        private ListBox listenListBox;
        private CheckBox checkBoxAllApps;
        private CheckBox checkBoxKill;
        private ListBox listBoxFound;
        private Button buttonAllClear;
        private CheckBox checkBoxKillAndRun;
        private Button buttonExit;
        private CheckBox checkBoxHideNullValue;
        private ListBox listBoxCmd;
        private Button buttonAddCmd;
        private TextBox textBoxSearch;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button buttonAddListFound;
        private Label label6;
        private Button buttonHide;
        private Label label7;
    }
}