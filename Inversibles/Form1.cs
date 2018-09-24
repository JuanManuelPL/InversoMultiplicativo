using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Inversibles
{
    public partial class Form1 : Form
    {
        // Mover formulario con panel
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public static void Drag_Form(IntPtr Handle, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Drag_Form(Handle, e);
        }
        //INVERSIBLES
        int z = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                listBox1.Items.Clear();
                dataGridView1.Rows.Clear();
                bool aux = false;
                int Vaux = 0;
                z = Int32.Parse(textBox1.Text);
                for (int i = 0; i < z; i++)
                {
                    aux = AskInverso(i,z);
                    if (aux == true)
                    {
                        Vaux = AskValInverso(i,z);
                        dataGridView1.Rows.Add(i,Vaux);   
                    }else
                    {
                        listBox1.Items.Add(i);
                    }
                }
            }
        }
        public bool AskInverso(int a, int m)
        {
            //valor de retorno
            bool res = false;
            if (a != 0)
            {
                int c1 = 1;
                int c2 = -(m / a); //coeficiente de a y b respectivamente
                int t1 = 0;
                int t2 = 1; //coeficientes penultima corrida
                int r = m % a; //residuo, asignamos 1 como condicion de entrada 
                int x = a, y = r, c;
                while (r != 0)
                {
                    c = x / y;//cociente
                    r = x % y;//residuo
                              //guardamos valores temporales de los coeficientes
                              //multiplicamos los coeficiente por -1*cociente de la division
                    c1 *= -c;
                    c2 *= -c;
                    //sumamos la corrida anterior
                    c1 += t1;
                    c2 += t2;
                    //actualizamos corrida anterior
                    t1 = -(c1 - t1) / c;
                    t2 = -(c2 - t2) / c;
                    x = y;
                    y = r;
                }
                //residuo anterior es 1 , son primos relativos y el inverso existe
                if (x == 1) { res = true; }
            }
            else { res = false;}
            return res;
        }
        public int AskValInverso(int a, int m)
        {
            //valor de retorno
            int res = 0;
            if (a != 0)
            {
                int c1 = 1;
                int c2 = -(m / a); //coeficiente de a y b respectivamente
                int t1 = 0;
                int t2 = 1; //coeficientes penultima corrida
                int r = m % a; //residuo, asignamos 1 como condicion de entrada 
                int x = a, y = r, c;
                while (r != 0)
                {
                    c = x / y;//cociente
                    r = x % y;//residuo
                              //guardamos valores temporales de los coeficientes
                              //multiplicamos los coeficiente por -1*cociente de la division
                    c1 *= -c;
                    c2 *= -c;
                    //sumamos la corrida anterior
                    c1 += t1;
                    c2 += t2;
                    //actualizamos corrida anterior
                    t1 = -(c1 - t1) / c;
                    t2 = -(c2 - t2) / c;
                    x = y;
                    y = r;
                }
                //residuo anterior es 1 , son primos relativos y el inverso existe
                if (x == 1) { if (t2 < 0) { t2 = t2 + m; } res = t2; }
            }
            return res;
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {          
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                BigInteger n = BigInteger.Parse(textBox2.Text);
                BigInteger z = BigInteger.Parse(textBox3.Text);
                if (n != 0)
                {
                    if (n < z)
                    {
                        lblAlerta.Text = "";
                        lblAlerta.Visible = false;
                        BigInteger c1 = 1;
                        BigInteger c2 = -(z / n); //coeficiente de a y b respectivamente
                        BigInteger t1 = 0;
                        BigInteger t2 = 1; //coeficientes penultima corrida
                        BigInteger r = z % n; //residuo, asignamos 1 como condicion de entrada 
                        BigInteger x = n, y = r, c;
                        while (r != 0)
                        {
                            c = x / y;//cociente
                            r = x % y;//residuo
                            c1 *= -c;
                            c2 *= -c;
                            //sumamos la corrida anterior
                            c1 += t1;
                            c2 += t2;
                            //actualizamos corrida anterior
                            t1 = -(c1 - t1) / c;
                            t2 = -(c2 - t2) / c;
                            x = y;
                            y = r;
                        }
                        //residuo anterior es 1 , son primos relativos y el inverso existe
                        if (x == 1)
                        {
                            if (t2 < 0)
                            {
                                t2 = t2 + z;
                            }
                            dataGridView2.Rows.Add(n, t2);
                        }
                        else
                        {
                            dataGridView2.Rows.Add(n, "Sin inverso");
                        }
                    }else
                    {
                        lblAlerta.Visible = true;
                        lblAlerta.Text = "El número debe ser menor a Z";
                    }
                }
            }
        }
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" || textBox3.Text != "")
            {
                textBox2.Clear();
                textBox3.Clear();
                lblAlerta.Text = "";
                lblAlerta.Visible = false;
            }   
        }
        // función para evitar ordenamiento de columnas en un datagridview    
        public void NotOrder(DataGridView dg)
        {
            foreach (DataGridViewColumn column in dg.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            NotOrder(dataGridView2);
            lblAlerta.Visible = false;
        }
        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
            }
        }
    }
}
