using SistemaPortaria.bill;
using SistemaPortaria.GetSet;
using SistemaPortaria.Modell;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SistemaPortaria
{
    public partial class FormCadastro : Form 
    {

        public FormCadastro()
        {
            InitializeComponent();            
        }

        
        Bill classCasBll = new Bill();
        Pessoa pessoa = new Pessoa();
        Acesso acesso = new Acesso();
        InOut inout = new InOut();
        Apartamento apartamento = new Apartamento();
        Veiculo veiculo = new Veiculo();  
       
        
        private void FormCadastro_Load(object sender, EventArgs e)
        {
            lislistaCadastro();
            //completarCor();
            //completarModelo();
            //completarAndar();
            //completarApartamento();
            //completarRamal();
        }

        void completarRamal()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarRamal().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarRamal().Rows[i]["RAMAL"].ToString());
            }
            textBoxRamalCad.AutoCompleteCustomSource = lista;
        }

        void completarApartamento()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarApartamento().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarApartamento().Rows[i]["NUMERO"].ToString());
            }
            textBoxApCad.AutoCompleteCustomSource = lista;
        }

        //ALTOCOMPLETE 
        void completarAndar()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarAndar().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarAndar().Rows[i]["ANDAR"].ToString());
            }
            textBoxAndarCad.AutoCompleteCustomSource = lista;
        }

        //ALTOCOMPLETE 
        void completarCor()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarCor().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarCor().Rows[i]["COR"].ToString());
            }
            textBoxCorCad.AutoCompleteCustomSource = lista;
        }

        void completarModelo()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarModelo().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarModelo().Rows[i]["MODELO"].ToString());
            }
            textBoxModeloCad.AutoCompleteCustomSource = lista;
        }

        private void pesquisacad(Pessoa pessoa)
        {
            string tipo = "";

            if (checkBoxMorador.Checked)
            {
                tipo = "MORADOR";
            }
            else if (checkBoxColaborador.Checked)
            {
                tipo = "COLABORADOR";
            }
            else if (checkBoxVisitante.Checked)
            {
                tipo = "VISITANTE";
            }

            pessoa.nome = textBoxPesquisaNome.Text;

            pessoa.tipo = tipo;

            dataGridViewTelaCad.DataSource = classCasBll.pesquisacad(pessoa);
        }

        private void pesquisaInOut(InOut inout)
        {
            inout.entrada = maskedTextBoxEntrada.Text;
            inout.saida = maskedTextBoxSaida.Text;
            
            dataGridViewTelaCad.DataSource = classCasBll.pesquisaInOut(inout);
        }

        public void lislistaCadastro()
        {
            if (checkBoxInOut.Checked)
            {
                dataGridViewTelaCad.DataSource = classCasBll.listaInout();              


                //DEFINIR TAMANHO DA COLUNA 
                dataGridViewTelaCad.Columns["nome"].Width = 200;
                dataGridViewTelaCad.Columns["PERFIL"].Width = 90;
                dataGridViewTelaCad.Columns["ENTRADA"].Width = 100;
                dataGridViewTelaCad.Columns["SAIDA"].Width = 100;
                dataGridViewTelaCad.Columns["AP"].Width = 40;
                dataGridViewTelaCad.Columns["ANDAR"].Width = 50;
                dataGridViewTelaCad.Columns["BLOCO"].Width = 50;
                dataGridViewTelaCad.Columns["STATUS"].Width = 50;
            }
            else
            {
                if (checkBoxColaborador.Checked)
                {
                    pessoa.tipo =  "COLABORADOR";
                    dataGridViewTelaCad.DataSource = classCasBll.listaCadastro(pessoa);
                    dataGridViewTelaCad.Sort(dataGridViewTelaCad.Columns["id"], ListSortDirection.Descending);
                }                
                else if(checkBoxMorador.Checked)
                {
                    pessoa.tipo = "MORADOR";
                    dataGridViewTelaCad.DataSource = classCasBll.listaCadastro(pessoa);
                    dataGridViewTelaCad.Sort(dataGridViewTelaCad.Columns["id"], ListSortDirection.Descending);
                }
                else if (checkBoxVisitante.Checked)
                {
                    pessoa.tipo = "VISITANTE";
                    dataGridViewTelaCad.DataSource = classCasBll.listaCadastro(pessoa);
                    dataGridViewTelaCad.Sort(dataGridViewTelaCad.Columns["id"], ListSortDirection.Descending);
                }
                else
                {
                    dataGridViewTelaCad.DataSource = classCasBll.listaCadastro();
                    dataGridViewTelaCad.Sort(dataGridViewTelaCad.Columns["id"], ListSortDirection.Descending);                   
                }

                //ESCONDER CAMPO
                dataGridViewTelaCad.Columns["foto"].Visible = false;
                dataGridViewTelaCad.Columns["login"].Visible = false;
                dataGridViewTelaCad.Columns["senha"].Visible = false;
                dataGridViewTelaCad.Columns["celular"].Visible = false;
                dataGridViewTelaCad.Columns["email"].Visible = false;


                //DEFINIR TAMANHO DA COLUNA 
                dataGridViewTelaCad.Columns["id"].Width = 50;
                dataGridViewTelaCad.Columns["nome"].Width = 230;
                dataGridViewTelaCad.Columns["AP"].Width = 73;
                dataGridViewTelaCad.Columns["bloco"].Width = 50;
                dataGridViewTelaCad.Columns["andar"].Width = 50;
                dataGridViewTelaCad.Columns["ramal"].Width = 60;
                dataGridViewTelaCad.Columns["placa"].Width = 70;
                dataGridViewTelaCad.Columns["modelo"].Width = 100;
                dataGridViewTelaCad.Columns["cor"].Width = 90;
                dataGridViewTelaCad.Columns["tipo"].Width = 100;
                dataGridViewTelaCad.Columns["STATUS"].Width = 50;
                dataGridViewTelaCad.Columns["usuario"].Width = 70;

            } 

            //DESIGNE DO TITULO
            dataGridViewTelaCad.EnableHeadersVisualStyles = false;
            dataGridViewTelaCad.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewTelaCad.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 63, 130);
            dataGridViewTelaCad.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //PARAMETROS PARA DESIGN DA TABELA           
            dataGridViewTelaCad.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewTelaCad.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);//COR DOS CAMPOS 
            //dataGridViewTabChamadoTP.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;//LINHAS VERTICAS
            dataGridViewTelaCad.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 231, 145);//COR DE SELEÇAO 
            dataGridViewTelaCad.DefaultCellStyle.SelectionForeColor = Color.Black;// COR DO CAMPO AO SELECIONAR
            dataGridViewTelaCad.BackgroundColor = Color.White;//COR DA TABELA VASIA
        }
                
        private void dataGridViewTelaCadClik()
        {
            if (checkBoxInOut.Checked == false)
            {
                if (dataGridViewTelaCad.CurrentRow != null)
                {
                    labelIDCad.Text = dataGridViewTelaCad.CurrentRow.Cells[0].Value.ToString();
                    labelNomeColaborador.Text = dataGridViewTelaCad.CurrentRow.Cells[1].Value.ToString();
                    textBoxNomeCad.Text = dataGridViewTelaCad.CurrentRow.Cells[1].Value.ToString();
                    textBoxRgCad.Text = dataGridViewTelaCad.CurrentRow.Cells[2].Value.ToString();
                    labelRgAcesso.Text = dataGridViewTelaCad.CurrentRow.Cells[2].Value.ToString();

                    textBoxCaminhoFoto.Text = dataGridViewTelaCad.CurrentRow.Cells[3].Value.ToString();

                    pictureBoxFotoCad.Image = null;

                    if (textBoxCaminhoFoto.Text != "")
                    {
                        MemoryStream ms = new MemoryStream((byte[])dataGridViewTelaCad.CurrentRow.Cells[3].Value);
                        pictureBoxFotoCad.Image = Image.FromStream(ms);
                    }

                    textBoxApCad.Text = dataGridViewTelaCad.CurrentRow.Cells[4].Value.ToString();
                    textBoxBlocoCad.Text = dataGridViewTelaCad.CurrentRow.Cells[5].Value.ToString();
                    textBoxAndarCad.Text = dataGridViewTelaCad.CurrentRow.Cells[6].Value.ToString();
                    textBoxRamalCad.Text = dataGridViewTelaCad.CurrentRow.Cells[7].Value.ToString();
                    textBoxPlacaCad.Text = dataGridViewTelaCad.CurrentRow.Cells[8].Value.ToString();
                    textBoxModeloCad.Text = dataGridViewTelaCad.CurrentRow.Cells[9].Value.ToString();
                    textBoxCorCad.Text = dataGridViewTelaCad.CurrentRow.Cells[10].Value.ToString();
                    comboBoxStatus.Text = dataGridViewTelaCad.CurrentRow.Cells[12].Value.ToString();
                    textBoxCadEmail.Text = dataGridViewTelaCad.CurrentRow.Cells[13].Value.ToString();
                    maskedTextBoxCadCel.Text = dataGridViewTelaCad.CurrentRow.Cells[14].Value.ToString();
                    textBoxLoginCad.Text = dataGridViewTelaCad.CurrentRow.Cells[15].Value.ToString();
                    textBoxSenhaCad.Text = dataGridViewTelaCad.CurrentRow.Cells[16].Value.ToString();
                    comboBoxPerfilCad.Text = dataGridViewTelaCad.CurrentRow.Cells[17].Value.ToString();
                }
                if ("COLABORADOR" == dataGridViewTelaCad.CurrentRow.Cells[11].Value.ToString())
                {
                    groupBoxApartamento.Enabled = false;
                    groupBoxVeiculoCad.Enabled = true;
                    groupBoxColaboradorCad.Enabled = true;

                }
                else if ("MORADOR" == dataGridViewTelaCad.CurrentRow.Cells[11].Value.ToString())
                {
                    groupBoxApartamento.Enabled = true;
                    groupBoxVeiculoCad.Enabled = true;
                    groupBoxColaboradorCad.Enabled = false;

                }
                else if ("VISITANTE" == dataGridViewTelaCad.CurrentRow.Cells[11].Value.ToString())
                {
                    groupBoxApartamento.Enabled = true;
                    groupBoxVeiculoCad.Enabled = true;
                    groupBoxColaboradorCad.Enabled = false;
                }

            }          
                      
        }       

        private void pesquiImag()
        {
            string foto;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files(*.jpg)|*,jpg|PNG Files(*.png)|*.png|AllFiles(*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foto = openFileDialog.FileName.ToString();
                pictureBoxFotoCad.ImageLocation = foto;
                textBoxCaminhoFoto.Text = foto;
            }
        }

        private void SalvarPessoa( Pessoa pessoa)
        {
            pessoa.nome = textBoxNomeCad.Text;
            pessoa.rg = textBoxRgCad.Text;
            pessoa.cel = maskedTextBoxCadCel.Text;
            pessoa.email = textBoxCadEmail.Text;
            pessoa.foto = textBoxCaminhoFoto.Text;
            pessoa.ativo = comboBoxStatus.Text;           

            if (checkBoxMorador.Checked)
            {
                pessoa.tipo = "MORADOR";
            }
            else if (checkBoxColaborador.Checked)
            {
                pessoa.tipo = "COLABORADOR";
            }
            else if (checkBoxVisitante.Checked)
            {
                pessoa.tipo = "VISITANTE";
            }            
            classCasBll.SalvarPessoa(pessoa,acesso, apartamento, veiculo);
        }

        private void SalvarAcesso(Acesso acesso)
        {
            if (checkBoxColaborador.Checked)
            {
                acesso.login = textBoxLoginCad.Text;
                acesso.senha = textBoxSenhaCad.Text;
                acesso.perfil = comboBoxPerfilCad.Text;

                classCasBll.salvarAcesso(acesso, pessoa);
            }            
        }

        private void SalvarApartamento(Apartamento apartamento, Pessoa pessoa)
        {
            apartamento.numero = textBoxApCad.Text;
            apartamento.andar = textBoxAndarCad.Text;
            apartamento.bloco = textBoxBlocoCad.Text;
            apartamento.ramal = textBoxRamalCad.Text;

            classCasBll.salvarApartamento(apartamento, pessoa);
        }

        private void SalvarVeiculo(Veiculo veiculo, Pessoa pessoa)
        {
            veiculo.placa = textBoxPlacaCad.Text;
            veiculo.modelo = textBoxModeloCad.Text;
            veiculo.cor = textBoxCorCad.Text;

            classCasBll.salvarVeiculo(veiculo,pessoa);
        }

        private void deletarPessoa(Pessoa pessoa, Acesso acesso)
        {
            pessoa.rg = textBoxRgCad.Text;

            classCasBll.deletarPessoa(pessoa,acesso);
        }       

        
        private void buttonFoto_Click(object sender, EventArgs e)
        {            
            pesquiImag();  
        }

        private void buttonSalvarCad_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxCadCel.Text == "  (  )     -    ")
            {
                new BalloonTip("Celular", "Este campo sera usado para notificação ao proprietario.", maskedTextBoxCadCel, BalloonTip.ICON.INFO, 5000);
            }            
            else if (textBoxNomeCad.Text == "")
            {
                labelNomeCad.ForeColor = Color.Red;
                textBoxNomeCad.Focus();

            }
            else if (textBoxRgCad.Text == "")
            {
                labelRg.ForeColor = Color.Red;
                textBoxRgCad.Focus();
            }
            else if (checkBoxColaborador.Checked==true)
            {
                if (textBoxLoginCad.Text == "")
                {
                    labelLoginCad.ForeColor = Color.Red;
                    textBoxLoginCad.Focus();
                }
                else if (textBoxSenhaCad.Text == "")
                {
                    labelSenhaCad.ForeColor = Color.Red;
                    textBoxSenhaCad.Focus();
                }
                else
                {
                    if (checkBoxMorador.Checked || checkBoxColaborador.Checked || checkBoxVisitante.Checked)
                    {
                        SalvarVeiculo(veiculo, pessoa);
                        SalvarAcesso(acesso);
                        SalvarPessoa(pessoa);
                        lislistaCadastro();
                        limparTextBoxes(pessoa);
                    }
                    else
                    {
                        new BalloonTip("Perfil", "Favor escolha um perfil", groupBoxPerfil, BalloonTip.ICON.INFO, 5000);
                    }
                    
                }
            }            
            if (checkBoxVisitante.Checked == true || checkBoxMorador.Checked == true)
            {
                if (textBoxRamalCad.Text == "")
                {
                    labelRamal.ForeColor = Color.Red;
                    textBoxRamalCad.Focus();
                }
                else if (textBoxAndarCad.Text == "")
                {
                    labelAndarCad.ForeColor = Color.Red;
                    textBoxAndarCad.Focus();
                }
                else if (textBoxBlocoCad.Text == "")
                {
                    labelBlocoCad.ForeColor = Color.Red;
                    textBoxBlocoCad.Focus();
                }
                else if (textBoxApCad.Text == "")
                {
                    labelApCad.ForeColor = Color.Red;
                    textBoxApCad.Focus();
                }
                else
                {
                    if (checkBoxMorador.Checked || checkBoxColaborador.Checked || checkBoxVisitante.Checked)
                    {
                        SalvarApartamento(apartamento, pessoa);
                        SalvarVeiculo(veiculo, pessoa);
                        SalvarPessoa(pessoa);
                        lislistaCadastro();
                        limparTextBoxes(pessoa);
                    }                    
                }
            }
            else
            {
                new BalloonTip("Perfil", "Favor escolha um perfil", groupBoxPerfil, BalloonTip.ICON.INFO, 5000);
            }

        }

        private void textBoxCorCad_TextChanged(object sender, EventArgs e)
        {
            textBoxCorCad.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBoxNomeCad_TextChanged(object sender, EventArgs e)
        {
            textBoxNomeCad.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBoxModeloCad_TextChanged(object sender, EventArgs e)
        {
            textBoxModeloCad.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBoxPlacaCad_TextChanged(object sender, EventArgs e)
        {
            textBoxPlacaCad.CharacterCasing = CharacterCasing.Upper;
        }      

        private void comboBoxPerfilCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxPerfilCad.AutoCompleteSource = AutoCompleteSource.AllUrl;
        }

        private void textBoxBlocoCad_TextChanged(object sender, EventArgs e)
        {
            textBoxBlocoCad.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBoxRamalCad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //NAO PERMITE QUE DIGITE LETRAS
            if (!char.IsDigit(e.KeyChar) & (e.KeyChar != 13) & (e.KeyChar != 46) & (e.KeyChar != 8))
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                
            }

        }

        private void textBoxAndarCad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //NAO PERMITE QUE DIGITE LETRAS
            if (!char.IsDigit(e.KeyChar) & (e.KeyChar != 13) & (e.KeyChar != 46) & (e.KeyChar != 8))
            {
                e.Handled = true;
            }            
        }

        private void textBoxApCad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //NAO PERMITE QUE DIGITE LETRAS
            if (!char.IsDigit(e.KeyChar) & (e.KeyChar != 13) & (e.KeyChar != 46) & (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void textBoxRgCad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBoxMorador.Checked || checkBoxColaborador.Checked || checkBoxVisitante.Checked)
            {
                
            }
            else
            {
                new BalloonTip("Perfil", "Favor escolha um perfil", groupBoxPerfil, BalloonTip.ICON.INFO, 5000);
            }
        }

        private void textBoxNomeCad_KeyPress(object sender, KeyPressEventArgs e)
       {
            if (checkBoxMorador.Checked || checkBoxColaborador.Checked || checkBoxVisitante.Checked)
            {

            }
            else
            {
                new BalloonTip("Perfil", "Favor escolha um perfil", groupBoxPerfil, BalloonTip.ICON.INFO, 5000);
            }
        }

        

        private void dataGridViewTelaCad_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            dataGridViewTelaCadClik();
        }      

        private void checkBoxMorador_Click(object sender, EventArgs e)
        {
            checkBoxInOut.Checked = false;
            checkBoxGeral.Checked = false;
            lislistaCadastro();            
            checkBoxMorador.Checked = true;
            checkBoxColaborador.Checked = false;
            checkBoxVisitante.Checked = false;
            groupBoxApartamento.Enabled = true;
            groupBoxVeiculoCad.Enabled = true;
            groupBoxColaboradorCad.Enabled = false;
            textBoxLoginCad.Text = "";
            textBoxSenhaCad.Text = "";
        }

        private void checkBoxColaborador_Click(object sender, EventArgs e)
        {
            checkBoxInOut.Checked = false;
            checkBoxGeral.Checked = false;
            lislistaCadastro();
            checkBoxColaborador.Checked = true;
            checkBoxMorador.Checked = false;
            checkBoxVisitante.Checked = false;
            groupBoxApartamento.Enabled = false;
            groupBoxVeiculoCad.Enabled = true;
            groupBoxColaboradorCad.Enabled = true;
            textBoxBlocoCad.Text = "";
            textBoxAndarCad.Text = "";
            textBoxRamalCad.Text = "";
            textBoxApCad.Text = "";
        }

        private void checkBoxVisitante_Click(object sender, EventArgs e)
        {
            checkBoxInOut.Checked = false;
            checkBoxGeral.Checked = false;
            lislistaCadastro();
            checkBoxVisitante.Checked = true;
            checkBoxColaborador.Checked = false;
            checkBoxMorador.Checked = false;
            groupBoxApartamento.Enabled = true;
            groupBoxVeiculoCad.Enabled = true;
            groupBoxColaboradorCad.Enabled = false;
            textBoxLoginCad.Text = "";
            textBoxSenhaCad.Text = "";
        }

        private void limparTextBoxes(Pessoa pessoa)
        {
           
            textBoxCaminhoFoto.Text = "System.Byte[]";
            pictureBoxFotoCad.Image = null;
            textBoxNomeCad.Text = "";
            textBoxRgCad.Text = "";
            textBoxCadEmail.Text = "email@email.com.br";
            maskedTextBoxCadCel.Text = "  (  )     -    ";
            textBoxApCad.Text = "";
            textBoxBlocoCad.Text = "";
            textBoxAndarCad.Text = "";
            textBoxRamalCad.Text = "";
            textBoxModeloCad.Text = "";
            textBoxPlacaCad.Text = "";
            textBoxCorCad.Text = "";
            textBoxLoginCad.Text = "";
            textBoxSenhaCad.Text = "";
            labelNomeColaborador.Text = "";
            labelRgAcesso.Text = "";
            labelIDCad.Text = "";           
            comboBoxStatus.Text = "ATIVO";
            comboBoxPerfilCad.Text = "USUARIO";
            checkBoxColaborador.Checked = false;
            checkBoxMorador.Checked = false;
            checkBoxVisitante.Checked = false;
            pessoa.fk_apartamento = 0;
            pessoa.fk_acesso = 0;
            pessoa.fk_veiculo = 0;
        }

        private void buttonDeletCad_Click(object sender, EventArgs e)
        {
            
            deletarPessoa(pessoa, acesso);
            lislistaCadastro();
            limparTextBoxes(pessoa);
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            limparTextBoxes(pessoa);
        }        

        private void textBoxRgCad_Leave(object sender, EventArgs e)
        {
            if (textBoxRgCad.Text == "  .   .   -  ")
            {
                textBoxRgCad.ForeColor = Color.Gray;
                textBoxRgCad.Text = "  .   .   -  ";
            }
        }

        private void textBoxRgCad_Click(object sender, EventArgs e)
        {
            //INFORMAÇOES PARA O USUARIO 
            if (textBoxRgCad.Text == "  .   .   -  ")
            {
                textBoxRgCad.Clear();
            }
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            if (textBoxCadEmail.Text == "")
            {
                textBoxCadEmail.ForeColor = Color.Gray;
                textBoxCadEmail.Text = "email@email.com.br";
            }
        }

        private void textBoxEmail_Click(object sender, EventArgs e)
        {
            //INFORMAÇOES PARA O USUARIO 
            if (textBoxCadEmail.Text == "email@email.com.br")
            {
                textBoxCadEmail.Clear();
            }
        }

        private void textBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxCadEmail.ForeColor = Color.Black;
        }

        private void checkBoxVisitante_CheckedChanged(object sender, EventArgs e)
        {
            lislistaCadastro();
        }
    
        private void checkBoxInOut_Click(object sender, EventArgs e)
        {
            lislistaCadastro(); 
        }

        private void checkBoxMorador_CheckedChanged(object sender, EventArgs e)
        {
            lislistaCadastro();
        }

        private void checkBoxColaborador_CheckedChanged(object sender, EventArgs e)
        {
            lislistaCadastro();
        }       

        private void checkBoxGeral_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxMorador.Checked = false;
            checkBoxColaborador.Checked = false;
            checkBoxVisitante.Checked = false;
            lislistaCadastro();
        }



        private void textBoxPesquisaNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBoxInOut.Checked == false)
            {
                pesquisacad(pessoa);
            }
           
        }

        private void textBoxPesquisaNome_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxInOut.Checked == false)
            {
                pesquisacad(pessoa);
            }

        }

        private void maskedTextBoxEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaInOut(inout);
            }  
        }

        private void maskedTextBoxSaida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaInOut(inout);
            }
        }
    }
}
