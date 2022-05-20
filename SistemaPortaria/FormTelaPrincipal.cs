using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaPortaria.bill;
using SistemaPortaria.dao;
using SistemaPortaria.GetSet;
using SistemaPortaria.Modell;

namespace SistemaPortaria
{
    public partial class FormTelaPrincipal : Form
    {
        public FormTelaPrincipal()
        {
            InitializeComponent();
            pictureBoxFaixaTopo.Parent = pictureBoxFundo;
            pictureBoxFaixaTopo.BackColor = Color.Transparent;
        }


        Bill classCasBll = new Bill();
        Pessoa pessoa = new Pessoa();
        Acesso acesso = new Acesso();
        Apartamento apartamento = new Apartamento();
        Veiculo veiculo = new Veiculo();
        InOut inout = new InOut();
        FormCadastro formCadastro = new FormCadastro();
        Register register = new Register();
        CadCrud cadCrud = new CadCrud();

        //ALTOCOMPLETE 
        void completarRG()
        {

            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarRG().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarRG().Rows[i]["RG"].ToString());
            }
            textBoxRgVis.AutoCompleteCustomSource = lista;
        }

        //ALTOCOMPLETE 
        void completarCor()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarCor().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarCor().Rows[i]["COR"].ToString());
            }
            textBoxCorVis.AutoCompleteCustomSource = lista;
        }

        void completarModelo()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarModelo().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarModelo().Rows[i]["MODELO"].ToString());
            }
            textBoxModeloVis.AutoCompleteCustomSource = lista;
        }

        void completarPlaca()
        {
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            for (int i = 0; i < classCasBll.completarModelo().Rows.Count; i++)
            {
                lista.Add(classCasBll.completarPlaca().Rows[i]["PLACA"].ToString());
            }
            textBoxPlacaVsi.AutoCompleteCustomSource = lista;
        }


        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pesquisarVisitante(Pessoa pessoa, Apartamento apartamento, Veiculo veiculo)
        {
            pessoa.rg = textBoxRgVis.Text;

            dataGridViewVis.DataSource = classCasBll.pesquisaEntradaVisitante(pessoa);
        }

        private void telaDeCadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formCadastro.BringToFront();
            formCadastro.TopMost = true;
            formCadastro.Show();
        }
        private void telaPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formCadastro.Visible = false;
        }

        private void pesquisaPessoa(Pessoa pessoa, Apartamento apartamento, Veiculo veiculo)
        {  
            classCasBll.ramalget = textBoxPesquisaApartamento.Text;
            classCasBll.placaget = textBoxPesquisaPlaca.Text;            

            classCasBll.pesquisaPessoa(pessoa, apartamento, veiculo);

            if (pessoa.rg != "")
            {
                textBoxNomeTelaPrincipal.Text = pessoa.nome;
                pictureBoxTelaPrinsipal.Image = pessoa.Img_fot;

                textBoxRgTelaPrincipal.Text = pessoa.rg;
                textBoxGSM.Text = pessoa.cel;
                textBoxNumeroTelaPrincipal.Text = apartamento.numero;
                textBoxBlocoTelaPrincipal.Text = apartamento.bloco;
                textBoxAndarTelaPrincipal.Text = apartamento.andar;
                textBoxRamalTeleTelaPrincipal.Text = apartamento.ramal;
                textBoxModeloTelaPrincipal.Text = veiculo.modelo;
                textBoxCorTelaPrincipal.Text = veiculo.cor;
                textBoxPlacaTelaPrincipal.Text = veiculo.placa;
                labelTipo.Text = pessoa.tipo;

                if (labelTipo.Text != "Aviso")
                {
                    if (labelTipo.Text == "VISITANTE")
                    {
                        labelTipo.Visible = true;
                        labelTipo.ForeColor = Color.Red;


                        textBoxGSM.Text = "";
                        textBoxNomeVis.Text = pessoa.nome;
                        textBoxPlacaVsi.Text = veiculo.placa;
                        textBoxCorVis.Text = veiculo.cor;
                        textBoxModeloVis.Text = veiculo.modelo;
                        textBoxRgVis.Text = pessoa.rg;
                    }
                    else
                    {
                        labelTipo.Visible = true;
                        labelTipo.ForeColor = Color.MediumAquamarine;
                    }
                }
            }
            else
            {
                if(textBoxPesquisaPlaca.Text != "")
                {
                    MessageBox.Show("Veiculo nao cadastrado. \r\n\r\nSera registrado como VISITANTE \r\nFavor concluir o preencher \r\n\r\nRG,NOME,MODELO,COR \r\n\r\nE APARTAMENTO", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBoxPlacaVsi.Text = textBoxPesquisaPlaca.Text;
                    textBoxNomeVis.Text = "";
                    textBoxRgVis.Text = "";
                    textBoxCorVis.Text = "";
                    textBoxModeloVis.Text = "";
                    
                }
                else
                {
                    MessageBox.Show("Dados nao encontrados. \r\n\r\nFavor veificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            
        }  
        
        private void listaVisitanteApartamento(Pessoa pessoa, Apartamento apartamento, Veiculo veiculo)
        {
            classCasBll.ramalget = textBoxRamalTeleTelaPrincipal.Text;

            classCasBll.listaVisitanteApartamento(pessoa, apartamento, veiculo);

            textBoxNomeTelaPrincipal.Text = pessoa.nome;
            pictureBoxTelaPrinsipal.Image = pessoa.Img_fot;

            textBoxRgTelaPrincipal.Text = pessoa.rg;
            textBoxNumeroTelaPrincipal.Text = apartamento.numero;
            textBoxBlocoTelaPrincipal.Text = apartamento.bloco;
            textBoxAndarTelaPrincipal.Text = apartamento.andar;
            textBoxRamalTeleTelaPrincipal.Text = apartamento.ramal;
            textBoxModeloTelaPrincipal.Text = veiculo.modelo;
            textBoxCorTelaPrincipal.Text = veiculo.cor;
            textBoxPlacaTelaPrincipal.Text = veiculo.placa;
            labelTipo.Text = pessoa.tipo;
            if (labelTipo.Text != "Aviso")
            {
                if (labelTipo.Text == "VISITANTE")
                {

                    textBoxGSM.Text = "";
                    labelTipo.Visible = true;
                    labelTipo.ForeColor = Color.Red;
                }
                else
                {
                    labelTipo.Visible = true;
                    labelTipo.ForeColor = Color.MediumAquamarine;
                }
            }

        }

        private void textBoxPesquisaPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {                
                if (textBoxPesquisaPlaca.MaxLength == 7)
                {
                    textBoxPesquisaPlaca.SelectAll();
                    pesquisaPessoa(pessoa, apartamento, veiculo);

                    pesquisaVisitaEmAP(apartamento);
                }
            }                
            else
            {
                textBoxPesquisaApartamento.Clear();
                limpaPesquisa(pessoa, veiculo, apartamento);

                listaVisitante(apartamento);
            }
        }

        private void textBoxPesquisaApartamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            //NAO PERMITE QUE DIGITE LETRAS
            if (!char.IsDigit(e.KeyChar) & (e.KeyChar != 13) & (e.KeyChar != 46) & (e.KeyChar != 8))
            {
                e.Handled = true;
            }
            else
            {

            }           
            
            if (e.KeyChar == 13)
            {
              
                textBoxPesquisaApartamento.SelectAll();
                pesquisaPessoa(pessoa, apartamento, veiculo);

                pesquisaVisitaEmAP(apartamento);
                
            }
            else
            {
                textBoxPesquisaPlaca.Clear();
                limpaPesquisa(pessoa, veiculo, apartamento);

                listaVisitante(apartamento);
            }
        }

        private void pesquisaVisitaEmAP(Apartamento apartamento)
        {
            apartamento.ramal = textBoxRamalTeleTelaPrincipal.Text;

            dataGridViewVis.DataSource = classCasBll.pesquisaVisitaEmAP(apartamento);          
        }

        private void limpaPesquisa(Pessoa pessoa, Veiculo veiculo, Apartamento apartamento)
        {           
            textBoxNomeTelaPrincipal.Clear();
            pictureBoxTelaPrinsipal.Image = null;
            textBoxRgTelaPrincipal.Clear();
            textBoxGSM.Clear();
            textBoxNumeroTelaPrincipal.Clear();
            textBoxBlocoTelaPrincipal.Clear();
            textBoxAndarTelaPrincipal.Clear();
            textBoxRamalTeleTelaPrincipal.Clear();
            textBoxModeloTelaPrincipal.Clear();
            textBoxCorTelaPrincipal.Clear();
            textBoxPlacaTelaPrincipal.Clear();
            labelTipo.Text = "Aviso";
            labelTipo.Visible = false;
            pessoa.Img_fot = null;
            pessoa.nome = "";
            pessoa.rg = "";
            pessoa.tipo = "";
            pessoa.rg = "";
            pessoa.email = "";
            pessoa.cel = "";
            apartamento.andar = "";
            apartamento.bloco = "";
            apartamento.numero = "";
            apartamento.ramal = "";
            veiculo.modelo = "";
            veiculo.cor = "";
            veiculo.placa = "";
        }


        private void salvarVisitnte(Pessoa pessoa,InOut inout)
        {
            pessoa.nome = textBoxNomeVis.Text;
            pessoa.rg = textBoxRgVis.Text;
            pessoa.foto = "System.Byte[]";

            pessoa.tipo = "VISITANTE";
            
            DateTime entrada = DateTime.Now;
            DateTime saida = DateTime.Now;

            DateTime Data = DateTime.Now;

            inout.entrada = Data.ToString(entrada.ToString("dd/MM/yyy HH:mm:ss"));
            inout.saida = Data.ToString(saida.ToString("dd/MM/yyy HH:mm:ss"));

            classCasBll.salvarVisitnte(pessoa, acesso, inout, apartamento, veiculo);
        }

        private void salvarVeiculoVisitante(Veiculo veiculo, Pessoa pessoa)
        {
            veiculo.placa = textBoxPlacaVsi.Text;
            veiculo.modelo = textBoxModeloVis.Text;
            veiculo.cor = textBoxCorVis.Text;

            classCasBll.salvarVeiculoVisitante(veiculo, pessoa);
        }

        private void salvarApartamentoVisitante(Apartamento apartamento, Pessoa pessoa)
        {
            apartamento.numero = textBoxNumeroTelaPrincipal.Text;
            apartamento.andar = textBoxAndarTelaPrincipal.Text;
            apartamento.bloco = textBoxBlocoTelaPrincipal.Text;
            apartamento.ramal = textBoxRamalTeleTelaPrincipal.Text;

            classCasBll.salvarApartamentoVisitante(apartamento, pessoa);
        }
        

        public void listaVisitante(Apartamento apartamento)
        {
            dataGridViewVis.DataSource = classCasBll.listaVisitante(apartamento);

            //dataGridViewVis.Sort(dataGridViewVis.Columns["id"], ListSortDirection.Descending);
            //ESCONDER CAMPO
            dataGridViewVis.Columns["ID"].Visible = false;
            dataGridViewVis.Columns["RG"].Visible = false;
            dataGridViewVis.Columns["FOTO"].Visible = false;        
            dataGridViewVis.Columns["ANDAR"].Visible = false;
            dataGridViewVis.Columns["RAMAL"].Visible = false;
            dataGridViewVis.Columns["STATUS"].Visible = false;
            dataGridViewVis.Columns["LOGIN"].Visible = false;
            dataGridViewVis.Columns["SENHA"].Visible = false;
            dataGridViewVis.Columns["USUARIO"].Visible = false;
            dataGridViewVis.Columns["TIPO"].Visible = false;
            dataGridViewVis.Columns["celular"].Visible = false;
            dataGridViewVis.Columns["email"].Visible = false;



            //DEFINIR TAMANHO DA COLUNA 
            //dataGridViewVisitante.Columns["id"].Width = 50;
            dataGridViewVis.Columns["NOME"].Width = 200;            
            dataGridViewVis.Columns["BLOCO"].Width = 35;
            dataGridViewVis.Columns["BLOCO"].HeaderText = "BL";
            dataGridViewVis.Columns["AP"].Width = 35;
            dataGridViewVis.Columns["PLACA"].Width = 70;
            dataGridViewVis.Columns["MODELO"].Width = 100;
            dataGridViewVis.Columns["COR"].Width = 90;           


            //DESIGNE DO TITULO
            dataGridViewVis.EnableHeadersVisualStyles = false;
            dataGridViewVis.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewVis.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 63, 130);
            dataGridViewVis.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //PARAMETROS PARA DESIGN DA TABELA           
            dataGridViewVis.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewVis.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);//COR DOS CAMPOS 
            //dataGridViewTabChamadoTP.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;//LINHAS VERTICAS
            dataGridViewVis.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 231, 145);//COR DE SELEÇAO 
            dataGridViewVis.DefaultCellStyle.SelectionForeColor = Color.Black;// COR DO CAMPO AO SELECIONAR
            dataGridViewVis.BackgroundColor = Color.White;//COR DA TABELA VASIA
        }

        private void dataGridViewVis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewVisClik(apartamento);
        }

        private void dataGridViewVisClik(Apartamento apartamento)
        {
            if (dataGridViewVis.CurrentRow != null)
            {
                textBoxNomeVis.Text = dataGridViewVis.CurrentRow.Cells[1].Value.ToString();
                textBoxRgVis.Text = dataGridViewVis.CurrentRow.Cells[2].Value.ToString();

                textBoxNumeroTelaPrincipal.Text = dataGridViewVis.CurrentRow.Cells[4].Value.ToString();
                textBoxBlocoTelaPrincipal.Text = dataGridViewVis.CurrentRow.Cells[5].Value.ToString();
                textBoxAndarTelaPrincipal.Text = dataGridViewVis.CurrentRow.Cells[6].Value.ToString();
                textBoxRamalTeleTelaPrincipal.Text = dataGridViewVis.CurrentRow.Cells[7].Value.ToString();
                textBoxPlacaVsi.Text = dataGridViewVis.CurrentRow.Cells[8].Value.ToString();
                textBoxModeloVis.Text = dataGridViewVis.CurrentRow.Cells[9].Value.ToString();
                textBoxCorVis.Text = dataGridViewVis.CurrentRow.Cells[10].Value.ToString();

                listaVisitanteApartamento(pessoa, apartamento, veiculo);
            }
        }

        private void buttonEntrada_Click(object sender, EventArgs e)
        {
            classCasBll.telacad = true;

            if (textBoxNomeVis.Text == "")
            {
                labelNomeVis.ForeColor = Color.Red;
                textBoxNomeVis.Focus();
            }
            else if (textBoxRgVis.Text == "")
            {
                labelRgVis.ForeColor = Color.Red;
                textBoxRgVis.Focus();
            }
            else if (textBoxRamalTeleTelaPrincipal.Text == "")
            {
                labelPesquisaApartamento.ForeColor = Color.Red;
                textBoxPesquisaApartamento.Focus();
                new BalloonTip("Registro", "Favor informar o BLOCO e APARTAMENTO \r\nExemplo:12345", textBoxPesquisaApartamento, BalloonTip.ICON.INFO, 5000);
            }
            else if (textBoxAndarTelaPrincipal.Text == "")
            {
                labelPesquisaApartamento.ForeColor = Color.Red;
                textBoxPesquisaApartamento.Focus();
            }
            else if (textBoxBlocoTelaPrincipal.Text == "")
            {
                labelPesquisaApartamento.ForeColor = Color.Red;
                textBoxPesquisaApartamento.Focus();
            }
            else if (textBoxNumeroTelaPrincipal.Text == "")
            {
                labelPesquisaApartamento.ForeColor = Color.Red;
                textBoxPesquisaApartamento.Focus();
            }
            else
            {

                textBoxGSM.Text = "";
                pessoa.cel = "";
                pessoa.ativo = "ATIVO";
                salvarApartamentoVisitante(apartamento, pessoa);
                salvarVeiculoVisitante(veiculo, pessoa);
                salvarVisitnte(pessoa,inout);
                
                listaVisitante(apartamento);
                textBoxNomeVis.Text = "";
                textBoxPlacaVsi.Text = "";
                textBoxCorVis.Text = "";
                textBoxModeloVis.Text = "";
                textBoxRgVis.Text = "";
                limpaPesquisa(pessoa, veiculo, apartamento);
            }
        }

        private void buttonSaida_Click_1(object sender, EventArgs e)
        {
            classCasBll.telacad = true;
            if (textBoxNomeVis.Text == "")
            {
                labelNomeVis.ForeColor = Color.Red;
                textBoxNomeVis.Focus();
            }
            else if (textBoxRgVis.Text == "")
            {
                labelRgVis.ForeColor = Color.Red;
                textBoxRgVis.Focus();
            }
            else if (textBoxRamalTeleTelaPrincipal.Text == "")
            {
                labelPesquisaApartamento.ForeColor = Color.Red;
                textBoxPesquisaApartamento.Focus();
            }
            else if (textBoxAndarTelaPrincipal.Text == "")
            {
                labelPesquisaApartamento.ForeColor = Color.Red;
                textBoxPesquisaApartamento.Focus();
            }
            else if (textBoxBlocoTelaPrincipal.Text == "")
            {
                labelPesquisaApartamento.ForeColor = Color.Red;
                textBoxPesquisaApartamento.Focus();
            }
            else if (textBoxNumeroTelaPrincipal.Text == "")
            {
                labelPesquisaApartamento.ForeColor = Color.Red;
                textBoxPesquisaApartamento.Focus();
            }
            else
            {

                textBoxGSM.Text = "";
                pessoa.cel = "";
                pessoa.ativo = "INATIVO";
                salvarApartamentoVisitante(apartamento, pessoa);
                salvarVeiculoVisitante(veiculo, pessoa);
                salvarVisitnte(pessoa,inout);

                listaVisitante(apartamento);
                textBoxNomeVis.Text = "";
                textBoxPlacaVsi.Text = "";
                textBoxCorVis.Text = "";
                textBoxModeloVis.Text = "";
                textBoxRgVis.Text = "";
                limpaPesquisa(pessoa, veiculo, apartamento);
            }            
        }

        private void FormTelaPrincipal_Load(object sender, EventArgs e)
        {            
            //completarPlaca();
            //completarCor();
            //completarModelo();
            //completarRG();
            dtLisenca();
            listaVisitante(apartamento);            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            classCasBll.diaLisencaFim(register);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            labeldata.Text = DateTime.Now.ToShortDateString();
            labelhora.Text = DateTime.Now.ToLongTimeString();
        }

        private void dtLisenca()
        {
            classCasBll.dtLisenca(register);

            DateTime dtfim = DateTime.Parse(register.dt_fim);
            DateTime dtatual = DateTime.Parse(register.dt_uso);

            TimeSpan dtuso;

            dtuso = DateTime.Parse(register.dt_fim) - DateTime.Parse(register.dt_uso);

            int dias = dtuso.Days;

            toolStripStatusLabelinicio.Text = DateTime.Now.ToString(register.dt_inicio);
            toolStripStatusLabelfim.Text = DateTime.Now.ToString(register.dt_fim);

            toolStripStatusLabeldias.Text = dias.ToString();
        }

        private void textBoxPesquisaPlaca_TextChanged(object sender, EventArgs e)
        {
            textBoxPesquisaPlaca.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBoxNomeVis_TextChanged(object sender, EventArgs e)
        {
            textBoxNomeVis.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBoxPlacaVsi_TextChanged(object sender, EventArgs e)
        {
            textBoxPlacaVsi.CharacterCasing = CharacterCasing.Upper;
        }       

        private void textBoxModeloVis_TextChanged(object sender, EventArgs e)
        {
            textBoxModeloVis.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBoxRgVis_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
               

            }
            else
            {
                pesquisarVisitante(pessoa, apartamento, veiculo);
            }
        }       


        private void textBoxCorVis_TextChanged(object sender, EventArgs e)
        {
            textBoxCorVis.CharacterCasing = CharacterCasing.Upper;
        }      

    }
}
