using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Import
using System.IO.Ports;

namespace Esp32ConnectComBluetooth
{
    public partial class Form1 : Form
    {
        // Get a list of serial port names.
        string[] ports = SerialPort.GetPortNames();

        // SerialPort utilized
        public SerialPort myport;

        // Bool logic conectar/desconectar
        public bool state = true;

        // Content item for the combo box
        private class Item
        {
            public string Com;
            public Item(string com)
            {
                Com = com;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Com;
            }
        }

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;

            // Add COMs in comboBox
            foreach (string port in ports)
            {
                comboBox1.Items.Add(new Item(port));
            }
        }

        // ComboBox Design
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display the Value property
            Item itm = (Item)comboBox1.SelectedItem;
            Console.WriteLine(itm.Com);
        }

        // Button Abrir conexão
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                button2.Enabled = true;

                rs232Port.PortName = comboBox1.Text;
                rs232Port.Open();
                label1.Text = "Conexão Ativa";
            }
            catch
            {
                if (rs232Port.IsOpen == false)
                {
                    MessageBox.Show("Porta não está aberta");
                    button1.Enabled = true;
                    button2.Enabled = false;
                }
            }
        }

        // Button Conectar/Desconectar
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (state == true)
                {
                    timer1.Enabled = true;
                    label1.Text = "Conectado";

                    button2.Text = "Desconectar";
                    state = false;
                }
                else if (state == false)
                {
                    rs232Port.Close();
                    timer1.Enabled = false;
                    label1.Text = "Desconectado";

                    button1.Enabled = true;
                    button2.Enabled = false;
                    button2.Text = "Conectar";
                    state = true;
                    
                }

            }
            catch
            {
                MessageBox.Show("Falha ao fechar a Porta");
                label1.Text = "Sem Conexão";
            }
        }

        // Timer read text in esp32
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = rs232Port.ReadExisting();
            //Text = textBox1.Text;

        }

        // Button On
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                rs232Port.WriteLine("O");
            }
            catch
            {
                MessageBox.Show("Com não selecionada");
            }

        }

        // Button Off
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                rs232Port.WriteLine("F");
            }
            catch
            {
                MessageBox.Show("Com não selecionada");
            }
        }
    }
}
