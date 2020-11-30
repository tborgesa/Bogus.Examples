using System;
using System.Text.Json;

namespace Bogus.Examples.Extensions
{
    public static class WriteLineExtensions
    {
        public static void EscreverNaTela(this object conteudo)
        {
            Console.WriteLine(conteudo);
        }

        public static void EscreverNaTelaEmFormatoJson(this object conteudo)
        {
            try
            {
                var opcoes = new JsonSerializerOptions { WriteIndented = true };
                Console.WriteLine(JsonSerializer.Serialize(conteudo, opcoes));
            }
            catch
            {
                conteudo.EscreverNaTela();
            }

        }
    }
}
