using SistemaPortaria.GetSet;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPortaria.dao
{
    public class ClassLogar: ClassConexao
    {
        //SQLiteConnection Conexion;
        SQLiteCommand comando = null;       

        public bool acesso = false;

        public void logar(Acesso acesso)
        {
            try
            {
                AbrirConexion();

                comando = new SQLiteCommand("select * from acesso where login = @login and senha = @senha", conexao);

                comando.Parameters.AddWithValue("@login", acesso.login);
                comando.Parameters.AddWithValue("@senha", acesso.senha);

                SQLiteDataReader lgUser;

                lgUser = comando.ExecuteReader();

                if (lgUser.Read())
                {
                    this.acesso = true;

                    string perfil = lgUser.GetString(3);
                    string nome = lgUser.GetString(1);
                    int id = lgUser.GetInt32(0);
                   
                    lgUser.Close();                    
                }
                else
                {
                    string perfil = lgUser.GetString(3);
                    string nome = lgUser.GetString(1);                   
                }

            }
            catch (Exception)
            {
              this.acesso = false;
            }
            finally
            {
                FecharConexao();
            }
        }

    }
}
