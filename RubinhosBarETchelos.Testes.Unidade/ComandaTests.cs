using Rubinho_s_Bar___Tchelos.Dominio.M�duloMesa;
using Rubinho_s_Bar___Tchelos.Dominio.M�duloPedido;
using Rubinho_s_Bar___Tchelos.Dominio.M�duloPedido.Pedidos;
using Rubinho_s_Bar___Tchelos.Dominio.M�duloPessoas;

namespace RubinhosBarETchelos.Testes.Unidade
{
    [TestClass] // Atributos
    public class ComandaTests
    {
        [TestMethod]
        [TestCategory("Testes Unitarios para comanda")]
        public void Deve_Validar_Conta_Corretamente()
        {
            // AAA = Triple A

            //arrange(prepara��o do teste)

            Comanda comandaInvalida = new(null, 0, null, null);

            List<string> errosEsperados =
                [
                "N�o � possivel iniciar uma comanda sem definir um gar�om",
                "N�o � possivel iniciar uma comanda sem definir uma mesa",
                "Deve haver ao menos um produto no pedido para iniciar a comanda"
                ];
            // Act (A��o do teste)
            List<string> erros = comandaInvalida.Validar();

            // Asserts (asser��o do teste)
            CollectionAssert.AreEqual(errosEsperados, erros);
        }

        [TestMethod]
        public void Deve_Fechar_Comanda_Corretamente()
        {
            // Arrange
            List<Pedido> pedidos = new();
            Mesa mesa = new(1);
            Gar�om gar�om = new("Tchelo", "156.156.155-98", 0);


            Comanda novaComanda = new(gar�om, EnumStatusPagamento.Fechada, mesa, pedidos);

            // Act
            novaComanda.Concluir();

            // Assert 
            Assert.IsTrue(novaComanda.Status == EnumStatusPagamento.Fechada);
            Assert.IsFalse(novaComanda.Mesa.Status);

        }

    }
}