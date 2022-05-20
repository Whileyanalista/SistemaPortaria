using System;
using System.IO;
using SistemaPortaria.GetSet;
using SistemaPortaria.dao;
using System.Windows.Forms;
using SistemaPortaria.bill;

namespace SistemaPortaria
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Exceptions exceptions = new Exceptions();
            Register register = new Register();
            Ativacao ativacao = new Ativacao();
            Bill bill = new Bill();

            //CODIGO PARA CRIAR UMA PASTA NO WIN CAMADO "Systema_Portaria" PARA A BASE DE DADOS DO SISTEMA 
            if (Directory.Exists("c:\\Systema") == false)
            {
                //CRIAR PASTA DO DIREITORIO C:                
                try
                {
                    Directory.CreateDirectory("C:\\Systema");
                }
                catch (Exception erro)
                {
                    exceptions.ExceptionsDB(1000, erro.ToString(), "Criar diretorio");
                    
                }
            }
            if (File.Exists("C:\\Systema\\Systema.s3db") == false)
            {
                try
                {
                    new ClassConexao().CrearBaseDados();
                }
                catch (Exception erro)
                {
                    exceptions.ExceptionsDB(1001, erro.ToString(), "Criar base de dados");
                }                
            }
            if (File.Exists("C:\\Systema\\Systema.s3db") == true)
            {
                try
                {
                    Application.EnableVisualStyles();

                    Application.SetCompatibleTextRenderingDefault(false);

                    ativacao.retornoEmail(register);

                    if (ativacao.valideAtivado == true)
                    {
                        Application.Run(new FormLogin()); 
                    }
                    else
                    {
                        Application.Run(new FormValidacao());
                    }
                }
                catch (Exception erro)
                {
                    exceptions.ExceptionsDB(1001, erro.ToString(), "Criar base de dados");
                }                
            }            
        }       
    }
}
