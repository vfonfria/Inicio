using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private List<string> ObtenerArchivos()
        {
            var archivos = from archivo
            in
            System.IO.Directory.GetFiles(@"C:\Windows\System32")
                           select archivo;
            System.Threading.Thread.Sleep(5000);
            return archivos.ToList();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            ListaDeArchivos.DataSource = await ObtenerArchivosAsync();
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Son las : " +
DateTime.Now.ToLongTimeString());
        }

        private Task<List<String>> ObtenerArchivosAsync()
        {
            return Task.Run(() =>
            {
                System.Threading.Thread.Sleep(5000);
                var archivos = from archivo in
                System.IO.Directory.GetFiles(@"C:\Windows\System32")
                               select archivo;
                return archivos.ToList();
            });
        }
    }
}
