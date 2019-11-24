using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace veriOkuma
{
    public partial class Form1 : Form
    {
        string[] portlar = SerialPort.GetPortNames();
        public Form1()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.SelectedIndex = 1;
            label2.Text = "Bağlantı Kapalı";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string sonuc = serialPort1.ReadExisting();
                label1.Text = sonuc + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                timer1.Stop();

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            if (serialPort1.IsOpen == false)
            {
                if (comboBox1.Text == "")

                    return;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                try
                {
                    serialPort1.Open();
                    label2.Text = "Bağlantı Açık";
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata:" + hata.Message);

                }
            }
            else

            {
                label2.Text = "Bağlantı Kuruldu";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label2.Text = "Bağlantı Kapalı";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            
                serialPort1.Close();          
        }
    }
}

