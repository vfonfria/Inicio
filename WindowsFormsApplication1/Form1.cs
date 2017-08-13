using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {


        #region Cargando
        public event EventHandler<CargandoEventArgs> CargandoPrimeros;
        protected virtual void OnCargando(CargandoEventArgs arg)
        {
            if (CargandoPrimeros != null)
                CargandoPrimeros(this, arg);

        }

        #endregion


        #region CargaCompleta
        public event EventHandler<CargaCompletaEventArgs> CargaCompleta;
        protected virtual void OnCargaCompleta(CargaCompletaEventArgs arg)
        {
            if (CargaCompleta != null)
                CargaCompleta(this, arg);

        }

        #endregion




        public Form1()
        {
            InitializeComponent();

            Connect();

            CargandoPrimeros += Form1_CargandoPrimeros;
            CargaCompleta += Form1_CargaCompleta;
            listBox1.DisplayMember = "Subject";
            listBox1.SelectedValue = "MessageId";
        }

        private void Form1_CargaCompleta(object sender, CargaCompletaEventArgs e)
        {
            CargaList(e);
        }

        private void CargaList(CargaCompletaEventArgs e)
        {
            
            listBox1.DataSource = e.Headers;
        }

        private void CargaList(CargandoEventArgs e)
        {
            List<OpenPop.Mime.Header.MessageHeader> reto = new List<OpenPop.Mime.Header.MessageHeader>();
            reto = e.Headers;
            listBox1.DisplayMember = "Subject";
            listBox1.SelectedValue = "MessageId";
            listBox1.DataSource = reto;
        }
        private void Form1_CargandoPrimeros(object sender, CargandoEventArgs e)
        {
            CargaList(e);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            SmtpClient c = new SmtpClient("smtp.gmail.com", 587);
            MailAddress add = new MailAddress("victorfonfria@gmail.com");
            MailMessage msg = new MailMessage();
            msg.To.Add(add);
            msg.From = new MailAddress("victorfonfria@gmail.com");
            msg.IsBodyHtml = true;
            msg.Subject = "Test Mail";
            msg.Body = "Prueba de mail smtp";
            c.Credentials = new System.Net.NetworkCredential("victorfonfria@gmail.com", "vittorino");
            c.EnableSsl = true;
            c.Send(msg);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int cant= Cliente.GetMessageCount();
            string messageId = ((OpenPop.Mime.Header.MessageHeader)listBox1.SelectedItem).MessageId;
            int id =1;
            for (int messageItem = cant; messageItem > 0; messageItem--)
            {
                // If the Message ID of the current message is the same as the parameter given, delete that message
                if (Cliente.GetMessageHeaders(messageItem).MessageId == messageId)
                {
                    // Delete
                    Cliente.DeleteMessage(messageItem);
                    
                }
            }
            //Cliente.DeleteMessage(id);
        }

        private void btnRecibir_Click(object sender, EventArgs e)
        {
            int messageCount = Cliente.GetMessageCount();

            List<OpenPop.Mime.Message> allMessages = new List<OpenPop.Mime.Message>(messageCount);
            List<OpenPop.Mime.Header.MessageHeader> allHeaders = new List<OpenPop.Mime.Header.MessageHeader>();

            List<string> uids = Cliente.GetMessageUids();
            StringBuilder sb = new StringBuilder();
            for (int i = messageCount; i > 0; i--)
            {

                //allHeaders.Add(Cliente.GetMessage(i).Headers);

                //if (messageCount-i==5)
                //{

                //    OnCargando(new CargandoEventArgs() { Headers = allHeaders });
                //}


                sb.AppendLine(string.Format("{0}\n", Cliente.GetMessageHeaders(i).MessageId));
                sb.AppendLine(string.Format("{0}\n", Cliente.GetMessage(i).Headers.From.Address));
                sb.AppendLine(string.Format("{0}\n", Cliente.GetMessage(i).Headers.To[0].Address));
                sb.AppendLine(string.Format("{0}\n", Cliente.GetMessage(i).Headers.Subject));
                sb.AppendLine(string.Format("{0}\n", Cliente.GetMessage(i).Headers.Date));
                sb.AppendLine(string.Format("{0}\n", Cliente.GetMessage(i).Headers.DateSent));
                sb.AppendLine();
                Console.WriteLine(sb.ToString());

                if (messageCount - i == 10)
                {
                    i = 0;
                }
                //OnCargaCompleta(new CargaCompletaEventArgs() { Headers = allHeaders });


                //textBox1.Text = sb.ToString();
            }
        }

        OpenPop.Pop3.Pop3Client _Cliente;
        public OpenPop.Pop3.Pop3Client Cliente {
            get
            {
                if (_Cliente==null)
                {
                    _Cliente = new OpenPop.Pop3.Pop3Client();
                }
                return _Cliente;

            }
            set { _Cliente = value; }
        }

        private void Connect()
        {

            //Gmail
            Cliente.Connect("pop.gmail.com", 995, true);
            Cliente.Authenticate("victorfonfria@gmail.com", "vittorino");

            //Hotmail
            //Cliente.Connect("pop-mail.outlook.com", 995, true);
            //Cliente.Authenticate("victorfonfria@hotmail.com", "Tomate01");
        }
    }
}
