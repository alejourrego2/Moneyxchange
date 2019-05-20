using Business;
using Business.Managers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        public static IEnumerable<TestCaseData> eurToCopTestCases
        {
            get
            {
                yield return new TestCaseData(45678.1234m, "EUR", "COP");
                yield return new TestCaseData(1000m, "EUR", "COP");
                yield return new TestCaseData(1m, "EUR", "COP");
                yield return new TestCaseData(0m, "EUR", "COP");
                yield return new TestCaseData(1200.4567m, "EUR", "USD");
                yield return new TestCaseData(100m, "EUR", "USD");
                yield return new TestCaseData(1m, "EUR", "USD");
                yield return new TestCaseData(0m, "EUR", "USD");
                yield return new TestCaseData(20m, "EUR", "JPY");
                yield return new TestCaseData(5m, "USD", "EUR");
                yield return new TestCaseData(20m, "USD", "COP");
                yield return new TestCaseData(500000m, "COP", "USD");
            }
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCaseSource(typeof(Tests), "eurToCopTestCases")]
        public async Task TestExchangeEURtoCOP(decimal value, string fromCurrency, string targetCurrency)
        {
            string url = @"http://data.fixer.io/api/latest?access_key=d0def2e2f997f931537d524b82434b2d&symbols=EUR,USD,COP";

            ExchangeManager exchange = new ExchangeManager(url);

            decimal result = await exchange.ExchangeCurrencies(value, fromCurrency, targetCurrency);

            Assert.GreaterOrEqual(result, 0);
        }
    }
}