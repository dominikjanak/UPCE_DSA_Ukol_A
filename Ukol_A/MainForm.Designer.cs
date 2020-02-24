namespace Ukol_A
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveDataButton = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadDataButton = new System.Windows.Forms.ToolStripMenuItem();
            this.graphCanvas = new Ukol_A.DoubleBufferedPanel();
            this.AddVertexButton = new System.Windows.Forms.Button();
            this.AddEdgeButton = new System.Windows.Forms.Button();
            this.RemoveVertexButton = new System.Windows.Forms.Button();
            this.RemoveEdgeButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.graphCanvas, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 522);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.RemoveEdgeButton, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.RemoveVertexButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.AddEdgeButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.AddVertexButton, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(613, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(147, 520);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageMenuItem,
            this.SaveDataButton,
            this.LoadDataButton,
            this.closeMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // saveImageMenuItem
            // 
            this.saveImageMenuItem.Name = "saveImageMenuItem";
            this.saveImageMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveImageMenuItem.Text = "Uložit obrázek";
            this.saveImageMenuItem.Click += new System.EventHandler(this.saveImageButton_Click);
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.Size = new System.Drawing.Size(148, 22);
            this.closeMenuItem.Text = "Ukončit";
            this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // SaveDataButton
            // 
            this.SaveDataButton.Name = "SaveDataButton";
            this.SaveDataButton.Size = new System.Drawing.Size(148, 22);
            this.SaveDataButton.Text = "Uložit data";
            this.SaveDataButton.Click += new System.EventHandler(this.SaveDataButton_Click);
            // 
            // LoadDataButton
            // 
            this.LoadDataButton.Name = "LoadDataButton";
            this.LoadDataButton.Size = new System.Drawing.Size(148, 22);
            this.LoadDataButton.Text = "Načíst data";
            this.LoadDataButton.Click += new System.EventHandler(this.LoadDataButton_Click);
            // 
            // graphCanvas
            // 
            this.graphCanvas.BackColor = System.Drawing.Color.White;
            this.graphCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphCanvas.Location = new System.Drawing.Point(3, 2);
            this.graphCanvas.Margin = new System.Windows.Forms.Padding(3, 2, 0, 3);
            this.graphCanvas.Name = "graphCanvas";
            this.graphCanvas.Size = new System.Drawing.Size(607, 517);
            this.graphCanvas.TabIndex = 0;
            this.graphCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.graphCanvas_Paint);
            this.graphCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graphCanvas_MouseDown);
            this.graphCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphCanvas_MouseMove);
            this.graphCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.graphCanvas_MouseUp);
            this.graphCanvas.Resize += new System.EventHandler(this.graphCanvas_Resize);
            // 
            // AddVertexButton
            // 
            this.AddVertexButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.AddVertexButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AddVertexButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.AddVertexButton.Location = new System.Drawing.Point(0, 0);
            this.AddVertexButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.AddVertexButton.Name = "AddVertexButton";
            this.AddVertexButton.Size = new System.Drawing.Size(147, 32);
            this.AddVertexButton.TabIndex = 0;
            this.AddVertexButton.Text = "Přidat vrchol";
            this.AddVertexButton.UseVisualStyleBackColor = false;
            this.AddVertexButton.Click += new System.EventHandler(this.AddVertexButton_Click);
            // 
            // AddEdgeButton
            // 
            this.AddEdgeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.AddEdgeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddEdgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AddEdgeButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.AddEdgeButton.Location = new System.Drawing.Point(0, 35);
            this.AddEdgeButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.AddEdgeButton.Name = "AddEdgeButton";
            this.AddEdgeButton.Size = new System.Drawing.Size(147, 32);
            this.AddEdgeButton.TabIndex = 1;
            this.AddEdgeButton.Text = "Přidat hranu";
            this.AddEdgeButton.UseVisualStyleBackColor = false;
            this.AddEdgeButton.Click += new System.EventHandler(this.AddEdgeButton_Click);
            // 
            // RemoveVertexButton
            // 
            this.RemoveVertexButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.RemoveVertexButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RemoveVertexButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RemoveVertexButton.Location = new System.Drawing.Point(0, 70);
            this.RemoveVertexButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.RemoveVertexButton.Name = "RemoveVertexButton";
            this.RemoveVertexButton.Size = new System.Drawing.Size(147, 32);
            this.RemoveVertexButton.TabIndex = 2;
            this.RemoveVertexButton.Text = "Odstranit vrchol";
            this.RemoveVertexButton.UseVisualStyleBackColor = false;
            this.RemoveVertexButton.Click += new System.EventHandler(this.RemoveVertexButton_Click);
            // 
            // RemoveEdgeButton
            // 
            this.RemoveEdgeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.RemoveEdgeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveEdgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RemoveEdgeButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.RemoveEdgeButton.Location = new System.Drawing.Point(0, 105);
            this.RemoveEdgeButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.RemoveEdgeButton.Name = "RemoveEdgeButton";
            this.RemoveEdgeButton.Size = new System.Drawing.Size(147, 32);
            this.RemoveEdgeButton.TabIndex = 2;
            this.RemoveEdgeButton.Text = "Odstranit hranu";
            this.RemoveEdgeButton.UseVisualStyleBackColor = false;
            this.RemoveEdgeButton.Click += new System.EventHandler(this.RemoveEdgeButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "UPCE DSA – Semestrální práce";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private DoubleBufferedPanel graphCanvas;
        private System.Windows.Forms.ToolStripMenuItem SaveDataButton;
        private System.Windows.Forms.ToolStripMenuItem LoadDataButton;
        private System.Windows.Forms.Button RemoveEdgeButton;
        private System.Windows.Forms.Button RemoveVertexButton;
        private System.Windows.Forms.Button AddEdgeButton;
        private System.Windows.Forms.Button AddVertexButton;
    }
}

