using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIEjercicio6T4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const string cadena = "123456789*0#";
        private Button button;
        private void Form1_Load(object sender, EventArgs e)
        {
            int cont = 3;
            Form2 f = new Form2();
            DialogResult res;

            bool flag = true;

            while (flag)
            {
                res = f.ShowDialog();
                switch (res)
                {
                    case DialogResult.OK:
                        if (f.textBox1.Text.ToUpper() == "AAAA")
                        {
                            MessageBox.Show("Contraseña Aceptada", "Checking",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flag = false;
                        }
                        else
                        {
                            cont--;
                            MessageBox.Show("Contraseña Incorrecta\nQuedan: " + cont + " intentos"
                                , "Checking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            f.textBox1.Text = "";

                            if (cont <= 0)
                            {
                                flag = false;
                                this.Close();
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        flag = false;
                        this.Close();
                        break;
                }
            }

            int coordenadaX = 40, coordenadaY = 50;
            for (int i = 0; i < cadena.Length; i++)
            {
                button = new Button();

                coordenadaX += 40;

                button.Text = cadena.Substring(i, 1);
                button.Location = new Point(coordenadaX, coordenadaY);
                button.Name = "button" + cadena.Substring(i, 1);
                button.Size = new Size(40, 30);
                button.Enabled = true;
                button.TabIndex = i;

                if ((i + 1) % 3 == 0)
                {
                    coordenadaY += 40;
                    coordenadaX = 40;
                }

                this.button.Click += new System.EventHandler(this.buttonClick);
                this.button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonMouseMove);
                this.button.MouseLeave += new System.EventHandler(this.buttonMouseLeave);
                this.Controls.Add(button);
            }
        }

        private void buttonMouseLeave(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor != Color.Red)
            {
                ((Button)sender).BackColor = Control.DefaultBackColor;
            }
        }

        private void buttonMouseMove(object sender, MouseEventArgs e)
        {
            if (((Button)sender).BackColor != Color.Red)
            {
                ((Button)sender).BackColor = Color.BlueViolet;
            }
        }

        private void buttonClick(object sender, EventArgs e)
        {
            textBox1.Text += ((Button)sender).Text;
            ((Button)sender).BackColor = Color.Red;
        }

        private void Buttonreset_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            foreach (Control item in this.Controls)
            {
                if (item.GetType() == typeof(System.Windows.Forms.Button))
                {
                    item.BackColor=DefaultBackColor;
                }
            } 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        private void saveNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length > 0)
            {
               
                saveFileDialog.Title = "Seleccion de directorio para guardar el número";
                saveFileDialog.InitialDirectory = "C:\\";
                saveFileDialog.Filter = "Texto (*.txt)| *.txt|Todos los archivos|*.*";
                saveFileDialog.ValidateNames = true;
                saveFileDialog.OverwritePrompt = false;
               
                saveFileDialog.ShowDialog();
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName,true))
                    {
                        sw.WriteLine(textBox1.Text);
                    }
                }
                catch (ArgumentException)
                {

                }
            }
        }
    }
}
