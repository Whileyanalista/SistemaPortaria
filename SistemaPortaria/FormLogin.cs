using SistemaPortaria.bill;
using SistemaPortaria.dao;
using SistemaPortaria.GetSet;
using SistemaPortaria.Modell;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SistemaPortaria
{
    public partial class FormLogin : Form
    {
        int panelWidth;
        bool Hidden;

        public string salvarSenha;

        public FormLogin()
        {            
            InitializeComponent();
            SonoExtraBold();
            panelWidth = PanelSlide.Width = 0;
            Hidden = false;
        }

        private void buttonFecharModall_Click_1(object sender, EventArgs e)
        {
            timerSlider.Start();
        }

        //Implemento da tela Modal Slider
        private void timerSlider_Tick(object sender, EventArgs e)
        {

            if (Hidden)
            {
                PanelSlide.Width = PanelSlide.Width + 20;
                if (PanelSlide.Width >= 436)
                {
                    panelWidth = PanelSlide.Width = 436;
                    timerSlider.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                PanelSlide.Width = PanelSlide.Width - 20;
                if (PanelSlide.Width <= 0)
                {
                    timerSlider.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }      

        FormTelaPrincipal formTelaPrincipal = new FormTelaPrincipal();
        Acesso acesso = new Acesso();        
        Pessoa pessoa = new Pessoa();
        Apartamento apartamento = new Apartamento();
        Veiculo veiculo = new Veiculo();
        Bill classCasBll = new Bill();
        Ativacao ativacao = new Ativacao();
        Register register = new Register();
        FormCadastro formCadastro = new FormCadastro();

        private void SonoExtraBold()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("C:\\Systema\\Fontes\\Sono-ExtraBold.ttf");
            labelEntrar.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
            labelBem.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
            labelVindo.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
            labelLogin.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            labelSenha.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            labelRestantes.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            buttonEnter.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            buttonCancelar.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);

            labelRgMod.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            labelNomeCadMod.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            labelPerfilCadMod.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            labelLoginCadMod.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            labelSenhaCadMod.Font = new Font(pfc.Families[0], 13, FontStyle.Regular);
            buttonSalvarCadMod.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        }


        private void logar(Acesso acesso, Pessoa pessoa)
        {            
            acesso.login = textBoxLogin.Text;
            acesso.senha = textBoxSenha.Text;

            classCasBll.logar(acesso, pessoa);

            if (classCasBll.acessaradm == "SIM")
            {
                formTelaPrincipal.Show();
                formTelaPrincipal.telaDeCadastroToolStripMenuItem.Enabled = true;
                this.Visible = false;

            }
            else if (classCasBll.acessaradm == "NÃO")
            {
                formTelaPrincipal.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Registro de login e senha invalido!", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelCad.Text = "Esqueceu sua Senha";
            }
        }

        private void CadLogar(Pessoa pessoa)
        {
            pessoa.rg = textBoxRgCad.Text;

            classCasBll.CadLogar(pessoa);

            if(classCasBll.cadlogar == "Cadastroadm")
            {
                comboBoxPerfilCad.Text = "ADMIN";
                comboBoxPerfilCad.Enabled = false;
            }

            if (classCasBll.cadlogar == "" && salvarSenha == "novoCadastro")
            {               
               MessageBox.Show("O cadastro de novo usuario so pode ser realizado por um ADMINISTRADOR.\r\nFavor entrar em contaro com:\r\n \r\n" + pessoa.nome, "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (classCasBll.cadlogar != "" && salvarSenha == "novoCadastro")
            {
                //MessageBox.Show(""+ classCasBll.cadlogar+ " " + salvarSenha);

                if (classCasBll.cadlogar == "USUARIO" )
                {
                    comboBoxPerfilCad.Text = "USUARIO";
                    SalvarAcesso(acesso);
                    SalvarPessoa(pessoa);
                }
                else if (classCasBll.cadlogar == "ADMIN")
                {
                    comboBoxPerfilCad.Text = "ADMIN";
                    SalvarAcesso(acesso);
                    SalvarPessoa(pessoa);
                }
                else if (classCasBll.cadlogar == "Cadastroadm")
                {
                    if (DialogResult.Yes == MessageBox.Show("Para este cadastro voce precisa ser um ADMINISTRADOR, gostaria de continuar com o registro?", "confirma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        SalvarAcesso(acesso);
                        SalvarPessoa(pessoa);
                    }
                }
            }
                      
        }

        private void dtLisenca(Register register)
        {
            classCasBll.dtLisenca(register);

            DateTime dtfim = DateTime.Parse(register.dt_fim);
            DateTime dtatual = DateTime.Parse(register.dt_uso);
            TimeSpan dtuso;            

            dtuso = DateTime.Parse(register.dt_fim) - DateTime.Parse(register.dt_uso);    

            switch (dtuso.Days)
                {
                    case 30:
                        pictureBox30.Visible = true;
                        break;
                    case 29:
                        pictureBox29.Visible = true;
                        break;
                    case 28:
                        pictureBox28.Visible = true;
                        break;
                    case 27:
                        pictureBox27.Visible = true;
                        break;
                    case 26:
                        pictureBox26.Visible = true;
                        break;
                    case 25:
                        pictureBox25.Visible = true;
                        break;
                    case 24:
                        pictureBox24.Visible = true;
                        break;
                    case 23:
                        pictureBox23.Visible = true;
                        break;
                    case 22:
                        pictureBox22.Visible = true;
                        break;
                    case 21:
                        pictureBox21.Visible = true;
                        break;
                    case 20:
                        pictureBox20.Visible = true;
                        break;
                    case 19:
                        pictureBox19.Visible = true;
                        break;
                    case 18:
                        pictureBox18.Visible = true;
                        break;
                    case 17:
                        pictureBox17.Visible = true;
                        break;
                    case 16:
                        pictureBox16.Visible = true;
                        break;
                    case 15:
                        pictureBox15.Visible = true;
                        break;
                    case 14:
                        pictureBox14.Visible = true;
                        break;
                    case 13:
                        pictureBox13.Visible = true;
                        break;
                    case 12:
                        pictureBox12.Visible = true;
                        break;
                    case 11:
                        pictureBox11.Visible = true;
                        break;
                    case 10:
                        pictureBox10.Visible = true;
                        break;
                    case 09:
                        pictureBox9.Visible = true;
                        break;
                    case 8:
                        pictureBox8.Visible = true;
                        break;
                    case 7:
                        pictureBox7.Visible = true;
                        break;
                    case 6:
                        pictureBox6.Visible = true;
                        break;
                    case 5:
                        pictureBox5.Visible = true;
                        break;
                    case 4:
                        pictureBox4.Visible = true;
                        break;
                    case 3:
                        pictureBox3.Visible = true;
                        break;
                    case 2:
                        pictureBox2.Visible = true;                   
                    break;
                    case 1:
                        pictureBox1.Visible = true;
                        labelBem.ForeColor = Color.Red;
                        labelRestantes.ForeColor = Color.Red;
                    break;
                    default:
                        pictureBox0.Visible = true;
                        labelBem.Text = "FINALIZADO";
                        labelBem.ForeColor = Color.Red;
                        labelRestantes.ForeColor = Color.Red;
                        break;
                }

        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "")
            {
                labelLogin.ForeColor = Color.Red;
                textBoxLogin.Focus();
                new BalloonTip("Erro", "Campo Obrigatorio", buttonImgLogin, BalloonTip.ICON.INFO, 2000);
            }
            else if (textBoxSenha.Text == "")
            {
                labelSenha.ForeColor = Color.Red;
                textBoxSenha.Focus();
                new BalloonTip("Erro", "Campo Obrigatorio", buttonImgSenha, BalloonTip.ICON.INFO, 2000);
            }
            else
            {
                logar(acesso, pessoa);                
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();             
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            
            timerSlider.Start();

            
            dtLisenca(register);
            CadLogar(pessoa);
            ativacao.retornoEmail(register);      
        }        
       
        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == 13)
            {
                if (textBoxLogin.Text == "")
                {
                    labelLogin.ForeColor = Color.Red;
                    textBoxLogin.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonImgLogin, BalloonTip.ICON.INFO, 2000);
                }
                else if (textBoxSenha.Text == "")
                {
                    labelSenha.ForeColor = Color.Red;
                    textBoxSenha.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonImgSenha, BalloonTip.ICON.INFO, 2000);
                }
                else
                {
                    logar(acesso, pessoa);
                }
            }            
        }

        private void textBoxSenha_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {                
                if (textBoxLogin.Text == "")
                {
                    labelLogin.ForeColor = Color.Red;
                    textBoxLogin.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonImgLogin, BalloonTip.ICON.INFO, 2000);
                }
                else if (textBoxSenha.Text == "")
                {
                    labelSenha.ForeColor = Color.Red;
                    textBoxSenha.Focus();
                    new BalloonTip("Erro", "Campo Obrigatorio", buttonImgSenha, BalloonTip.ICON.INFO, 2000);
                }
                else
                {
                    logar(acesso, pessoa);
                }
            }
        }   
       

        private void SalvarPessoa(Pessoa pessoa)
        {
            pessoa.nome = textBoxNomeCad.Text;
            pessoa.rg = textBoxRgCad.Text;           
            pessoa.tipo = "COLABORADOR";
            pessoa.foto = "System.Byte[]";
            pessoa.ativo = "ATIVO";


            classCasBll.SalvarPessoa(pessoa, acesso, apartamento, veiculo);
        }

        private void SalvarAcesso(Acesso acesso)
        {            
            acesso.login = textBoxLoginCad.Text;
            acesso.senha = textBoxSenhaCad.Text;
            acesso.perfil = comboBoxPerfilCad.Text;
            classCasBll.salvarAcesso(acesso, pessoa);            
        }
       
        private void buttonFecharMod_Click(object sender, EventArgs e)
        {
            timerSlider.Start();
        }

        private void labelCad_Click(object sender, EventArgs e)
        {
            timerSlider.Start();
        }

        private void buttonSalvarCadMod_Click(object sender, EventArgs e)
        {
            salvarSenha = "novoCadastro";

            if (textBoxRgCad.Text == "##.###.###-##")
            {
                labelNomeCadMod.ForeColor = Color.Red;
                textBoxNomeCad.Focus();
            }
            else if (textBoxNomeCad.Text == "")
            {
                labelRgMod.ForeColor = Color.Red;
                textBoxRgCad.Focus();
            }
            else if (textBoxLoginCad.Text == "")
            {
                labelLoginCadMod.ForeColor = Color.Red;
                textBoxLoginCad.Focus();
            }
            else if (textBoxSenhaCad.Text == "")
            {
                labelSenhaCadMod.ForeColor = Color.Red;
                textBoxSenhaCad.Focus();
            }
            else
            {
                CadLogar(pessoa);
                timerSlider.Start();
            }
        }
    }
}
