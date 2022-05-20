using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaPortaria.bill;
using SistemaPortaria.dao;
using SistemaPortaria.GetSet;
using SistemaPortaria.Modell;

namespace SistemaPortaria
{
    public partial class FormValidacao : Form
    {
        public FormValidacao()
        {
            InitializeComponent();
        }


        private void SonoExtraBold()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("C:\\Systema\\Fontes\\Sono-ExtraBold.ttf");
            labelValidar.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
            labelNomeCondominio.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            labelEmail.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            labelSerie.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            buttonRegister.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        }
        

        Ativacao classAtivacao = new Ativacao();
        Bill casBll = new Bill();        
        Register register = new Register();
      
        public int time = 1;

        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);


        // Um método que verifica se esta conectado
        private static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }

        private void retornoEmail(Register register)
        {
            casBll.retornoEmail(register);            
            
           if(register.email != null)
            {
                textBoxConominio.Text = register.nm_local;
                textBoxEmailAtivar.Text = register.email;
                textBoxChave.Text = register.chaveID;                
            }           
        }        

        public void interOnline()
        {
            if (IsConnected())
            {
                pictureBoxConecxãoOk.Visible = true;
                pictureBoxConecxão.Visible = false;
            }
            else
            {
                pictureBoxConecxão.Visible = true;
                pictureBoxConecxãoOk.Visible = false;
                new BalloonTip("Internet", "Favor verificar a sua internet para efetuar a validação", pictureBoxConecxão, BalloonTip.ICON.ERROR, 5000);                
            }
        }     


        public void salvarAtivacao(Register register)
        {
            if (textBoxEmailAtivar.Text != "")
            {
                SKGL.Generate GEN = new SKGL.Generate();
                GEN.secretPhase = "123";

                register.validarID = GEN.doKey(Convert.ToInt32("15"));

                register.chaveID = textBoxChave.Text;
                register.email = textBoxEmailAtivar.Text;
                register.nm_local = textBoxConominio.Text;

                DateTime inicio = DateTime.Now;

                DateTime fim = inicio.AddDays(30);

                DateTime dtuso = DateTime.Now;

                DateTime Data = DateTime.Now;

                register.dt_inicio = Data.ToString(inicio.ToString("yyy-MM-dd"));
                register.dt_fim = Data.ToString(fim.ToString("yyy-MM-dd"));
                register.dt_uso = Data.ToString(dtuso.ToString("yyy-MM-dd"));

                register.state_cod = 1;

                casBll.salvarAtivacao(register);               

            }          

            retornoEmail(register);

            if (textBoxChave.Text == register.validarID)
            {
                panelValidacao.Enabled = false;
                FormLogin formLogin = new FormLogin();
                formLogin.Show();
                this.Visible = false;
            }
            else if (textBoxChave.Text != register.validarID && textBoxChave.Text != "")
            {
                MessageBox.Show("a Chave e INVALIDA!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Validacao_Load(object sender, EventArgs e)
        {           
            SonoExtraBold();
            interOnline();
            retornoEmail(register);            

            if (textBoxChave.Enabled)
            {
                buttonKeyHabilite.BackColor = Color.Gray;
            }
            if (register.email != null)
            {   
                buttonKeyHabilite.Enabled = true;                
            }
            if (textBoxChave.Enabled == true)
            {
                if (textBoxChave.Text == "" && pictureBoxConecxãoOk.Visible == true)
                {                   
                    new BalloonTip("Validacao", "Favor informar a chave enviada para seu e-mail", buttonKeyHabilite, BalloonTip.ICON.INFO, 3000);
                }
                else if (pictureBoxConecxãoOk.Visible == true)
                {
                    new BalloonTip("Validacao", "Serial incorreta! confirme os dados e tente novamente", buttonKeyHabilite, BalloonTip.ICON.INFO, 3000);
                }
            }
            else if (textBoxChave.Text == register.validarID)
            {                
                FormLogin formLogin = new FormLogin();
                formLogin.Show();
                this.Visible = false;
            }
        }

        private void buttonAtivação_Click(object sender, EventArgs e)
        {
            if (IsConnected())
            {
                if (textBoxConominio.Text == "")
                {
                    labelNomeCondominio.ForeColor = Color.Red;
                    textBoxConominio.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonCond, BalloonTip.ICON.INFO, 2000);
                }
                else if (textBoxEmailAtivar.Text == "")
                {
                    labelEmail.ForeColor = Color.Red;
                    textBoxEmailAtivar.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonHabilitarEmail, BalloonTip.ICON.INFO, 2000);
                }
                else if (textBoxConominio.Text != register.nm_local || textBoxEmailAtivar.Text != register.email || textBoxChave.Text != "")
                {
                    salvarAtivacao(register);
                    textBoxConominio.ForeColor = Color.Gray;
                    textBoxEmailAtivar.ForeColor = Color.Gray;

                }
                if (register.email != null)
                {                    
                    buttonKeyHabilite.Enabled = true;
                    textBoxChave.Enabled = true;

                    if (textBoxChave.Text == "" && pictureBoxConecxãoOk.Visible == true)
                    {
                        new BalloonTip("Validacao", "Favor informar a chave enviada para seu e-mail", buttonKeyHabilite, BalloonTip.ICON.INFO, 3000);
                    }
                    else if (pictureBoxConecxãoOk.Visible == true)
                    {
                        new BalloonTip("Validacao", "Serial incorreta! confirme os dados e tente novamente", buttonKeyHabilite, BalloonTip.ICON.INFO, 3000);
                    }

                }
            }
            else
            {
                interOnline();
            }
            
        } 
        private void textBoxEmailAtivar_Click(object sender, EventArgs e)
        {
            interOnline();
            textBoxEmailAtivar.ForeColor = Color.Black;
            textBoxConominio.ForeColor = Color.Gray;
        }

        private void textBoxConominio_Click(object sender, EventArgs e)
        {
            interOnline();
            textBoxConominio.ForeColor = Color.Black;
            textBoxEmailAtivar.ForeColor = Color.Gray;
        }

        private void textBoxChave_Click(object sender, EventArgs e)
        {
            interOnline();
            textBoxConominio.ForeColor = Color.Gray;
            textBoxEmailAtivar.ForeColor = Color.Gray;
        }

        private void textBoxConominio_KeyPress(object sender, KeyPressEventArgs e)
        {           

            if (e.KeyChar == 13)
            {
                if (textBoxConominio.Text == "")
                {
                    labelNomeCondominio.ForeColor = Color.Red;
                    textBoxConominio.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonCond, BalloonTip.ICON.INFO, 2000);
                }
                else if (textBoxEmailAtivar.Text == "")
                {
                    labelEmail.ForeColor = Color.Red;
                    textBoxEmailAtivar.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonHabilitarEmail, BalloonTip.ICON.INFO, 2000);
                }
                else if (textBoxConominio.Text != register.nm_local || textBoxEmailAtivar.Text != register.email || textBoxChave.Text != "")
                {
                    salvarAtivacao(register);
                }
            }
        }

        private void textBoxEmailAtivar_KeyPress(object sender, KeyPressEventArgs e)
        {           

            if (e.KeyChar == 13)
            {
                if (textBoxConominio.Text == "")
                {
                    labelNomeCondominio.ForeColor = Color.Red;
                    textBoxConominio.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonCond, BalloonTip.ICON.INFO, 2000);
                }
                else if (textBoxEmailAtivar.Text == "")
                {
                    labelEmail.ForeColor= Color.Red;
                    textBoxEmailAtivar.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonHabilitarEmail, BalloonTip.ICON.INFO, 2000);
                }               
                else if (textBoxConominio.Text != register.nm_local || textBoxEmailAtivar.Text != register.email || textBoxChave.Text != "")
                {
                    salvarAtivacao(register);
                }
            }
        }
       
        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxChave_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxChave.Text == "" && pictureBoxConecxãoOk.Visible == true)
            {
                new BalloonTip("Validacao", "Favor informar a chave enviada para seu e-mail", buttonKeyHabilite, BalloonTip.ICON.INFO, 3000);
            }
            else if (pictureBoxConecxãoOk.Visible == true)
            {
                new BalloonTip("Validacao", "Serial incorreta! confirme os dados e tente novamente", buttonKeyHabilite, BalloonTip.ICON.INFO, 3000);
            }
        }
        
    }
}
