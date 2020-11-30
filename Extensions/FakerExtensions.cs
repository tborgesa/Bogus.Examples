using Bogus.Examples.Entidade;
using System.Collections.Generic;
using System.Linq;

namespace Bogus.Examples.Extensions
{
    public static class FakerExtensions
    {
        public static List<CategoriaDoFaker> ObterListaDeCategoriaFaker(this Faker faker)
        {
            var listaDeCategoriaDoFaker = new List<CategoriaDoFaker>();

            faker.GetType().GetProperties().
                ToList().
                ForEach(_ => listaDeCategoriaDoFaker.Add(new CategoriaDoFaker(_, faker)));

            return listaDeCategoriaDoFaker.Where(_ => _.Valido).ToList();
        }
    }
}
