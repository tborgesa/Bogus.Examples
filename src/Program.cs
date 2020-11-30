using Bogus.Examples._Resources;
using Bogus.Examples.Extensions;
using System.Linq;

namespace Bogus.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var categoriaDoFaker in new Faker().ObterListaDeCategoriaFaker())
            {
                foreach (var metodo in categoriaDoFaker.ListaDeMetodoDaCategoriaDoFaker)
                {
                    metodo.ToStringComParametros().EscreverNaTela();
                    metodo.ExecutarMetodo().EscreverNaTelaEmFormatoJson();
                    GenericoResource.Separador.EscreverNaTela();
                }
            }
        }
    }
}
