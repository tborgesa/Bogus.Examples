using Bogus.Examples._Resources;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bogus.Examples.Entidade
{
    public class ParametroDoMetodoDaCategoriaDoFaker
    {
        private readonly MetodoDaCategoriaDoFaker MetodoDaCategoriaDoFaker;
        private readonly bool _opcional;
        private readonly string _nome;
        private readonly System.Type _tipo;
        public object Valor;

        public bool Valido => !(Valor == null && !_opcional);

        public ParametroDoMetodoDaCategoriaDoFaker(ParameterInfo parametro, MetodoDaCategoriaDoFaker metodoDaCategoriaDoFaker)
        {
            MetodoDaCategoriaDoFaker = metodoDaCategoriaDoFaker;
            _opcional = parametro.IsOptional;
            _nome = parametro.Name;
            _tipo = parametro.ParameterType;
            AtribuirValor();
        }

        private void AtribuirValor()
        {
            switch (_nome)
            {
                case "abbreviation":
                case "returnMax":
                case "upperCase":
                case "UseAbbreviation":
                case "withPrefix":
                case "withSuffix": Valor = true; break;

                case "minDigit": case "minLength": case "yearsToGoForward": Valor = Constantes.Numero1; break;

                case "count":
                case "length":
                case "maxLength":
                case "lineCount":
                case "line":
                case "num":
                case "numbers":
                case "sentenceCount":
                case "weight":
                case "weights":
                case "wordcount": Valor = ConverteValorParaOTipoDoParametro(Constantes.Numero100); break;

                case "height": Valor = Constantes.Numero480; break;

                case "width": Valor = Constantes.Numero720; break;

                case "maxDigit": Valor = Constantes.Numero5; break;

                case "start": Valor = DateTime.Now.AddYears(Constantes.Numero1Negativo); break;

                case "end": Valor = DateTime.Now.AddYears(Constantes.Numero1); break;

                case "yearsToGoBack": Valor = Constantes.Numero1Negativo; break;

                case "max": Valor = ConverteValorParaOTipoDoParametro(Constantes.Numero100); break;

                case "min": Valor = ConverteValorParaOTipoDoParametro(Constantes.Numero1); break;

                default: Valor = null; break;
            }
        }

        private object ConverteValorParaOTipoDoParametro(object valor)
        {
            if (valor == null) return null;

            if (_tipo == typeof(decimal)) return Decimal.Parse(valor.ToString());
            if (_tipo == typeof(double)) return Double.Parse(valor.ToString());
            if (_tipo == typeof(System.Single)) return Single.Parse(valor.ToString());
            if (_tipo == typeof(UInt16)) return UInt16.Parse(valor.ToString());
            if (_tipo == typeof(UInt32)) return UInt32.Parse(valor.ToString());
            if (_tipo == typeof(UInt64)) return UInt64.Parse(valor.ToString());
            if (_tipo == typeof(Int16)) return Int16.Parse(valor.ToString());

            if (_tipo == typeof(byte))
            {
                if (int.Parse(valor.ToString()) > Constantes.Numero255) valor = byte.MaxValue;
                return byte.Parse(valor.ToString());
            }

            if (_tipo == typeof(SByte))
            {
                if (int.Parse(valor.ToString()) > Constantes.Numero127) valor = sbyte.MaxValue;
                return SByte.Parse(valor.ToString());
            }

            if (_tipo == typeof(System.Int32[]))
            {
                return new List<int>((int) valor).ToArray();
            }

            if (_tipo == typeof(IEnumerable<System.Int32>))
            {
                return new List<int>((int)valor);
            }

            if (_tipo == typeof(IEnumerable<System.Int64>))
            {
                return new List<long>((int)valor);
            }

            if (!(_tipo == typeof(Int32)
                || _tipo == typeof(bool)
                || _tipo == typeof(Nullable<System.Int32>)
                || _tipo == typeof(System.Int64)
                || _tipo == typeof(System.Int16))) return valor;

            return null;
        }

        public override string ToString()
        {
            var caracteristica = _opcional ? " (opcional)" : "";
            var descricaoDoValor = Valor == null ? "" : $" = {Valor}";
            return $"{_nome}:{_tipo}{caracteristica}{descricaoDoValor}";
        }
    }
}
