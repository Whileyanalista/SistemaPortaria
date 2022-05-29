using SistemaPortaria.dao;
using SistemaPortaria.GetSet;
using SistemaPortaria.Modell;
using System;
using System.Data;

namespace SistemaPortaria.bill
{
    public class Bill
    {
        CadCrud cadCrud = new CadCrud();        
        Ativacao ativacao = new Ativacao();
        Exceptions exceptions = new Exceptions();

        DataTable dataTable = new DataTable();
        Gsm gsm = new Gsm();

        
      

        
        public void SalvarPessoa(Pessoa pessoa, Acesso acesso, Apartamento apartamento, Veiculo veiculo)
        {
            try
            {
                cadCrud.salvarPessoa(pessoa, apartamento, veiculo, acesso);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Salvar Pessoa");
            }
        }

        public void inout(InOut inout, Pessoa pessoa)
        {
            try
            {
                cadCrud.inout(pessoa, inout);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Registro INOUT");
            }
        }

        public void logar(Acesso acesso, Pessoa pessoa)
        {
            try
            {
                cadCrud.logar(acesso, pessoa);

                if (cadCrud.acessaradm == "SIM")
                {
                    acessaradm = "SIM";
                }
                else if(cadCrud.acessaradm == "NÃO")
                {
                    acessaradm = "NÃO";
                }
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004 ,erro.ToString(),"Logar");
            }
        }

        public string acessaradm;
        public string cadlogar;

        public void CadLogar(Pessoa pessoa)
        {
            try
            {
                cadCrud.CadLogaraDM(pessoa);

                if (cadCrud.cadlogar == "Cadastroadm")
                {
                    cadlogar = cadCrud.cadlogar;                   
                }
                else
                {
                    cadCrud.CadLogar(pessoa);

                    cadlogar = cadCrud.cadlogar;
                }
                                                         
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Logar");
            }
        }

        public void salvarAcesso(Acesso acesso, Pessoa pessoa)
        {
            try
            {
                cadCrud.salvarAcesso(acesso, pessoa);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString() , "Salvar Acesso");
            }
        }
         
        public void salvarVeiculo(Veiculo veiculo, Pessoa pessoa)
        {
            try
            {
                cadCrud.salvarVeiculo(veiculo, pessoa);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Salvar Veiculo");
            }
        }

        public void salvarApartamento(Apartamento apartamento, Pessoa pessoa)
        {
            try
            {
                cadCrud.salvarApartamento(apartamento,pessoa);
            }
            catch (Exception erro )
            {
                
                exceptions.ExceptionsDB(1004, erro.ToString(), "Salvar Apartamento");
            }
        }

        public void deletarPessoa(Pessoa pessoa, Acesso acesso)
        {
            try
            {
                cadCrud.deletarPessoa(pessoa, acesso);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Deletar dados");
                throw;
            }
        }
        
        //_______________________Ativação________________________
        public void retornoEmail(Register register )
        {
            try
            {
                ativacao.retornoEmail(register);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Retorno de Email");
            }
        }

        public void diaLisencaFim(Register register)
        {
            try
            {
                ativacao.diaLisencaFim(register);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Fim da Lisença");
            }
        }

        public void dtLisenca(Register register)
        {
            try
            {
                ativacao.diaLisenca(register);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Data da Lisença");
            }
        }
        
        public void salvarAtivacao(Register register)
        {
            try
            {
                ativacao.salvarAtivacao(register);                
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Salvar Ativaçã");

            }
        }

        public DataTable listaCadastro()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.listaCadastro();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(),"Lista de Cadastro");
                throw;
            }
        }

        public DataTable listaCadastro(Pessoa pessoa)
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.listaCadastro(pessoa);

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Lista de Cadastro");
                throw;
            }
        }

        public DataTable listaInout()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.listaInout();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Lista de INOUT");
                throw;
            }
        }

        //_____________________PESQUISA TELA PRINCIPAL__________________
        public string placaget = "";
        public string ramalget = "";
        public bool telacad;

        public void salvarVisitnte(Pessoa pessoa, Acesso acesso, InOut inout, Apartamento apartamento, Veiculo veiculo)
        {
            try
            {
                cadCrud.salvarPessoa(pessoa, apartamento, veiculo, acesso);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Salvar Visitante");
            }
        }

        public void salvarApartamentoVisitante(Apartamento apartamento, Pessoa pessoa)
        {
            try
            {
                cadCrud.salvarApartamento(apartamento, pessoa);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Salvar Apartamento de Visitante");
            }
        }

        public void salvarVeiculoVisitante(Veiculo veiculo, Pessoa pessoa)
        {
            try
            {
                cadCrud.salvarVeiculo(veiculo, pessoa);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Salvar Veiculo de visitante");
            }
        }
        
        public void pesquisaPessoa(Pessoa pessoa, Apartamento apartamento, Veiculo veiculo)
        {
            try
            {                
                cadCrud.telacad = telacad;
                cadCrud.ramalget = ramalget;
                cadCrud.placaget = placaget;
                cadCrud.pesquisaPessoa(pessoa, apartamento, veiculo);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Pesquisar Morador");
            }

        }        

        public void listaVisitanteApartamento(Pessoa pessoa, Apartamento apartamento, Veiculo veiculo)
        {
            try
            {                
                cadCrud.ramalget = ramalget;
                cadCrud.pesquisaPessoa(pessoa, apartamento, veiculo);
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Listar visitante Apartamento");
            }
        }

        public DataTable listaVisitante(Apartamento apartamento)
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.listaVisitante();

                return dataTable; 
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Listar Visitante");
                throw;
            }
        }

        public DataTable pesquisaVisitaEmAP(Apartamento apartamento)
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.pesquisaVisitaEmAP(apartamento);

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Pesquisar Visitante");
                throw;
            }
        }

        public DataTable pesquisaEntradaVisitante(Pessoa pessoa)
        {
            try
            {
                dataTable = cadCrud.pesquisaEntradaVisitante(pessoa);

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Pesquisar Entrada de Visitante");
                throw;
            }
        }

        public DataTable pesquisacad(Pessoa pessoa)
        {
            try
            {
                dataTable = cadCrud.pesquisacad(pessoa);

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Pesquisar pessoa");
                throw;
            }
        }

        public DataTable pesquisaInOut(InOut inout)
        {
            try
            {   
                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Pesquisar IINOUT");
                throw;
            }
        }

        public DataTable completarApartamento()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.completarApartamento();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Completar Campo de Apartamento");
                throw;
            }
        }

        public DataTable completarAndar()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.completarAndar();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Completar campo de Andar");
                throw;
            }
        }

        public DataTable completarCor()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.completarCor();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Completar Campo de Cor");
                throw;
            }
        }

        public DataTable completarPlaca()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.completarPlaca();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Completar Campo de PLACA");
                throw;
            }
        }

        public DataTable completarModelo()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.completarModelo();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Completar o campo Modelo");
                throw;
            }
        }

        public DataTable completarRG()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.completarRG();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Completar campo RG");
                throw;
            }
        }

        public DataTable completarRamal()
        {
            try
            {
                DataTable dataTable = new DataTable();

                dataTable = cadCrud.completarRamal();

                return dataTable;
            }
            catch (Exception erro)
            {
                exceptions.ExceptionsDB(1004, erro.ToString(), "Competar campo Ramal");
                throw;
            }
        }

    }
}
