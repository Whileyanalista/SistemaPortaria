using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaPortaria.dao
{
    public class Exceptions
    {
        public void ExceptionsDB(int erro, string ex, string exceptionEB)
        {
            try
            {
                string CaminhoNome = @"C:\\Systema\\log-systemportaria.txt";
                if (!File.Exists(CaminhoNome))
                    File.Create(CaminhoNome).Close();

                File.AppendAllText(CaminhoNome, "\r\nErro:" + exceptionEB + " o tipo " + ex + " Data de Procesamento(" + DateTime.Now.ToString("dd/MM/yyy HH:mm:ss")  + ")\r\n");

                switch (erro)
                {

                    case 1000:
                        MessageBox.Show("Erro 1000: Não foi possível criar o diretório :\r\n \r\nRETORNO: " + exceptionEB, "Alerta ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 10001:
                        MessageBox.Show("Erro 1001: Não foi possível criar a base de dados :\r\n \r\nRETORNO: " + exceptionEB, "Alerta ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 10002:
                        MessageBox.Show("Erro 1002: não foi possível abrir a transação :\r\n \r\nRETORNO: "+ exceptionEB, "Alerta ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1003:
                        MessageBox.Show("Erro 1003: não foi possível fechar a transação :\r\n \r\nRETORNO: " + exceptionEB, "Alerta ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1004:
                        MessageBox.Show("Erro 1004: Erro com realização da transação/consulta com a base de dados :\r\n \r\nRETORNO: " + exceptionEB, "Alerta ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1005:
                        MessageBox.Show("Erro 1005: Favor verificar se e-mail está correto! \r\n \r\nRETORNO: "+ exceptionEB, "Alerta ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1006:
                        MessageBox.Show("Erro 1006: Tentativa de leitura ou gravação em memória protegida. Normalmente, isso é uma indicação de que outra memória está danificada.\r\n \r\nRETORNO: "+ exceptionEB, "Alerta ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Erro ao completar a ação. Este erro não foi previsto :\r\n \r\nRETORNO: " + exceptionEB, "Alerta ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}

