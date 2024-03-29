﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class SelectTwoVerticesDialog : Form
    {
        public string StartVertex { get; set; }
        public string TargetVertex { get; set; }
        public bool Starship { get; set; }

        public SelectTwoVerticesDialog(bool findPath = false)
        {
            InitializeComponent();
            if (findPath)
            {
                Text = "Vyhledat cestu";
                titleLabel.Text = "Vyhledat cestu";
                startLabel.Text = "Start:";
                targetLabel.Text = "Cíl:";
                DialogSubmitButton.Text = "Najít";
                tableLayoutPanel2.ColumnStyles[0].Width = 43;
                tableLayoutPanel4.ColumnStyles[0].Width = 43;
            }
            else
            {
                tableLayoutPanel5.Dispose();
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

            }

            Point position = Properties.Settings.Default.DialogPosition;
            if (!(position.X <= -1000 && position.Y <= -1000))
            {
                StartPosition = FormStartPosition.Manual;
                Location = position;
            }
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            StartVertex = StartTextbox.Text;
            TargetVertex = TargetTextbox.Text;
            Starship = StarshipCheckbox.Checked;
        }

        private void SelectEdgeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool startValid = ValidateText(StartTextbox);
            bool targetValid = ValidateText(TargetTextbox);
            if (DialogResult.OK == this.DialogResult && (!startValid || !targetValid))
            {
                e.Cancel = true;
            }
        }

        private void Textbox_TextChanged(object sender, EventArgs e)
        {
            ValidateText((TextBox)sender);
        }

        private bool ValidateText(TextBox box, bool highlight = true)
        {
            if (box.Text.Length > 0)
            {
                if (highlight)
                {
                    box.BackColor = Color.White;
                }
                return true;
            }
            else
            {
                if (highlight)
                {
                    box.BackColor = Color.FromArgb(255, 200, 200);
                }
            }
            return false;
        }

        private void FormSubmit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                bool submit = true;

                if (!ValidateText(TargetTextbox, false))
                {
                    TargetTextbox.Focus();
                    submit = false;
                }

                if (!ValidateText(StartTextbox, false))
                {
                    StartTextbox.Focus();
                    submit = false;
                }

                if (submit)
                {
                    this.DialogSubmitButton.PerformClick();
                }
            }
        }

        private void SelectTwoVerticesDialog_Move(object sender, EventArgs e)
        {
            Properties.Settings.Default.DialogPosition = Location;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == 256)
            {
                if (keyData == Keys.Escape)
                {
                    this.Close();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
