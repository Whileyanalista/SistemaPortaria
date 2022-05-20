using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaPortaria.GetSet
{
    public class Register
    {
        public int id_register { get; set; }
        public string validarID { get; set; }
        public string chaveID { get; set; }
        public string email { get; set; }
        public string dt_inicio { get; set; }
        public string dt_fim { get; set; }
        public string dt_uso { get; set; }
        public string nm_local { get; set; }
        public int time { get; set; }
        public int state_cod { get; set; }
    }

    public class Acesso
    {
        public int id { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string perfil { get; set; }
    }

    public class InOut
    {
        public int id { get; set; }
        public string entrada { get; set; }
        public string saida { get; set; }
        public int fk_pessoa { get; set; }
        public int fk_apartamento { get; set; }
        public string observacao { get; set; }
    }

    public class Pessoa
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string rg { get; set; }
        public string foto { get; set; }
        public Image Img_fot { get; set; }
        public string tipo { get; set; }
        public string ativo { get; set; }
        public string cel { get; set; }
        public string email { get; set; }
        public string msn { get; set; }
        public int fk_apartamento { get; set; }
        public int fk_veiculo { get; set; } 
        public int fk_acesso { get; set; }
    }
    public class Veiculo
    {
        public int id { get; set; }
        public string placa { get; set; }
        public string modelo { get; set; }
        public string cor { get; set; }

    }


    public class Apartamento
    {
        public int id { get; set; }
        public string numero { get; set; }
        public string bloco { get; set; }
        public string andar { get; set; }
        public string ramal { get; set; }
    }
}

    