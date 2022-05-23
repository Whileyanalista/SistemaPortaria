using System;
using System.Data.SQLite;
using System.Data;
using SistemaPortaria.GetSet;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace SistemaPortaria.dao
{
    public class CadCrud : ClassConexao
    {    

        //public byte[] byteImagealter = null;

        public string acessaradm;

        public string cadlogar;

        public string rgpessoa;

        public bool telacad;

        public string cadAlterar;        

        //SQLiteConnection Conexion;
        SQLiteCommand command = null;
        //_______________________LOGAR______________________________________

        public void logar(Acesso acesso, Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select * from acesso a inner join pessoa b on b.fk_acesso = a.id where login = @login and senha = @senha", conexao);

                command.Parameters.AddWithValue("@login", acesso.login);
                command.Parameters.AddWithValue("@senha", acesso.senha);

                SQLiteDataReader lgUser;

                lgUser = command.ExecuteReader();

                if (lgUser.Read())
                {
                    string idAcesso = lgUser.GetString(6);     

                    string perfil = lgUser.GetString(3);

                    if (perfil == "ADMIN")
                    {
                        acessaradm = "SIM";
                    }
                    else
                    {
                        acessaradm = "NÃO";
                    }                   
                }
                else
                {
                    acessaradm = "";
                }
                lgUser.Close();

            }
            catch (SQLiteException erro)
            {                
                throw erro;
               
            }
            finally
            {
                FecharConexao();
            }
        }

        public void CadLogar(Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select nome, perfil from acesso a inner join pessoa b on a.id = b.fk_acesso where rg = @rg", conexao);
                                
                command.Parameters.AddWithValue("@rg", pessoa.rg);

                SQLiteDataReader lgUser;

                lgUser = command.ExecuteReader();                             

                if (lgUser.Read())
                {
                    cadlogar = lgUser.GetString(1);

                    if (cadlogar == "USUARIO")
                    {
                        cadlogar = "USUARIO";
                    }
                    else if (cadlogar == "ADMIN")
                    {
                        cadlogar = "ADMIN";                        
                    }                     
                }
                else
                {
                    cadlogar = "";
                }
                
                lgUser.Close();

            }
            catch (SQLiteException)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public void CadLogaraDM(Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select nome from acesso a inner join pessoa b on a.id = b.fk_acesso where perfil = 'ADMIN' and b.ativo = 'ATIVO'", conexao);
                
                SQLiteDataReader lgUser;

                lgUser = command.ExecuteReader();

                if (lgUser.Read())
                {
                    string nome = lgUser.GetString(0);
                    pessoa.nome = nome;
                }
                else
                {
                    cadlogar = "Cadastroadm";
                }               

                lgUser.Close();

            }
            catch (SQLiteException)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }
        //________________________INSERT______________________________________   


        public void salvarPessoa(Pessoa pessoa, Apartamento apartamento, Veiculo veiculo, Acesso acesso)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select * from pessoa where rg = @rg or nome = @nome", conexao);

                command.Parameters.AddWithValue("@nome", pessoa.nome);
                command.Parameters.AddWithValue("@rg", pessoa.rg);                

                SQLiteDataReader verificarpessoa;

                verificarpessoa = command.ExecuteReader();
                
                if (verificarpessoa.Read())
                {
                    string rgpessoa = verificarpessoa.GetString(2);
                    int idpessoa = verificarpessoa.GetInt32(0);

                    byte[] img_byte = null;

                    pessoa.id = idpessoa;                   
                                       
                    if (pessoa.foto == "System.Byte[]")
                    {
                        command = new SQLiteCommand("update pessoa set nome = @nome, rg = @rg, tipo = @tipo, ativo = @ativo, cel = @cel, email = @email  ," +
                            "fk_apartamento = (select id from apartamento where ramal = @ramal), " +
                            "fk_veiculo = (select id FROM veiculo where placa = @placa), " +
                            "fk_acesso = (select CASE WHEN @tipo =='COLABORADOR' THEN id  ELSE '' END AS id from acesso where login = @login or senha = @senha)  " +
                            "where  id = (select id from pessoa where rg = @rg or nome = @nome)", conexao);
                    }
                    else
                    {
                        command = new SQLiteCommand("update pessoa set nome = @nome, rg = @rg, tipo = @tipo, foto = @foto, ativo = @ativo, cel = @cel, email = @email ," +
                            "fk_apartamento = (select id from apartamento where ramal = @ramal), fk_veiculo = (select id FROM veiculo where placa = @placa), " +
                            "fk_acesso = (select CASE WHEN @tipo =='COLABORADOR' THEN ID  ELSE '' END AS FkAcesso from acesso where login = @login or senha = @senha ) " +
                            "where id = (select id from pessoa where rg = @rg or nome = @nome)", conexao);

                        if (pessoa.foto != "")
                        {
                            FileStream fileStream = new FileStream(pessoa.foto, FileMode.Open, FileAccess.Read);

                            BinaryReader br = new BinaryReader(fileStream);

                            img_byte = br.ReadBytes((int)fileStream.Length);

                            command.Parameters.AddWithValue("@foto", img_byte);
                        }
                    }  
                    
                    command.Parameters.AddWithValue("@nome", pessoa.nome);
                    command.Parameters.AddWithValue("@rg", pessoa.rg);
                    command.Parameters.AddWithValue("@tipo", pessoa.tipo);
                    command.Parameters.AddWithValue("@foto", img_byte);
                    command.Parameters.AddWithValue("@ativo", pessoa.ativo);
                    command.Parameters.AddWithValue("@cel", pessoa.cel);
                    command.Parameters.AddWithValue("@email", pessoa.email);
                    command.Parameters.AddWithValue("@ramal", apartamento.ramal);
                    command.Parameters.AddWithValue("@placa", veiculo.placa);
                    command.Parameters.AddWithValue("@login", acesso.login);
                    command.Parameters.AddWithValue("@senha", acesso.senha);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Dados alterados com sucesso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {                   

                    command = new SQLiteCommand("insert into pessoa values (null ,@nome, @rg, @tipo, @foto, @ativo, @cel, @email, " +
                        "(select id from apartamento where ramal = @ramal)," +
                        "(select id from veiculo where placa = @placa)," +
                        "(select CASE WHEN @tipo == 'COLABORADOR' THEN id  ELSE '' END AS id from acesso where login = @login and senha = @senha))", conexao);

                    byte[] img_byte = null;

                    if (pessoa.foto != "System.Byte[]")
                    {
                        FileStream fileStream = new FileStream(pessoa.foto, FileMode.Open, FileAccess.Read);

                        BinaryReader br = new BinaryReader(fileStream);

                        img_byte = br.ReadBytes((int)fileStream.Length);
                    }

                    command.Parameters.AddWithValue("@id", pessoa.id);
                    command.Parameters.AddWithValue("@nome", pessoa.nome);
                    command.Parameters.AddWithValue("@rg", pessoa.rg);
                    command.Parameters.AddWithValue("@tipo", pessoa.tipo);
                    command.Parameters.AddWithValue("@foto", img_byte);
                    command.Parameters.AddWithValue("@ativo", pessoa.ativo);
                    command.Parameters.AddWithValue("@cel", pessoa.cel);
                    command.Parameters.AddWithValue("@email", pessoa.email);
                    command.Parameters.AddWithValue("@ramal", apartamento.ramal);
                    command.Parameters.AddWithValue("@placa", veiculo.placa);
                    command.Parameters.AddWithValue("@login", acesso.login);
                    command.Parameters.AddWithValue("@senha", acesso.senha);


                    command.ExecuteNonQuery();

                    MessageBox.Show("Dados Salvos com sucesso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                verificarpessoa.Close();
            }
            catch (Exception)
            {
                throw ;
            }
            finally
            {
                FecharConexao();
            }
        }

        public void inout(Pessoa pessoa, InOut inout)
        {
            try
            {
                    AbrirConexao();

                    command = new SQLiteCommand("select * from inout where fk_pessoa = (select id from pessoa where rg = @rg or nome = @nome) and saida = 'N/L' ORDER BY entrada DESC LIMIT 1", conexao);

                    SQLiteDataReader inOut;

                    inOut = command.ExecuteReader();
                
                    if (inOut.Read())
                    {
                        string observacao = "Registrado de acesso do visitante " + pessoa.nome + " entrando as " + inout.entrada + " e saindo as " + inout.saida + ".";

                        command = new SQLiteCommand("update inout set saida = @saida, observacao = @observacao WHERE fk_pessoa = (select id from pessoa where rg = @rg or nome = @nome) and saida = 'N/L'", conexao);

                        command.Parameters.AddWithValue("@saida", inout.saida);
                        command.Parameters.AddWithValue("@observacao", observacao);

                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        string observacao = "Registrado de acesso do visitante " + pessoa.nome + " entrando as " + inout.entrada;

                        command = new SQLiteCommand("insert into inout values(NULL,@entrada, @saida, (select id from pessoa where rg = @rg or nome = @nome), " +
                            "@fk_apartamento = (select fk_apartamento from pessoa where rg = @rg or nome = @nome), @observacao)", conexao);

                        command.Parameters.AddWithValue("@entrada", inout.entrada);
                        command.Parameters.AddWithValue("@saida", "N/L");
                        command.Parameters.AddWithValue("@observacao", observacao);

                        command.ExecuteNonQuery();
                    }
                    
                    inOut.Close();                
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

        public void salvarAcesso(Acesso acesso,Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select a.id,nome, perfil from acesso a inner join pessoa b on a.id = b.fk_acesso where b.rg = @rg", conexao);

                command.Parameters.AddWithValue("@login", acesso.login);
                command.Parameters.AddWithValue("@senha", acesso.senha);
                command.Parameters.AddWithValue("@rg", pessoa.rg);



                SQLiteDataReader verificaracesso;

                    verificaracesso = command.ExecuteReader();

                if (verificaracesso.Read())
                {
                    int idacesso = verificaracesso.GetInt32(0);

                    command = new SQLiteCommand("update acesso set login = @login, senha = @senha, perfil = @perfil where id = (select fk_acesso from pessoa where rg = @rg)", conexao);

                    acesso.id = idacesso;

                    command.Parameters.AddWithValue("@id", acesso.id);
                    command.Parameters.AddWithValue("@login",acesso.login);
                    command.Parameters.AddWithValue("@senha",acesso.senha);
                    command.Parameters.AddWithValue("@perfil",acesso.perfil);
                    command.Parameters.AddWithValue("@rg", pessoa.rg);


                    command.ExecuteNonQuery();
                }
                else 
                {
                    command = new SQLiteCommand("insert into acesso values ( null, @login, @senha, @perfil)", conexao);
                    
                    command.Parameters.AddWithValue("@login", acesso.login);
                    command.Parameters.AddWithValue("@senha", acesso.senha);
                    command.Parameters.AddWithValue("@perfil", acesso.perfil);

                    command.ExecuteNonQuery();
                }
                verificaracesso.Close();
            }
            catch (SQLiteException Erro )
            {
                throw Erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        public void salvarVeiculo(Veiculo veiculo,Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select * from veiculo where placa = @placa", conexao);

                command.Parameters.AddWithValue("@placa", veiculo.placa);

                SQLiteDataReader verificarveiculo;

                verificarveiculo = command.ExecuteReader();

                if (verificarveiculo.Read())
                {

                    command = new SQLiteCommand("update veiculo set placa = @placa, modelo = @modelo, cor = @cor where id = @id", conexao);

                    int idveiculo = verificarveiculo.GetInt32(0);

                    veiculo.id = idveiculo;

                    pessoa.fk_veiculo = idveiculo;

                    command.Parameters.AddWithValue("@placa", veiculo.placa);
                    command.Parameters.AddWithValue("@modelo", veiculo.modelo);
                    command.Parameters.AddWithValue("@cor", veiculo.cor);
                    command.Parameters.AddWithValue("@id", veiculo.id);


                    command.ExecuteNonQuery();
                }
                else if(veiculo.placa != "")
                {
                    command = new SQLiteCommand("insert into veiculo values (null,@placa, @modelo, @cor)", conexao);

                    command.Parameters.AddWithValue("@placa", veiculo.placa);
                    command.Parameters.AddWithValue("@modelo", veiculo.modelo);
                    command.Parameters.AddWithValue("@cor", veiculo.cor);

                    command.ExecuteNonQuery();

                    command = new SQLiteCommand("select * from veiculo where placa = @placa", conexao);

                    command.Parameters.AddWithValue("@placa", veiculo.placa);

                    SQLiteDataReader veiculofk;

                    veiculofk = command.ExecuteReader();

                    if (veiculofk.Read())
                    {
                        int valorveiculofk = veiculofk.GetInt32(0);

                        pessoa.fk_veiculo = valorveiculofk;
                    }

                    veiculofk.Close();
                }
                verificarveiculo.Close();
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

        public void salvarApartamento(Apartamento apartamento, Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select * from apartamento where bloco = @bloco and numero = @numero ",conexao);

                
                command.Parameters.AddWithValue("@numero",apartamento.numero);
                command.Parameters.AddWithValue("@bloco", apartamento.bloco);

                SQLiteDataReader verificarapartamento;

                verificarapartamento = command.ExecuteReader();

                if (verificarapartamento.Read())
                {
                    command = new SQLiteCommand("update apartamento set numero = @numero,  bloco = @bloco, andar = @andar, ramal = @ramal where id = @id", conexao);

                    int idapartamento = verificarapartamento.GetInt32(0);
                   
                    apartamento.id = idapartamento;

                    pessoa.fk_apartamento = idapartamento;

                    command.Parameters.AddWithValue("@numero", apartamento.numero);
                    command.Parameters.AddWithValue("@bloco", apartamento.bloco);
                    command.Parameters.AddWithValue("@andar", apartamento.andar);
                    command.Parameters.AddWithValue("@ramal", apartamento.ramal);
                    command.Parameters.AddWithValue("@id", apartamento.id);

                    command.ExecuteNonQuery();
                }
                else
                {                  

                    command = new SQLiteCommand("insert into apartamento values(null, @numero,  @bloco, @andar, @ramal)", conexao);

                    command.Parameters.AddWithValue("@numero", apartamento.numero);
                    command.Parameters.AddWithValue("@bloco", apartamento.bloco);
                    command.Parameters.AddWithValue("@andar", apartamento.andar);
                    command.Parameters.AddWithValue("@ramal", apartamento.ramal); 

                    command.ExecuteNonQuery();

                    command = new SQLiteCommand("select * from apartamento where bloco = @bloco and numero = @numero ", conexao);

                    command.Parameters.AddWithValue("@numero", apartamento.numero);
                    command.Parameters.AddWithValue("@bloco", apartamento.bloco);

                    SQLiteDataReader verificarapartamentofk;

                    verificarapartamentofk = command.ExecuteReader();                    

                    if (verificarapartamentofk.Read())
                    {
                        int tapartamentofk = verificarapartamentofk.GetInt32(0);

                        pessoa.fk_apartamento = tapartamentofk;
                    }
                    verificarapartamentofk.Close();
                }
                verificarapartamento.Close();
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

        public DataTable listaCadastro()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                command = new SQLiteCommand("SELECT * FROM CADASTRO_VIEW;", conexao);

                sQLiteDataAdapter.SelectCommand = command;

                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        public DataTable listaCadastro(Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                command = new SQLiteCommand("SELECT * FROM CADASTRO_VIEW where TIPO = @tipo", conexao);

                command.Parameters.AddWithValue("@tipo",pessoa.tipo);

                sQLiteDataAdapter.SelectCommand = command;

                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        
        //_______________________DELETE____________________________________________//             

        public void deletarPessoa(Pessoa pessoa, Acesso acesso)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select * from pessoa a LEFT JOIN acesso d on d.id = a.fk_acesso where a.rg = @rg", conexao);

                command.Parameters.AddWithValue("@rg", pessoa.rg);

                SQLiteDataReader deletPessoa;

                deletPessoa = command.ExecuteReader();

                if (deletPessoa.Read())
                {                

                    if (DialogResult.Yes == MessageBox.Show("Gostaria de DELETAR estes dados?", "confirma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        int idpessoa = deletPessoa.GetInt32(0);
                        
                        command = new SQLiteCommand("delete from pessoa where id = (select id from pessoa where rg = @rg);" + "delete from acesso where id = (select fk_acesso from pessoa where rg = @rg);", conexao);

                        command.Parameters.AddWithValue("@rg", pessoa.rg);


                        command.ExecuteNonQuery();
                    } 
                }
                deletPessoa.Close();
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
        //_____________________PESQUISA TELA PRINCIPAL_____________________________//
        public string placaget;

        public string ramalget;

        public void pesquisaPessoa(Pessoa pessoa, Apartamento apartamento, Veiculo veiculo)
        {
            try
            {
                AbrirConexao();

                command = new SQLiteCommand("select NOME,RG,FOTO,AP,BLOCO,ANDAR,RAMAL,PLACA,MODELO,COR,TIPO,CELULAR from CADASTRO_VIEW where placa = @placa or ramal = @ramal", conexao);

                command.Parameters.AddWithValue("@placa", placaget);
                command.Parameters.AddWithValue("@ramal", ramalget);
                
                SQLiteDataReader pesquisaPlaca;

                pesquisaPlaca = command.ExecuteReader();                

                if (pesquisaPlaca.Read())
                {
                   
                    byte[] img_byte = null;
                  
                    command = new SQLiteCommand("select NOME,RG,FOTO,AP,BLOCO,ANDAR,RAMAL,PLACA,MODELO,COR,TIPO,CELULAR from CADASTRO_VIEW where placa = @placa AND FOTO IS NOT NULL or ramal = @ramal AND FOTO IS NOT NULL", conexao);

                    command.Parameters.AddWithValue("@placa", placaget);
                    command.Parameters.AddWithValue("@ramal", ramalget);

                    SQLiteDataReader pesquisafoto;

                    pesquisafoto = command.ExecuteReader();

                    if (pesquisafoto.Read())
                    {
                       img_byte = (byte[])pesquisaPlaca["FOTO"];

                       MemoryStream memoryStream = new MemoryStream(img_byte);

                       pessoa.Img_fot = Image.FromStream(memoryStream);
                        
                    }                    
                    else
                    {
                        
                    }
                    pesquisafoto.Close();
                    pessoa.tipo = pesquisaPlaca.GetString(10);

                    if (pessoa.tipo =="COLABORADOR")
                    {
                        pessoa.nome = pesquisaPlaca.GetString(0);
                        pessoa.rg = pesquisaPlaca.GetString(1);
                        pessoa.tipo = pesquisaPlaca.GetString(10);

                        veiculo.placa = pesquisaPlaca.GetString(7);
                        veiculo.modelo = pesquisaPlaca.GetString(8);
                        veiculo.cor = pesquisaPlaca.GetString(9);                       

                    }
                    else
                    {
                        pessoa.nome = pesquisaPlaca.GetString(0);
                        pessoa.rg = pesquisaPlaca.GetString(1);
                        pessoa.tipo = pesquisaPlaca.GetString(10);
                        pessoa.cel = pesquisaPlaca.GetString(11);                    

                        apartamento.numero = pesquisaPlaca.GetString(3);
                        apartamento.bloco = pesquisaPlaca.GetString(4);
                        apartamento.andar = pesquisaPlaca.GetString(5);
                        apartamento.ramal = pesquisaPlaca.GetString(6);


                        veiculo.modelo = ""; //pesquisaPlaca.GetString(8);
                        veiculo.cor = "";//pesquisaPlaca.GetString(9);
                        veiculo.placa = "";//pesquisaPlaca.GetString(7);

                    }                    
                    pesquisaPlaca.Close();
                }
                pesquisaPlaca.Close();
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

        public DataTable pesquisaVisitaEmAP(Apartamento apartamento)
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("select * from CADASTRO_VIEW where tipo = 'VISITANTE' AND STATUS = 'ATIVO' and ramal like @ramal", conexao);
                command.Parameters.AddWithValue("@ramal", "%" + apartamento.ramal + "%");
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {

                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
            
        public DataTable pesquisaEntradaVisitante(Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("select * from CADASTRO_VIEW where tipo = 'VISITANTE' AND rg like @rg", conexao);
                command.Parameters.AddWithValue("@rg", "%" + pessoa.rg + "%");
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {

                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        
        public DataTable pesquisacad(Pessoa pessoa)
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                if (pessoa.tipo != "")
                {
                    command = new SQLiteCommand("SELECT * FROM CADASTRO_VIEW where TIPO = @tipo and NOME like @nome;", conexao);
                }
                else
                {
                    command = new SQLiteCommand("SELECT * FROM CADASTRO_VIEW where NOME like @nome;", conexao);
                }               
                command.Parameters.AddWithValue("@nome", "" + pessoa.nome + "%");
                command.Parameters.AddWithValue("@tipo", pessoa.tipo);
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        public DataTable listaInout()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                command = new SQLiteCommand("SELECT * FROM INOUT_VIEW;", conexao);

                sQLiteDataAdapter.SelectCommand = command;

                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
        
        public DataTable pesquisaInOut(InOut inout)
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO               
                command = new SQLiteCommand("SELECT * from INOUT_VIEW WHERE ENTRADA BETWEEN @saida AND @entrada;", conexao);

                command.Parameters.AddWithValue("@entrada", "" + inout.entrada + "%");
                command.Parameters.AddWithValue("@saida", "" + inout.saida + "%");
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        public DataTable listaVisitante()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                command = new SQLiteCommand("SELECT * FROM CADASTRO_VIEW where tipo = 'VISITANTE' AND STATUS = 'ATIVO'", conexao);

                sQLiteDataAdapter.SelectCommand = command;

                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }
       
        //ALTOCOMPLETE
        public DataTable completarApartamento()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("select * from NUMERO_VIEW", conexao);
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {

                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        //ALTOCOMPLETE
        public DataTable completarAndar()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("select * from ANDAR_VIEW", conexao);
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {

                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        //ALTOCOMPLETE
        public DataTable completarCor()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("SELECT * FROM COR_VIEW", conexao);
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        //ALTOCOMPLETE
        public DataTable completarModelo()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("SELECT * FROM MODELO_VIEW", conexao);
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        //ALTOCOMPLETE
        public DataTable completarPlaca()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("SELECT * FROM PLACA_VIEW", conexao);
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {
                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        //ALTOCOMPLETE
        public DataTable completarRG()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("SELECT * FROM RG_VIEW", conexao);
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {

                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        //ALTOCOMPLETE
        public DataTable completarRamal()
        {
            try
            {
                AbrirConexao();

                DataTable dataTable = new DataTable();
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();

                //PARAMETROS QUE POSSIBILITA O SELECT COM LIKE NO BANCO
                command = new SQLiteCommand("SELECT * FROM RAMAL_VIEW", conexao);
                sQLiteDataAdapter = new SQLiteDataAdapter(command);
                dataTable = new DataTable();
                sQLiteDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (SQLiteException erro)
            {

                throw erro;
            }
            finally
            {
                FecharConexao();
            }
        }

        ///__________________REGISTRO DE VISITANTE_______________________________//


    }
}
