using GUI.Drawing;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GUI.Dialog
{
    public partial class GenerateDialog : Form
    {
        public int JunctionsCount { get; set; }
        public int RestAreasCount { get; set; }
        public int StopsCount { get; set; }

        public int MinVertexEdge { get; set; }
        public int MaxVertexEdge { get; set; }


        public GenerateDialog()
        {
            InitializeComponent();
            JunctionsCount = 0;
            RestAreasCount = 0;
            StopsCount = 0;
            MinVertexEdge = 0;
            MaxVertexEdge = 0;

            Point position = Properties.Settings.Default.DialogPosition;
            if (!(position.X <= -1000 && position.Y <= -1000))
            {
                StartPosition = FormStartPosition.Manual;
                Location = position;
            }
        }

        private void DialogSubmitButton_Click(object sender, EventArgs e)
        {
            int number;

            if (int.TryParse(JunctionsTextbox.Text, out number))
            {
                JunctionsCount = number;
            }
            if (int.TryParse(RestAreasTextbox.Text, out number))
            {
                RestAreasCount = number;
            }
            if (int.TryParse(StopsTextbox.Text, out number))
            {
                StopsCount = number;
            }

            if (int.TryParse(MinEdgesTextbox.Text, out number))
            {
                MinVertexEdge = number;
            }
            if (int.TryParse(MaxEdgesTextbox.Text, out number))
            {
                MaxVertexEdge = number;
            }
        }

        private void VertexDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool res = ValidatePositiveInt(JunctionsTextbox) 
                && ValidatePositiveInt(RestAreasTextbox) 
                && ValidatePositiveInt(StopsTextbox) 
                && ValidatePositiveInt(MinEdgesTextbox)
                && ValidatePositiveInt(MaxEdgesTextbox);

            if (DialogResult.OK == this.DialogResult && !res)
            {
                e.Cancel = true;
            }
        }

        private bool ValidatePositiveInt(TextBox box, bool highlight = true)
        {
            int num;
            Color color = Colors.White;
            bool state = true;


            if (int.TryParse(box.Text, out num))
            {
                if (num < 0)
                {
                    color = Color.FromArgb(255, 200, 200);
                    state = false;
                }
            }
            else
            {
                color = Color.FromArgb(255, 200, 200);
                state = false;
            }

            if (highlight)
            {
                box.BackColor = color;
            }
            return state;
        }

        private void FormSubmit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                bool submit = true;


                if (!ValidatePositiveInt(MaxEdgesTextbox, false))
                {
                    MaxEdgesTextbox.Focus();
                    submit = false;
                }

                if (!ValidatePositiveInt(MinEdgesTextbox, false))
                {
                    MinEdgesTextbox.Focus();
                    submit = false;
                }

                if (!ValidatePositiveInt(StopsTextbox, false))
                {
                    StopsTextbox.Focus();
                    submit = false;
                }

                if (!ValidatePositiveInt(RestAreasTextbox, false))
                {
                    RestAreasTextbox.Focus();
                    submit = false;
                }

                if (!ValidatePositiveInt(JunctionsTextbox, false))
                {
                    JunctionsTextbox.Focus();
                    submit = false;
                }

                if (submit)
                {
                    this.DialogSubmitButton.PerformClick();
                }
            }
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

        private void VertexCount_TextChanged(object sender, EventArgs e)
        {
            ValidatePositiveInt((TextBox)sender);
        }

        private void EdgesCount_TextChanged(object sender, EventArgs e)
        {
            ValidatePositiveInt((TextBox)sender);
        }

        private void VertexDialog_Move(object sender, EventArgs e)
        {
            Properties.Settings.Default.DialogPosition = Location;
        }
    }
}
