using System;
using System.Drawing;
using System.Windows.Forms;
using Back;
namespace DamkaUI
{
    public partial class gameSettingsForm : Form
    {
        private bool m_EnsuredDone = false;
        private bool m_ClosedByDone = false;

        public gameSettingsForm()
        {
            this.Text = "Game Settings";
            this.Size = new Size(250, 230);
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            InitializeComponent();
            this.buttonDone.Click += buttons_Click;
            this.radioButton6x6.Click += radioButton6x6_Click;
            this.radioButton8x8.Click += radioButton8x8_Click;
            this.radioButton10x10.Click += radioButton10x10_Click;
            this.checkBoxPlayer2.Click += checkBoxPlayer2_Click;
            this.textBoxPlayer1.Click += textBoxPlayer1_Click;
            this.textBoxPlayer2.Click += textBoxPlayer2_Click;
        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBoxPlayer2.Text;
            }
        }

        public RadioButton RadioButton6x6
        {
            get
            {
                return radioButton6x6;
            }
        }

        public RadioButton RadioButton8x8
        {
            get
            {
                return radioButton8x8;
            }
        }
        public RadioButton RadioButton10x10
        {
            get
            {
                return radioButton10x10;
            }
        }

        public CheckBox CheckBoxPlayer2
        {
            get
            {
                return checkBoxPlayer2;
            }
        }

        public bool ClosedByDone
        {
            get
            {
                return this.m_ClosedByDone;
            }
        }

        private void textBoxPlayer2_Click(object sender, EventArgs e)
        {
            this.textBoxPlayer2.Text.ToString();
        }

        private void textBoxPlayer1_Click(object sender, EventArgs e)
        {
            this.textBoxPlayer1.Text.ToString();
        }

        private void checkBoxPlayer2_Click(object sender, EventArgs e)
        {
            if (checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Enabled = true;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
            }
        }

        private void CheckBoxPlayer2_CheckedChanged(Object sender, EventArgs e)
        {
           
        }

        private void radioButton10x10_Click(object sender, EventArgs e)
        {
            this.radioButton10x10.Checked = true;
        }

        private void radioButton8x8_Click(object sender, EventArgs e)
        {
            this.radioButton8x8.Checked = true;
        }

        private void radioButton6x6_Click(object sender, EventArgs e)
        {
            this.radioButton6x6.Checked = true;
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            m_ClosedByDone = sender == buttonDone;
            if (m_ClosedByDone)
            {
                if (ensuredValidDone())
                {
                    createDamkaBoard();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void radioButton6x6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private bool ensuredValidDone()
        {
            bool validPlayer2 = (checkBoxPlayer2.Checked && checkPlayerName(textBoxPlayer2.Text)) ||
                        !checkBoxPlayer2.Checked;

            if (!m_EnsuredDone)
            {
                if (checkPlayerName(textBoxPlayer1.Text) && this.checkedRadioButton() && validPlayer2)
                {
                    m_EnsuredDone = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong name", "Invalid name", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }

            return m_EnsuredDone;
        }

        private bool checkedRadioButton()
        {
            return radioButton6x6.Checked || radioButton8x8.Checked || radioButton10x10.Checked;
        }

        private static bool checkPlayerName(string i_Name)
        {
            return Rules.IsLegalInput1(i_Name);
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void createDamkaBoard()
        {
           this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxPlayer2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPlayer1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
