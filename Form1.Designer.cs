namespace ADAB
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AddItemButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ChangeItemButton = new System.Windows.Forms.Button();
            this.DeleteItemButton = new System.Windows.Forms.Button();
            this.SaveItemButton = new System.Windows.Forms.Button();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.lbStatusText = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.afqhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qweqweqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьНовуюКнигуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переименоватьКнигуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьКнигуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.импортИзНедавнихСеансовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qweqweToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.MoveItemButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 76);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(271, 303);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(368, 73);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(214, 20);
            this.textBox1.TabIndex = 4;
            // 
            // AddItemButton
            // 
            this.AddItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddItemButton.Location = new System.Drawing.Point(292, 245);
            this.AddItemButton.Name = "AddItemButton";
            this.AddItemButton.Size = new System.Drawing.Size(84, 29);
            this.AddItemButton.TabIndex = 9;
            this.AddItemButton.Text = "Добавить";
            this.AddItemButton.UseVisualStyleBackColor = true;
            this.AddItemButton.Click += new System.EventHandler(this.AddItemButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(368, 99);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(214, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(368, 128);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(214, 20);
            this.textBox3.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Псевдоним";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(289, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Имя";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Комментарий";
            // 
            // ChangeItemButton
            // 
            this.ChangeItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ChangeItemButton.Location = new System.Drawing.Point(292, 280);
            this.ChangeItemButton.Name = "ChangeItemButton";
            this.ChangeItemButton.Size = new System.Drawing.Size(84, 29);
            this.ChangeItemButton.TabIndex = 10;
            this.ChangeItemButton.Text = "Изменить";
            this.ChangeItemButton.UseVisualStyleBackColor = true;
            this.ChangeItemButton.Click += new System.EventHandler(this.ChangeItemButton_Click);
            // 
            // DeleteItemButton
            // 
            this.DeleteItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteItemButton.Location = new System.Drawing.Point(292, 350);
            this.DeleteItemButton.Name = "DeleteItemButton";
            this.DeleteItemButton.Size = new System.Drawing.Size(84, 29);
            this.DeleteItemButton.TabIndex = 11;
            this.DeleteItemButton.Text = "Удалить";
            this.DeleteItemButton.UseVisualStyleBackColor = true;
            this.DeleteItemButton.Click += new System.EventHandler(this.DeleteItemButton_Click);
            // 
            // SaveItemButton
            // 
            this.SaveItemButton.Location = new System.Drawing.Point(498, 203);
            this.SaveItemButton.Name = "SaveItemButton";
            this.SaveItemButton.Size = new System.Drawing.Size(84, 29);
            this.SaveItemButton.TabIndex = 8;
            this.SaveItemButton.Text = "Сохранить";
            this.SaveItemButton.UseVisualStyleBackColor = true;
            this.SaveItemButton.Click += new System.EventHandler(this.SaveItemButton_Click);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SearchTextBox.Location = new System.Drawing.Point(12, 392);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(271, 20);
            this.SearchTextBox.TabIndex = 3;
            this.SearchTextBox.Text = "Поиск...";
            // 
            // lbStatusText
            // 
            this.lbStatusText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStatusText.AutoEllipsis = true;
            this.lbStatusText.AutoSize = true;
            this.lbStatusText.Location = new System.Drawing.Point(509, 33);
            this.lbStatusText.Name = "lbStatusText";
            this.lbStatusText.Size = new System.Drawing.Size(66, 13);
            this.lbStatusText.TabIndex = 16;
            this.lbStatusText.Text = "lbStatusText";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 49);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(271, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.afqhToolStripMenuItem,
            this.qweqweqToolStripMenuItem,
            this.qweqweToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(587, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // afqhToolStripMenuItem
            // 
            this.afqhToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.afqhToolStripMenuItem.Name = "afqhToolStripMenuItem";
            this.afqhToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.afqhToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // qweqweqToolStripMenuItem
            // 
            this.qweqweqToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьНовуюКнигуToolStripMenuItem,
            this.переименоватьКнигуToolStripMenuItem,
            this.удалитьКнигуToolStripMenuItem,
            this.toolStripSeparator1,
            this.импортИзНедавнихСеансовToolStripMenuItem});
            this.qweqweqToolStripMenuItem.Name = "qweqweqToolStripMenuItem";
            this.qweqweqToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.qweqweqToolStripMenuItem.Text = "Сервис";
            // 
            // создатьНовуюКнигуToolStripMenuItem
            // 
            this.создатьНовуюКнигуToolStripMenuItem.Name = "создатьНовуюКнигуToolStripMenuItem";
            this.создатьНовуюКнигуToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.создатьНовуюКнигуToolStripMenuItem.Text = "Создать новую книгу...";
            this.создатьНовуюКнигуToolStripMenuItem.Click += new System.EventHandler(this.создатьНовуюКнигуToolStripMenuItem_Click);
            // 
            // переименоватьКнигуToolStripMenuItem
            // 
            this.переименоватьКнигуToolStripMenuItem.Name = "переименоватьКнигуToolStripMenuItem";
            this.переименоватьКнигуToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.переименоватьКнигуToolStripMenuItem.Text = "Переименовать книгу...";
            this.переименоватьКнигуToolStripMenuItem.Click += new System.EventHandler(this.переименоватьКнигуToolStripMenuItem_Click);
            // 
            // удалитьКнигуToolStripMenuItem
            // 
            this.удалитьКнигуToolStripMenuItem.Name = "удалитьКнигуToolStripMenuItem";
            this.удалитьКнигуToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.удалитьКнигуToolStripMenuItem.Text = "Удалить книгу...";
            this.удалитьКнигуToolStripMenuItem.Click += new System.EventHandler(this.удалитьКнигуToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(239, 6);
            // 
            // импортИзНедавнихСеансовToolStripMenuItem
            // 
            this.импортИзНедавнихСеансовToolStripMenuItem.Name = "импортИзНедавнихСеансовToolStripMenuItem";
            this.импортИзНедавнихСеансовToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.импортИзНедавнихСеансовToolStripMenuItem.Text = "Импорт из недавних сеансов...";
            this.импортИзНедавнихСеансовToolStripMenuItem.Click += new System.EventHandler(this.импортИзНедавнихСеансовToolStripMenuItem_Click);
            // 
            // qweqweToolStripMenuItem1
            // 
            this.qweqweToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.qweqweToolStripMenuItem1.Name = "qweqweToolStripMenuItem1";
            this.qweqweToolStripMenuItem1.Size = new System.Drawing.Size(65, 20);
            this.qweqweToolStripMenuItem1.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Адресная книга:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(368, 155);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(214, 42);
            this.textBox4.TabIndex = 7;
            // 
            // MoveItemButton
            // 
            this.MoveItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MoveItemButton.Location = new System.Drawing.Point(292, 315);
            this.MoveItemButton.Name = "MoveItemButton";
            this.MoveItemButton.Size = new System.Drawing.Size(84, 29);
            this.MoveItemButton.TabIndex = 21;
            this.MoveItemButton.Text = "Переместить";
            this.MoveItemButton.UseVisualStyleBackColor = true;
            this.MoveItemButton.Click += new System.EventHandler(this.MoveItemButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 424);
            this.Controls.Add(this.MoveItemButton);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lbStatusText);
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.SaveItemButton);
            this.Controls.Add(this.DeleteItemButton);
            this.Controls.Add(this.ChangeItemButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.AddItemButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button AddItemButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ChangeItemButton;
        private System.Windows.Forms.Button DeleteItemButton;
        private System.Windows.Forms.Button SaveItemButton;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Label lbStatusText;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem afqhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qweqweqToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qweqweToolStripMenuItem1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьНовуюКнигуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переименоватьКнигуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьКнигуToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem импортИзНедавнихСеансовToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button MoveItemButton;
    }
}

