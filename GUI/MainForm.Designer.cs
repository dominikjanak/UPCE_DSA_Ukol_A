using GUI.Drawing;

namespace GUI
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.RemoveEdgeButton = new System.Windows.Forms.Button();
            this.RemoveVertexButton = new System.Windows.Forms.Button();
            this.AddEdgeButton = new System.Windows.Forms.Button();
            this.AddVertexButton = new System.Windows.Forms.Button();
            this.TrajectoryMatrixButton = new System.Windows.Forms.Button();
            this.FindRouteButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewGraphStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.LoadDataStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveDataStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsDataStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoloadStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoloadSaveStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoloadLoadStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveImageStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutProgramButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgramHelpButton = new System.Windows.Forms.ToolStripMenuItem();
            this.grafToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddVertexStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.odeberVrcholToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.přidatHranuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odebratHranuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.najítCestuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zobrazitMaticiTrajektoriíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.ostatníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateGraphStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.graphCanvas = new GUI.DoubleBufferedPanel();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.Controls.Add(this.graphCanvas, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(563, 380);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.CloseButton, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.RemoveEdgeButton, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.RemoveVertexButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.AddEdgeButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.AddVertexButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.TrajectoryMatrixButton, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.FindRouteButton, 0, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(436, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(127, 378);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CloseButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CloseButton.Location = new System.Drawing.Point(0, 343);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(127, 32);
            this.CloseButton.TabIndex = 7;
            this.CloseButton.Text = "Ukončit";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
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
            this.RemoveEdgeButton.Size = new System.Drawing.Size(127, 32);
            this.RemoveEdgeButton.TabIndex = 4;
            this.RemoveEdgeButton.Text = "Odstranit hranu";
            this.RemoveEdgeButton.UseVisualStyleBackColor = false;
            this.RemoveEdgeButton.Click += new System.EventHandler(this.RemoveEdgeButton_Click);
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
            this.RemoveVertexButton.Size = new System.Drawing.Size(127, 32);
            this.RemoveVertexButton.TabIndex = 3;
            this.RemoveVertexButton.Text = "Odstranit vrchol";
            this.RemoveVertexButton.UseVisualStyleBackColor = false;
            this.RemoveVertexButton.Click += new System.EventHandler(this.RemoveVertexButton_Click);
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
            this.AddEdgeButton.Size = new System.Drawing.Size(127, 32);
            this.AddEdgeButton.TabIndex = 2;
            this.AddEdgeButton.Text = "Přidat hranu";
            this.AddEdgeButton.UseVisualStyleBackColor = false;
            this.AddEdgeButton.Click += new System.EventHandler(this.AddEdgeButton_Click);
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
            this.AddVertexButton.Size = new System.Drawing.Size(127, 32);
            this.AddVertexButton.TabIndex = 1;
            this.AddVertexButton.Text = "Přidat vrchol";
            this.AddVertexButton.UseVisualStyleBackColor = false;
            this.AddVertexButton.Click += new System.EventHandler(this.AddVertexButton_Click);
            // 
            // TrajectoryMatrixButton
            // 
            this.TrajectoryMatrixButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.TrajectoryMatrixButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TrajectoryMatrixButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TrajectoryMatrixButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.TrajectoryMatrixButton.Location = new System.Drawing.Point(0, 175);
            this.TrajectoryMatrixButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.TrajectoryMatrixButton.Name = "TrajectoryMatrixButton";
            this.TrajectoryMatrixButton.Size = new System.Drawing.Size(127, 32);
            this.TrajectoryMatrixButton.TabIndex = 5;
            this.TrajectoryMatrixButton.Text = "Matice trajektorií";
            this.TrajectoryMatrixButton.UseVisualStyleBackColor = false;
            this.TrajectoryMatrixButton.Click += new System.EventHandler(this.TrajectoryMatrixButton_Click);
            // 
            // FindRouteButton
            // 
            this.FindRouteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.FindRouteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindRouteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FindRouteButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FindRouteButton.Location = new System.Drawing.Point(0, 210);
            this.FindRouteButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.FindRouteButton.Name = "FindRouteButton";
            this.FindRouteButton.Size = new System.Drawing.Size(127, 32);
            this.FindRouteButton.TabIndex = 6;
            this.FindRouteButton.Text = "Najít cestu";
            this.FindRouteButton.UseVisualStyleBackColor = false;
            this.FindRouteButton.Click += new System.EventHandler(this.FindRouteButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.AboutProgramButton,
            this.ProgramHelpButton,
            this.grafToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(587, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewGraphStrip,
            this.toolStripMenuItem3,
            this.LoadDataStrip,
            this.SaveDataStrip,
            this.SaveAsDataStrip,
            this.AutoloadStrip,
            this.toolStripMenuItem1,
            this.SaveImageStrip,
            this.toolStripMenuItem2,
            this.CloseStrip});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // NewGraphStrip
            // 
            this.NewGraphStrip.Name = "NewGraphStrip";
            this.NewGraphStrip.Size = new System.Drawing.Size(148, 22);
            this.NewGraphStrip.Text = "Nový";
            this.NewGraphStrip.Click += new System.EventHandler(this.NewGraphButton_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(145, 6);
            // 
            // LoadDataStrip
            // 
            this.LoadDataStrip.Name = "LoadDataStrip";
            this.LoadDataStrip.Size = new System.Drawing.Size(148, 22);
            this.LoadDataStrip.Text = "Načíst";
            this.LoadDataStrip.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveDataStrip
            // 
            this.SaveDataStrip.Name = "SaveDataStrip";
            this.SaveDataStrip.Size = new System.Drawing.Size(148, 22);
            this.SaveDataStrip.Text = "Uložit";
            this.SaveDataStrip.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveAsDataStrip
            // 
            this.SaveAsDataStrip.Name = "SaveAsDataStrip";
            this.SaveAsDataStrip.Size = new System.Drawing.Size(148, 22);
            this.SaveAsDataStrip.Text = "Uložit jako...";
            this.SaveAsDataStrip.Click += new System.EventHandler(this.SaveAsButton_Click);
            // 
            // AutoloadStrip
            // 
            this.AutoloadStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoloadSaveStrip,
            this.AutoloadLoadStrip});
            this.AutoloadStrip.Name = "AutoloadStrip";
            this.AutoloadStrip.Size = new System.Drawing.Size(148, 22);
            this.AutoloadStrip.Text = "Autoload";
            // 
            // AutoloadSaveStrip
            // 
            this.AutoloadSaveStrip.Name = "AutoloadSaveStrip";
            this.AutoloadSaveStrip.Size = new System.Drawing.Size(107, 22);
            this.AutoloadSaveStrip.Text = "Uložit";
            this.AutoloadSaveStrip.Click += new System.EventHandler(this.Autoload_SaveButton_Click);
            // 
            // AutoloadLoadStrip
            // 
            this.AutoloadLoadStrip.Name = "AutoloadLoadStrip";
            this.AutoloadLoadStrip.Size = new System.Drawing.Size(107, 22);
            this.AutoloadLoadStrip.Text = "Načíst";
            this.AutoloadLoadStrip.Click += new System.EventHandler(this.Autoload_LoadButton_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // SaveImageStrip
            // 
            this.SaveImageStrip.Name = "SaveImageStrip";
            this.SaveImageStrip.Size = new System.Drawing.Size(148, 22);
            this.SaveImageStrip.Text = "Uložit obrázek";
            this.SaveImageStrip.Click += new System.EventHandler(this.saveImageButton_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 6);
            // 
            // CloseStrip
            // 
            this.CloseStrip.Name = "CloseStrip";
            this.CloseStrip.Size = new System.Drawing.Size(148, 22);
            this.CloseStrip.Text = "Ukončit";
            this.CloseStrip.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // AboutProgramButton
            // 
            this.AboutProgramButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.AboutProgramButton.Name = "AboutProgramButton";
            this.AboutProgramButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AboutProgramButton.Size = new System.Drawing.Size(84, 20);
            this.AboutProgramButton.Text = "O programu";
            this.AboutProgramButton.Click += new System.EventHandler(this.AboutProgramButton_Click);
            // 
            // ProgramHelpButton
            // 
            this.ProgramHelpButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ProgramHelpButton.Name = "ProgramHelpButton";
            this.ProgramHelpButton.Size = new System.Drawing.Size(73, 20);
            this.ProgramHelpButton.Text = "Nápověda";
            this.ProgramHelpButton.Click += new System.EventHandler(this.ProgramHelpButton_Click);
            // 
            // grafToolStripMenuItem
            // 
            this.grafToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddVertexStrip,
            this.odeberVrcholToolStripMenuItem,
            this.toolStripMenuItem4,
            this.přidatHranuToolStripMenuItem,
            this.odebratHranuToolStripMenuItem,
            this.toolStripMenuItem5,
            this.najítCestuToolStripMenuItem,
            this.zobrazitMaticiTrajektoriíToolStripMenuItem,
            this.toolStripMenuItem6,
            this.ostatníToolStripMenuItem});
            this.grafToolStripMenuItem.Name = "grafToolStripMenuItem";
            this.grafToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.grafToolStripMenuItem.Text = "Graf";
            // 
            // AddVertexStrip
            // 
            this.AddVertexStrip.Name = "AddVertexStrip";
            this.AddVertexStrip.Size = new System.Drawing.Size(206, 22);
            this.AddVertexStrip.Text = "Přidat vrchol";
            this.AddVertexStrip.Click += new System.EventHandler(this.AddVertexButton_Click);
            // 
            // odeberVrcholToolStripMenuItem
            // 
            this.odeberVrcholToolStripMenuItem.Name = "odeberVrcholToolStripMenuItem";
            this.odeberVrcholToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.odeberVrcholToolStripMenuItem.Text = "Odebrat vrchol";
            this.odeberVrcholToolStripMenuItem.Click += new System.EventHandler(this.RemoveVertexButton_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(203, 6);
            // 
            // přidatHranuToolStripMenuItem
            // 
            this.přidatHranuToolStripMenuItem.Name = "přidatHranuToolStripMenuItem";
            this.přidatHranuToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.přidatHranuToolStripMenuItem.Text = "Přidat hranu";
            this.přidatHranuToolStripMenuItem.Click += new System.EventHandler(this.AddEdgeButton_Click);
            // 
            // odebratHranuToolStripMenuItem
            // 
            this.odebratHranuToolStripMenuItem.Name = "odebratHranuToolStripMenuItem";
            this.odebratHranuToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.odebratHranuToolStripMenuItem.Text = "Odebrat hranu";
            this.odebratHranuToolStripMenuItem.Click += new System.EventHandler(this.RemoveEdgeButton_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(203, 6);
            // 
            // najítCestuToolStripMenuItem
            // 
            this.najítCestuToolStripMenuItem.Name = "najítCestuToolStripMenuItem";
            this.najítCestuToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.najítCestuToolStripMenuItem.Text = "Najít cestu";
            this.najítCestuToolStripMenuItem.Click += new System.EventHandler(this.FindRouteButton_Click);
            // 
            // zobrazitMaticiTrajektoriíToolStripMenuItem
            // 
            this.zobrazitMaticiTrajektoriíToolStripMenuItem.Name = "zobrazitMaticiTrajektoriíToolStripMenuItem";
            this.zobrazitMaticiTrajektoriíToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.zobrazitMaticiTrajektoriíToolStripMenuItem.Text = "Zobrazit matici trajektorií";
            this.zobrazitMaticiTrajektoriíToolStripMenuItem.Click += new System.EventHandler(this.TrajectoryMatrixButton_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(203, 6);
            // 
            // ostatníToolStripMenuItem
            // 
            this.ostatníToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GenerateGraphStrip});
            this.ostatníToolStripMenuItem.Name = "ostatníToolStripMenuItem";
            this.ostatníToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ostatníToolStripMenuItem.Text = "Ostatní";
            // 
            // GenerateGraphStrip
            // 
            this.GenerateGraphStrip.Name = "GenerateGraphStrip";
            this.GenerateGraphStrip.Size = new System.Drawing.Size(139, 22);
            this.GenerateGraphStrip.Text = "Generuj graf";
            this.GenerateGraphStrip.Click += new System.EventHandler(this.GenerateGraphStrip_Click);
            // 
            // graphCanvas
            // 
            this.graphCanvas.BackColor = System.Drawing.Color.White;
            this.graphCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphCanvas.Location = new System.Drawing.Point(3, 2);
            this.graphCanvas.Margin = new System.Windows.Forms.Padding(3, 2, 0, 3);
            this.graphCanvas.Name = "graphCanvas";
            this.graphCanvas.Size = new System.Drawing.Size(430, 375);
            this.graphCanvas.TabIndex = 0;
            this.graphCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.graphCanvas_Paint);
            this.graphCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graphCanvas_MouseDown);
            this.graphCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphCanvas_MouseMove);
            this.graphCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.graphCanvas_MouseUp);
            this.graphCanvas.Resize += new System.EventHandler(this.graphCanvas_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 419);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Dynamic Title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Move += new System.EventHandler(this.MainForm_Move);
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
        private System.Windows.Forms.ToolStripMenuItem SaveImageStrip;
        private System.Windows.Forms.ToolStripMenuItem CloseStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveAsDataStrip;
        private System.Windows.Forms.ToolStripMenuItem LoadDataStrip;
        private System.Windows.Forms.Button RemoveEdgeButton;
        private System.Windows.Forms.Button RemoveVertexButton;
        private System.Windows.Forms.Button AddEdgeButton;
        private System.Windows.Forms.Button AddVertexButton;
        private System.Windows.Forms.ToolStripMenuItem AutoloadStrip;
        private System.Windows.Forms.ToolStripMenuItem AutoloadSaveStrip;
        private System.Windows.Forms.ToolStripMenuItem AutoloadLoadStrip;
        private System.Windows.Forms.Button TrajectoryMatrixButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button FindRouteButton;
        private System.Windows.Forms.ToolStripMenuItem AboutProgramButton;
        private GUI.DoubleBufferedPanel graphCanvas;
        private System.Windows.Forms.ToolStripMenuItem NewGraphStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveDataStrip;
        private System.Windows.Forms.ToolStripMenuItem ProgramHelpButton;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem grafToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddVertexStrip;
        private System.Windows.Forms.ToolStripMenuItem odeberVrcholToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem přidatHranuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odebratHranuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem najítCestuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zobrazitMaticiTrajektoriíToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem ostatníToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GenerateGraphStrip;
    }
}

