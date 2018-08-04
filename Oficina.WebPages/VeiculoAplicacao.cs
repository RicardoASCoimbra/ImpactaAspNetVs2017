using Oficina.Dominio;
using Oficina.Repositorios.SistemaDeArquivos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Oficina.WebPages
{
    public class VeiculoAplicacao
    {
        private CorRepositorio _corRepositorio = new CorRepositorio();
        private ModeloRepositorio _modeloRepositorio = new ModeloRepositorio();
        private MarcaRepositorio _marcaRepositorio = new MarcaRepositorio();
        private VeiculoRepositorio _veiculoRepositorio = new VeiculoRepositorio();

        #region ViewModels

            public List<Marca> Marcas { get; private set; }
            public string MarcaSelecionada { get; private set; }
            public List<Modelo> Modelos { get; private set; } = new List<Modelo>();
            public List<Cor> Cores { get; private set; }
            public List<Combustivel> Combustiveis { get; private set; }
            public List<Cambio> Cambios { get; private set; }

        #endregion


        public VeiculoAplicacao()
        {
            PopularControles();
        }

        private void PopularControles()
        {
            Marcas = _marcaRepositorio.Selecionar();
            MarcaSelecionada = HttpContext.Current.Request.QueryString["marcaId"];
            if (!string.IsNullOrEmpty(MarcaSelecionada))
            {
                Modelos = _modeloRepositorio.SelecionarPorMarca(Convert.ToInt32(MarcaSelecionada));
            }

            Cores = _corRepositorio.Selecionar();
            Combustiveis = Enum.GetValues(typeof(Combustivel)).Cast<Combustivel>().ToList();
            Cambios = Enum.GetValues(typeof(Cambio)).Cast<Cambio>().ToList();


        }

        public void Inserir()
        {
            try
            {
                var veiculo = new VeiculoPasseio();
                var formulario = HttpContext.Current.Request.Form;

                veiculo.Ano = Convert.ToInt32(formulario["ano"]);
                veiculo.Cambio = (Cambio)Convert.ToInt32(formulario["cambio"]);
                veiculo.Combustivel = (Combustivel)Convert.ToInt32(formulario["combustivel"]);
                veiculo.Cor = _corRepositorio.Selecionar(Convert.ToInt32(formulario["cor"]));
                veiculo.Modelo = _modeloRepositorio.Selecionar(Convert.ToInt32(formulario["modelo"]));
                veiculo.Observacao = formulario["observacao"];
                veiculo.Placa = formulario["placa"]/*.ToUpper()*/;
                veiculo.Carroceria = TipoCarroceria.Hatch;
                

                _veiculoRepositorio.Inserir(veiculo);
            }

            catch (FileNotFoundException ex)
            {
                HttpContext.Current.Items.Add("MensagemErro", $"O arquivo {ex.FileName} não foi encontrado");
                
            }
            catch (UnauthorizedAccessException )
            {
                HttpContext.Current.Items.Add("MensagemErro", "Arquivo sem permissão de gravação.");
                
            }
            catch (DirectoryNotFoundException )
            {
                HttpContext.Current.Items.Add("MensagemErro", "O caminho do arquivo não foi encontrada.");
                
            }
            catch (Exception )
            {
                HttpContext.Current.Items.Add("MensagemErro", "Ooops! Ocorreu um erro e sua ação não foi realizada.");

                //logar o objeto de exception ex.
                //throw;
            }
            finally
            {
                // finally: chamado sempre, independente de erro ou sucesso. É executado mesmo se há um return no código.
            }
        }


    }
}