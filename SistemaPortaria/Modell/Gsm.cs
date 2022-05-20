using System;
using System.Text;
using System.Windows.Forms;
using SistemaPortaria.GetSet;
using System.IO;
using System.Net;

namespace SistemaPortaria.Modell
{
    class Gsm
    {
        Pessoa pessoa = new Pessoa();

        public void EnviarModem(Pessoa pessoa)
        {
            using (var port = new System.IO.Ports.SerialPort())
            {
                port.PortName = "COM5";
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;
                port.Write("AT\r"); // iniciando a comunicação
                port.Write("AT+CMGF=1\r"); // setando a comunicação para o modo texto
                port.Write(string.Format("AT+CMGS=\"{0}\"\r","tbTelefone.Text")); // setando o número do destinatário
                port.Write("tbMensagem.Text" + char.ConvertFromUtf32(26)); // enviando a mensagem
            }
        }       
        
        public void enviarMex(Pessoa pessoa)
        {         
            String result;
            string apiKey = "NmM2ZjdhNzkzNDc1NzU2NjZmNmI2ZTMxNzUzMTU3MzY=";
            string numbers = pessoa.cel; // in a comma seperated list
            string message = pessoa.msn;
            string sender = "whiley";

            String url = "https://api.txtlocal.com/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;
            //refer to parameters to complete correct url string

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
                MessageBox.Show(""+e);
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }
            MessageBox.Show(result);         

        }       
            
    }
}
