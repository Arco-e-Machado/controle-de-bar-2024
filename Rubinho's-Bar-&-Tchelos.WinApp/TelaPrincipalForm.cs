using Rubinho_s_Bar___Tchelos.Dominio.M�duloMesa;
using Rubinho_s_Bar___Tchelos.Dominio.M�duloPedido;
using Rubinho_s_Bar___Tchelos.Dominio.M�duloProduto;
using Rubinho_s_Bar___Tchelos.Dominio.M�duloPessoas;
using Rubinho_s_Bar___Tchelos.Infra.Orm.M�duloMesa;
using Rubinho_s_Bar___Tchelos.Infra.Orm.M�duloPedido;
using Rubinho_s_Bar___Tchelos.Infra.Orm.M�duloProduto;
using Rubinho_s_Bar___Tchelos.Infra.Orm.M�duloPessoas;
using Rubinho_s_Bar___Tchelos.Infra.Orm.M�duloCompartilhado;
using Rubinho_s_Bar___Tchelos.WinApp.M�duloMesa;
using Rubinho_s_Bar___Tchelos.WinApp.M�duloPedido;
using Rubinho_s_Bar___Tchelos.WinApp.M�duloPessoas;
using Rubinho_s_Bar___Tchelos.WinApp.M�duloProduto;
using Rubinho_s_Bar___Tchelos.WinApp.M�duloCompartilhado;

namespace Rubinho_s_Bar___Tchelos.WinApp
{

    public partial class TelaPrincipalForm : Form
    {

        ControladorBase controlador;

        IRepositorioPessoas repositorioPessoas;
        IRepositorioMesa repositorioMesa;
        IRepositorioComanda repositorioComanda;
        IRepositorioProduto repositorioProduto;

        public static TelaPrincipalForm Instancia { get; private set; }

        public TelaPrincipalForm()
        {
            InitializeComponent();

            toolStripTipo.Text = string.Empty;

            Instancia = this;

            BotecoDbContext dbContext = new BotecoDbContext();

            repositorioMesa = new RepositorioMesaEmOrm(dbContext);
            repositorioProduto = new RepositorioProdutoEmOrm(dbContext);
            repositorioComanda = new RepositorioPedidoEmOrm(dbContext);
            repositorioPessoas = new RepositorioPessoasEmOrm(dbContext);

        }

        public void AtualizarRodape(string texto)
        {
            statusLabelPrincipal.Text = texto;
        }

        private void ConfigurarTelaPrincipal(ControladorBase controladorSelecionado)
        {
            toolStripTipo.Text = "Gerenciamento de " + controladorSelecionado.TipoCadastro;

            ConfigurarToolBox(controladorSelecionado);
            ConfigurarListagem(controladorSelecionado);
        }

        private void ConfigurarToolBox(ControladorBase controladorSelecionado)
        {
            btnAdicionar.Enabled = controladorSelecionado is ControladorBase;
            btnEditar.Enabled = controladorSelecionado is IControladorEditavel;
            btnExcluir.Enabled = controladorSelecionado is ControladorBase;

            btnFecharContas.Enabled = controladorSelecionado is IControladorConcluir;
            btnGerarExtrato.Enabled = controladorSelecionado is IControladorVisualizarExtratos;

            if (controladorSelecionado is IControladorEditavel)
                btnEditar.Enabled = true;

            ConfigurarToolTips(controladorSelecionado);
        }

        private void ConfigurarToolTips(ControladorBase controladorSelecionado)
        {
            btnAdicionar.ToolTipText = controladorSelecionado.ToolTipAdicionar;
            btnExcluir.ToolTipText = controladorSelecionado.ToolTipExcluir;

        }

        private void ConfigurarListagem(ControladorBase controladorSelecionado)
        {
            UserControl listagem = controladorSelecionado.ObterListagem();
            listagem.Dock = DockStyle.Fill;

            pnlRegistros.Controls.Clear();
            pnlRegistros.Controls.Add(listagem);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            controlador.Adicionar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (controlador is IControladorEditavel controladorEditavel)
                controladorEditavel.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void funcion�riosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorPessoas(repositorioPessoas);

            ConfigurarTelaPrincipal(controlador);
        }


        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorComanda(repositorioPessoas, repositorioMesa, repositorioComanda, repositorioProduto);

            ConfigurarTelaPrincipal(controlador);
        }

        private void mesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorMesa(repositorioMesa);

            ConfigurarTelaPrincipal(controlador);

            controlador.CarregarRegistros();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorProduto(repositorioProduto);

            ConfigurarTelaPrincipal(controlador);
        }

        private void btnPagamentos_Click(object sender, EventArgs e)
        {

        }

        private void btnFecharContas_Click(object sender, EventArgs e)
        {
            if (controlador is IControladorConcluir controladorConcluir)
                controladorConcluir.Concluir();
        }

        private void btnGerarExtrato_Click(object sender, EventArgs e)
        {
            if (controlador is IControladorVisualizarExtratos controladorExtratos)
                controladorExtratos.VisualizarExtratos();
        }
    }
}
