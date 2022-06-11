using SistemaPortaria.GetSet;
using System;
using System.Data.SQLite;
using System.Net.Mail;
using System.Windows.Forms;

namespace SistemaPortaria.dao
{
    public class Ativacao : ClassConexao
    {
        SQLiteCommand command = null;

        Exceptions exceptions = new Exceptions();

        public bool emailretorno;
        public bool valideAtivado = false;
        string emailsmtp = "";
        string nmlocalsmtp = "";
        string emailativacao = "";
        public bool acessoChave = false;
        public int enviaEmail = 0;
        

        public void retornoEmail(Register register)
        {
            try
            {
                AbrirConexao();
                
                command = new SQLiteCommand("select * from Register ORDER BY id_register DESC LIMIT 1", conexao);

                SQLiteDataReader emailCad;

                emailCad = command.ExecuteReader();               


                if (emailCad.Read())
                {
                    string validar = emailCad.GetString(1);
                    string Chave = emailCad.GetString(2);
                    string email = emailCad.GetString(3);                    
                    string dtinicio = emailCad.GetString(4);
                    string dtfim = emailCad.GetString(5);
                    string dtuso = emailCad.GetString(6);
                    string nmlocal = emailCad.GetString(7);   
                    

                    emailsmtp = email;
                    nmlocalsmtp = nmlocal;
                    register.email = email;

                    register.dt_uso = dtuso;
                    register.chaveID = Chave;
                    register.validarID = validar;
                    emailativacao = validar;

                    register.dt_inicio = dtinicio;
                    register.dt_fim = dtfim;
                    register.nm_local = nmlocal;

                    if (register.chaveID == register.validarID)
                    {
                        valideAtivado = true;                        
                    }                            
                    emailretorno = true;                   
                }
                else
                {
                    emailretorno = false;
                }
                emailCad.Close();                
            }
            catch (Exception erro)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }          
        }

