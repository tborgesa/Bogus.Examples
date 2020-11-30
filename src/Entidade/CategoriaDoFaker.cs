using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bogus.Examples.Entidade
{
    public class CategoriaDoFaker
    {
        public readonly bool Valido;
        private readonly Faker _faker;
        public readonly object Instancia;
        public readonly string Nome;

        public List<MetodoDaCategoriaDoFaker> ListaDeMetodoDaCategoriaDoFaker { get; private set; }
        

        public CategoriaDoFaker(PropertyInfo propriedade, Faker faker)
        {
            if (propriedade.DeclaringType.Name != typeof(Faker).Name)
            {
                Valido = false;
                return;
            }

            var listaDePropriedadesIgnoradas = new List<string>();
            listaDePropriedadesIgnoradas.Add("UniqueIndex");
            listaDePropriedadesIgnoradas.Add("Locale");
            listaDePropriedadesIgnoradas.Add("IndexGlobal");

            if (listaDePropriedadesIgnoradas.Contains(propriedade.Name))
            {
                Valido = false;
                return;
            }

            _faker = faker;
            Instancia = propriedade.GetValue(_faker);
            Nome = propriedade.Name;

            GerarListaDeMetodoDaCategoriaDoFaker(propriedade);

            Valido = true;
        }

        private void GerarListaDeMetodoDaCategoriaDoFaker(PropertyInfo propriedade)
        {
            ListaDeMetodoDaCategoriaDoFaker = new List<MetodoDaCategoriaDoFaker>();
            Instancia.GetType().GetMethods().
                ToList().
                ForEach(_ => ListaDeMetodoDaCategoriaDoFaker.Add(new MetodoDaCategoriaDoFaker(_, this)));

            ListaDeMetodoDaCategoriaDoFaker = ListaDeMetodoDaCategoriaDoFaker.Where(_ => _.Valido).ToList();
        }
    }
}
