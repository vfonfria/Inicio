using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Connect();
            MostrarMensajes();
        }

        private void MostrarMensajes()
        {
            //var lista = this.Cliente.GetAllMessages();
            //var lista = this.Cliente.GetAllUIDMessages();
            IAsyncResult reto = this.Cliente.BeginRecv(MostrarMails);
            //this.Cliente.EndRecv(reto);
           
         
        }

        public void MostrarMails(IAsyncResult result)
        {
            Email.Net.Pop3.Pop3Client colection = result.AsyncState as Email.Net.Pop3.Pop3Client;

            colection.EndRecv(result);
            //foreach (var item in colection)
            //{
            //    Console.WriteLine("{0}", item.From.DisplayName);
            //    Console.WriteLine("{0}", item.Date);
            //    Console.WriteLine("---------------------------");
            //}
        }

        public Email.Net.Pop3.Pop3Client Cliente { get; set; }
        private void Connect()
        {

            //Cliente.Connect("", 995, true);
            //Cliente.Authenticate("victorfonfria@hotmail.com", "Tomate01");
             Cliente = new Email.Net.Pop3.Pop3Client();
            Cliente.Host = "pop-mail.outlook.com";
            Cliente.Port = 995;
            Cliente.SSLInteractionType = Email.Net.Common.Configurations.EInteractionType.SSLPort;
            Cliente.Username = "victorfonfria@hotmail.com";
            Cliente.Password = "Tomate01";
            Cliente.Login();
        }
    }
}
