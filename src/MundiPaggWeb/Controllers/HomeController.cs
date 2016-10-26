using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using Microsoft.AspNetCore.Mvc;
using MundiPaggAsync;

namespace MundiPaggWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Pay()
        {
            StringBuilder console = new StringBuilder();

            // Cria a transação.
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = 10000,
                CreditCard = new CreditCard()
                {
                    CreditCardBrand = CreditCardBrandEnum.Visa,
                    CreditCardNumber = "4111111111111111",
                    ExpMonth = 10,
                    ExpYear = 22,
                    HolderName = "LUKE SKYWALKER",
                    SecurityCode = "123"
                },
                InstallmentCount = 1
            };

            // Cria requisição.
            var createSaleRequest = new CreateSaleRequest()
            {
                // Adiciona a transação na requisição.
                CreditCardTransactionCollection = new Collection<CreditCardTransaction>(new CreditCardTransaction[] { transaction }),
                Order = new Order()
                {
                    OrderReference = "NumeroDoPedido"
                }
            };

            // Coloque a sua MerchantKey aqui.
            Guid merchantKey = Guid.Parse("0d96500c-fe5e-4454-bd3a-d2a6b83e81a3");

            // Cria o client que enviará a transação.


            var serviceClient = new GatewayServiceClient(merchantKey, new Uri("https://sandbox.mundipaggone.com"));

            // Autoriza a transação e recebe a resposta do gateway.
            var httpResponse = await serviceClient.CreateSale(createSaleRequest);

            console.AppendLine(string.Format("Código retorno: {0}", httpResponse.HttpStatusCode));
            console.AppendLine(string.Format("Chave do pedido: {0}", httpResponse.Response.OrderResult.OrderKey));

            if (httpResponse.Response.CreditCardTransactionResultCollection != null)
            {
                console.AppendLine(string.Format("Status transação: {0}", httpResponse.Response.CreditCardTransactionResultCollection.FirstOrDefault().CreditCardTransactionStatus));
            }

            ViewBag.Result = console.ToString();

            return View();
        }
    }
}
