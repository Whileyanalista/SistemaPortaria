using System;
using System.Text;
using System.Data.SQLite;
using SistemaPortaria.dao;
using SistemaPortaria.Modell;
using System.Security.Cryptography;

namespace SistemaPortaria.GetSet
{
    public class ClassConexao
    {
        string key;

        public ClassConexao()
        {
            key = generatekey();
        }

        private string generatekey()
        {
            DESCryptoServiceProvider dESCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(dESCrypto.Key);
        }

        Exceptions exceptions = new Exceptions();

        CriptFile criptFile = new CriptFile();

        SQLiteCommand command = null;
       
        string conectar = "Data Source = C:\\Systema\\Systema.s3db";       

       

        protected SQLiteConnection conexao = null;
       
        //ESTABELECENDO UMA CONEXÃO COM O BANCO
        public void AbrirConexao()
        {
            try
            {
                conexao = new SQLiteConnection(conectar);
                conexao.Open();
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1002, erro.ToString(), "Abrir Conexão");
            }
        }

        //DESCONECTA A BASE DE DADOS
        public void FecharConexao()
        {
            try
            {
                conexao = new SQLiteConnection(conectar);

                conexao.Close();
            }
            catch (SQLiteException erro)
            {
                exceptions.ExceptionsDB(1003, erro.ToString(), "Fechar Conexão");
            }
        }       

        //CRIANDO TABELA COM BANCO SQLite   
        public void CrearBaseDados()
        {
            this.AbrirConexao();

            try
            {
            //CRAR TABELA 
            command = new SQLiteCommand ("CREATE TABLE [apartamento] (" +
                "[id] INTEGER NOT NULL PRIMARY KEY," +
                "[numero] VARCHAR (5) NOT NULL," +
                "[bloco] VARCHAR(20) NOT NULL," +
                "[andar] VARCHAR (5)NOT NULL," +
                "[ramal] VARCHAR(20) NOT NULL" +                 
                ");" +     
                    
                "CREATE TABLE[veiculo] (" +
                "[id] INTEGER NOT NULL PRIMARY KEY," +
                "[placa] VARCHAR(10) UNIQUE NOT NULL," +
                "[modelo] VARCHAR(20) NOT NULL," +
                "[cor] VARCHAR(20) NOT NULL" +                
                ");" +   

                "CREATE TABLE [Register] (" +
                "[id_register] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                "[validarID] VARCHAR(40) NOT NULL," +
                "[chaveID] VARCHAR(40)  NULL," +
                "[email] VARCHAR(30) NULL," +
                "[dt_inicio] date NULL," +
                "[dt_fim] date NOT NULL," +
                "[dt_uso] date NOT NULL," +
                "[nm_local] VARCHAR(50) NULL,"+
                "[time] INTEGER NULL," +
                "[state_cod] INTEGER NOT NULL" +
                ");"+

                "CREATE TABLE[pessoa] (" +
                "[id] INTEGER NOT NULL PRIMARY KEY," +
                "[nome] VARCHAR(40)  NOT NULL," +
                "[rg] VARCHAR(15) UNIQUE NOT NULL," +
                "[tipo] VARCHAR(40)  NOT NULL," +
                "[foto] BLOB NULL," +
                "[ativo] VARCHAR(10) NOT NULL," +
                "[cel] VARCHAR (15) NULL,"+
                "[email] VARCHAR (50) NULL," +
                "[fk_apartamento] INTEGER DEFAULT NULL," +
                "[fk_veiculo] INTEGER DEFAULT NULL," +
                "[fk_acesso] INTEGER DEFAULT NULL," +
                "FOREIGN KEY(fk_apartamento) REFERENCES apartamento(id)," +
                "FOREIGN KEY(fk_veiculo) REFERENCES veiculo(id)," +
                "FOREIGN KEY(fk_acesso) REFERENCES acesso(id)" +
                ");" +

                "CREATE TABLE[acesso] (" +
                "[id] INTEGER NOT NULL PRIMARY KEY," +
                "[login] VARCHAR(20)  NOT NULL," +
                "[senha] VARCHAR(20)  UNIQUE NOT NULL," +
                "[perfil] VARCHAR(20)  NOT NULL" +
                ");" +

                "CREATE TABLE[inout] (" +
                "[id] INTEGER NOT NULL PRIMARY KEY," +
                "[entrada] datetime NOT NULL," +
                "[saida] datetime NOT NULL," +
                "[fk_pessoa] INTEGER DEFAULT NULL," +
                "[fk_apartamento] INTEGER DEFAULT NULL," +
                "[observacao] STRING," +
                "FOREIGN KEY(fk_apartamento) REFERENCES apartamento(id)," +
                "FOREIGN KEY(fk_pessoa) REFERENCES pessoa(id)" +
                ");" +

                "CREATE VIEW ANDAR_VIEW AS " +
                "select "+
                "distinct ANDAR "+
                "from "+
                "apartamento;"+

                "CREATE VIEW COR_VIEW AS " +
                "select " +
                "distinct COR " +
                "from " +
                "veiculo;" +

                "CREATE VIEW MODELO_VIEW AS " +
                "select " +
                "distinct MODELO " +
                "from " +
                "veiculo;" +

                "CREATE VIEW NUMERO_VIEW AS " +
                "select " +
                "distinct NUMERO " +
                "from " +
                "apartamento;" +

                "CREATE VIEW RAMAL_VIEW AS " +
                "select " +
                "distinct RAMAL " +
                "from " +
                "apartamento;" +

                "CREATE VIEW RG_VIEW AS " +
                "select " +
                "RG " +
                "from " +
                "pessoa " +
                "where tipo = 'VISITANTE';" +

                "CREATE VIEW PLACA_VIEW AS " +
                "select "+
                "PLACA "+
                "from "+
                "veiculo;" +                             

                "CREATE VIEW INOUT_VIEW AS " +
                "select "+
                "a.nome NOME, "+
                "a.tipo PERFIL, "+
                "b.entrada ENTRADA, "+
                "b.saida SAIDA, "+
                "c.numero AP, "+
                "c.andar ANDAR, "+
                "c.bloco BLOCO, "+
                "a.ativo STATUS, "+
                "observacao OBSERVAÇÃO "+
                "from pessoa a "+
                "inner join inout b on a.id = b.fk_pessoa "+
                "inner join apartamento c on c.id = b.fk_apartamento "+
                "where a.tipo = 'VISITANTE';"+


                "CREATE VIEW CADASTRO_VIEW AS " +
                "select "+
                "a.id ID,"+
                "a.nome NOME,"+
                "a.rg RG,"+
                "a.foto FOTO,"+
                "b.numero AP,"+
                "b.bloco BLOCO,"+
                "b.andar ANDAR,"+
                "b.ramal RAMAL,"+
                "c.placa PLACA,"+
                "c.modelo MODELO,"+
                "c.cor COR,"+
                "a.tipo TIPO,"+
                "a.ativo STATUS,"+
                "a.email EMAIL,"+
                "a.cel CELULAR," +
                "d.login LOGIN," +
                "d.senha SENHA,"+
                "d.perfil USUARIO "+
                "from pessoa a "+
                "LEFT join apartamento b on a.fk_apartamento = b.id "+
                "LEFT Join veiculo c on a.fk_veiculo = c.id "+
                "LEFT JOIN acesso d on d.id = a.fk_acesso ", conexao);               

            command.ExecuteNonQuery();                
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1001, erro.ToString(), "Criar base de dados");                 
            }
            finally
            {
                this.FecharConexao();
            }
            
        }
    }
}