        public void diaLisencaFim(Register register)
        {
            
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select * from Register ORDER BY id_register DESC LIMIT 1", conexao);
                
                SQLiteDataReader tempoAtiva;

                tempoAtiva = command.ExecuteReader();

                if (tempoAtiva.Read())
                {   
                    string dtinicio = tempoAtiva.GetString(4);
                    string dtfim = tempoAtiva.GetString(5);
                    string dtuso = tempoAtiva.GetString(6);
                    int time2 = tempoAtiva.GetInt32(8);
                    string nmlocal = tempoAtiva.GetString(7);


                    register.dt_inicio = dtinicio;
                    register.dt_fim = dtfim;
                    register.dt_uso = dtuso;
                    register.nm_local = nmlocal;

                    if (register.dt_fim != register.dt_uso)
                    {
                        command = new SQLiteCommand("update Register set time = time + 1", conexao);

                        command.ExecuteNonQuery();
                        tempoAtiva.Close();
                    }
                    if (time2 == 1440 || time2 > 1440)
                    {
                        command = new SQLiteCommand("update Register set dt_uso = date(dt_uso,'+1 days'), time = 1;", conexao);

                        command.ExecuteNonQuery();
                        tempoAtiva.Close();
                    }
                    else if (register.dt_fim == register.dt_uso)
                    {
                        command = new SQLiteCommand("delete from Register", conexao);                        
                        command.ExecuteNonQuery();
                        tempoAtiva.Close();
                    }

                    tempoAtiva.Close();
                }
                else
                {
                    MessageBox.Show("Ola,\r\nSinto informar!\r\n \r\nA licença do " 
                        + register.nm_local + " expirou.\r\n \r\nPara continuar a utilizar o SBGA (Sistema Básico de Gestão de Acesso), será necessário solicitar uma nova chave de ativação.\r\n \r\nObrigado pela compreensão!", "Alerta", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }               
            }
            catch (Exception)
            {
                    
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public void diaLisenca(Register register)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select * from Register ORDER BY id_register DESC LIMIT 1", conexao);

                SQLiteDataReader lisendei;

                lisendei = command.ExecuteReader();

                if (lisendei.Read())
                {
                    string nmCond = lisendei.GetString(7);
                    string dtinicio = lisendei.GetString(4);                   
                    string dtfim = lisendei.GetString(5);
                    string dtuso = lisendei.GetString(6);

                    register.dt_inicio = dtinicio;
                    register.dt_fim = dtfim;
                    register.dt_uso = dtuso;
                    register.nm_local = nmCond;
                }
                else
                {

                }
                lisendei.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }
        
        private void EnviarEmail(Register register, string Assunto, string CorpoEmail, string mensagem)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    using (MailMessage email = new MailMessage())
                    {

                        //client.Credentials = new NetworkCredential("<Usuario>", "<Senha>");
                        //client.UseDefaultCredentials = true;
                        //client.EnableSsl = true;

                        //Servidor de Email
                        smtp.Host = "smtp.gmail.com";
                        smtp.UseDefaultCredentials = false;                        
                        smtp.Credentials = new System.Net.NetworkCredential("systemagerenciaip@gmail.com", "whiley5604");
                        smtp.Port = 587;
                        smtp.EnableSsl = false;

                        //Email (Mensagem)
                        email.From = new MailAddress(register.email/*Remetente*/);
                        email.To.Add("beelinkxiii@gmail.com"/*Destinatario*/);

                        email.Subject = Assunto; //Assunto
                        email.IsBodyHtml = false;
                        email.Body = CorpoEmail; //Corpo do email
                        

                        //Enviar email
                        smtp.Send(email);

                        MessageBox.Show(mensagem, "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void salvarAtivacao(Register register)
        {
            try
            {

                AbrirConexao();                

                if (emailretorno == false && register.validarID != null)
                {
                    command = new SQLiteCommand("insert into Register values (null, @validarID, @chaveID, @email, @dt_inicio, @dt_fim, @dt_uso, @nm_local, @time, @state_cod)", conexao);

                    command.Parameters.AddWithValue("@validarID", register.validarID);
                    command.Parameters.AddWithValue("@chaveID", register.chaveID);
                    command.Parameters.AddWithValue("@email", register.email);
                    command.Parameters.AddWithValue("@dt_inicio", register.dt_inicio);
                    command.Parameters.AddWithValue("@dt_fim", register.dt_fim);
                    command.Parameters.AddWithValue("@dt_uso", register.dt_uso);
                    command.Parameters.AddWithValue("@nm_local", register.nm_local);
                    command.Parameters.AddWithValue("@time", 1);
                    command.Parameters.AddWithValue("@state_cod", register.state_cod);                    

                    command.ExecuteNonQuery();
                    try
                    {

                        EnviarEmail( 
                            register,                            
                            "Solicitação de ativação estabelecimento " + register.nm_local + "",
                            "Dados de solicitação de ativação do Sistema Básico de Gestão de Acesso " +
                            "\r\n \r\nEstabelecimento: " 
                            + register.nm_local + "\r\nE-mail: " 
                            + register.email + "\r\nSeria: " 
                            + register.validarID + "\r\n \r\nRegistro de periodo para uso..\r\nData de inicio: " 
                            + register.dt_inicio + "\r\nData de termino: " + register.dt_fim,
                            "Dados CADASTRADOS com sucesso "
                            ) ;

                      
                    }
                    catch (Exception erro)
                    {
                        exceptions.ExceptionsDB(1005, erro.ToString(), "Salvar Ativação");
                    }
                }
                else 
                {                  
                    command = new SQLiteCommand("update Register set chaveID = @chaveID, email = @email, dt_inicio = @dt_inicio, dt_fim = @dt_fim, dt_uso = @dt_uso, nm_local = @nm_local ,time = @time, state_cod = @state_cod", conexao);
                    
                    command.Parameters.AddWithValue("@chaveID", register.chaveID);
                    command.Parameters.AddWithValue("@email", register.email);
                    command.Parameters.AddWithValue("@dt_inicio", register.dt_inicio);
                    command.Parameters.AddWithValue("@dt_fim", register.dt_fim);
                    command.Parameters.AddWithValue("@dt_uso", register.dt_uso);
                    command.Parameters.AddWithValue("@nm_local", register.nm_local);
                    command.Parameters.AddWithValue("@time", 1);
                    command.Parameters.AddWithValue("@state_cod", register.state_cod);

                    command.ExecuteNonQuery();

                    try
                    {
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            using (MailMessage email = new MailMessage())
                            {                                
                                if (register.email != emailsmtp)
                                {

                                    EnviarEmail(
                                        register,
                                        "Alteração de dados estabelecimento " + nmlocalsmtp,
                                        "Alteração dos dados de solicitação de ativação do Sistema Básicode Gestão de Acesso \r\n \r\nEstabelecimento: "
                                        + register.nm_local + "\r\nDo E-mail: "
                                        + emailsmtp + "\r\nPara Email: "
                                        + register.email + "\r\nSeria: "
                                        + emailativacao + "\r\n \r\nREGISTRO DA ALTERAÇÃO.\r\nData de inicio: " + register.dt_inicio,
                                        "Seu e-mail foi alterado.\r\n\r\nDe e-mail: " + emailsmtp + "\r\nPara o e-mail: " + register.email
                                        );                                    
                                }
                                if (register.nm_local != nmlocalsmtp)
                                {
                                    EnviarEmail(
                                        register,
                                        "Alteração de dados estabelecimento " + nmlocalsmtp,
                                        "Alteração dos dados de solicitação de ativação do Sistema Básicode Gestão de Acesso \r\n\r\nDo Estabelecimento: " 
                                        + nmlocalsmtp + "\r\nPara Estabelecimento: " 
                                        + register.nm_local + "\r\nSeria: " 
                                        + emailativacao + "\r\n \r\nREGISTRO DA ALTERAÇÃO.\r\nData de inicio: " 
                                        + register.dt_inicio,
                                        "Seu estabelecimento foi alterado.\r\n\r\nDe Estabelecimento: " + nmlocalsmtp + "\r\nPara o Estabelecimento: " + register.nm_local
                                        );
                                   
                                }
                                else if (register.chaveID == emailativacao)
                                {
                                    EnviarEmail(
                                        register,
                                        "Confirmação de ativação de sistema estabelecimento " + nmlocalsmtp + "",
                                        "Obrigado por utilizar nosos Serviços  \r\n \r\nSeja bem vindo! \r\n \r\nSeu sistema foi ativado." 
                                        + "\r\n \r\nEstabelecimento: " 
                                        + register.nm_local + "\r\nPeriodo de uso.\r\n" + "Ate a data de: " 
                                        + register.dt_fim,
                                        "O sistema foi ativado"
                                        );                                    
                                }
                            }
                        }
                    }
                    catch (Exception erro)
                    {
                        exceptions.ExceptionsDB(1005, erro.ToString(), "Ativarcão");
                    }                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }
    }
}
