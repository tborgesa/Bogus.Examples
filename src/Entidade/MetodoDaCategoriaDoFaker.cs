using Bogus.Examples._Resources;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bogus.Examples.Entidade
{
    public class MetodoDaCategoriaDoFaker
    {
        private readonly CategoriaDoFaker _categoriaDoFaker;
        private readonly MethodInfo _metodo;
        public readonly bool Valido;
        private readonly string _nome;

        public List<ParametroDoMetodoDaCategoriaDoFaker> ListaDeParametroDoMetodoDaCategoriaDoFaker { get; private set; }

        public MetodoDaCategoriaDoFaker(MethodInfo metodo, CategoriaDoFaker categoriaDoFaker)
        {
            if (metodo.DeclaringType.Name != categoriaDoFaker.Instancia.GetType().Name)
            {
                Valido = false;
                return;
            }

            _categoriaDoFaker = categoriaDoFaker;
            _metodo = metodo;
            _nome = metodo.Name;
            GerarListaDeParametroDoMetodoDaCategoriaDoFaker(metodo);

            Valido = true;
        }

        private void GerarListaDeParametroDoMetodoDaCategoriaDoFaker(MethodInfo metodo)
        {
            ListaDeParametroDoMetodoDaCategoriaDoFaker = new List<ParametroDoMetodoDaCategoriaDoFaker>();
            metodo.GetParameters().
                ToList().
                ForEach(_ => ListaDeParametroDoMetodoDaCategoriaDoFaker.Add(new ParametroDoMetodoDaCategoriaDoFaker(_, this)));
        }

        public object ExecutarMetodo()
        {
            if (ListaDeParametroDoMetodoDaCategoriaDoFaker.Any(_ => !_.Valido)) return MetodoDaCategoriaDoFakerResource.ParametrosInvalidos;

            try
            {
                return _metodo.Invoke(
                    _categoriaDoFaker.Instancia,
                    ListaDeParametroDoMetodoDaCategoriaDoFaker.Select(_ => _.Valor).ToArray());
            }
            catch
            {
                return MetodoDaCategoriaDoFakerResource.ErroNaChamada;
            }
        }

        public override string ToString()
        {
            return $"{_categoriaDoFaker.Nome}.{_nome}";
        }

        public string ToStringComParametros()
        {
            var descricaoDosParametros = new List<string>();
            ListaDeParametroDoMetodoDaCategoriaDoFaker.ForEach(_ => descricaoDosParametros.Add(_.ToString()));

            return $"{ToString()}({string.Join(", ",descricaoDosParametros)})";
        }
    }
}
