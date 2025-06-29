using System;
using System.Collections.Generic;
using System.Linq;

namespace RenameTorpedoByOrder
{
    public static class Logger
    {
        public static List<string> LogErros { get; private set; } = new List<string>();
        public static List<string> LogAvisos { get; private set; } = new List<string>();


        public static void ConsoleLogErro()
        {
            Console.WriteLine("\nLog de Erros:");

            if (LogErros.Any())
            {
                foreach (var erro in LogErros) 
                { 
                    Console.WriteLine(erro); 
                }
            }
            else 
            {
                Console.WriteLine("Nenhum erro encontrado.");
            }
        }

        public static void ConsoleLogAviso()
        {
            Console.WriteLine("\nLog de Avisos:");

            if (LogAvisos.Any())
            {
                foreach (var erro in LogAvisos)
                {
                    Console.WriteLine(erro); 
                }
            }
            else
            {
                Console.WriteLine("Nenhum aviso encontrado.");
            }
        }
    }
}
