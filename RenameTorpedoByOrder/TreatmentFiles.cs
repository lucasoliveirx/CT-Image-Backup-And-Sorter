using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace RenameTorpedoByOrder
{
    public static class TreatmentFiles
    {
        public static void ProcessSubpaths(string pastaOrigem, string pastaDestinoBase)
        {
            Logger.LogErros.Clear();
            Logger.LogAvisos.Clear();

            string[] arquivos = Directory.GetFiles(pastaOrigem, "*.png");

            List<string> grupo_01 = new List<string>();
            List<string> grupo_02 = new List<string>();
            List<string> grupo_03 = new List<string>();
            List<string> grupo_04 = new List<string>();
            List<string> grupo_05 = new List<string>();
            List<string> grupo_06 = new List<string>();

            string nomeSubpasta = new DirectoryInfo(pastaOrigem).Name;
            string pastaDestino = Path.Combine(pastaDestinoBase, nomeSubpasta);


            try
            {
                foreach (var caminhoArquivo in arquivos)
                {
                    string nome = Path.GetFileNameWithoutExtension(caminhoArquivo);
                    string[] partes = nome.Split('_');

                    if (partes.Length < 3)
                    {
                        Logger.LogErros.Add($"[ERRO] Nome inesperado: '{nome}' não tem 3 partes separadas por '_'");
                        Console.WriteLine($"[ERRO] Nome inesperado: '{nome}' não tem 3 partes separadas por '_'");
                        continue;
                    }

                    if (partes[1] == "01")
                    {
                        grupo_01.Add(caminhoArquivo);
                        Console.WriteLine($"{caminhoArquivo} adicionado ao -> grupo_01");
                    }

                    else if (partes[1] == "02")
                    {
                        grupo_02.Add(caminhoArquivo);
                        Console.WriteLine($"{caminhoArquivo} adicionado ao -> grupo_02");
                    }

                    else if (partes[1] == "03")
                    {
                        grupo_03.Add(caminhoArquivo);
                        Console.WriteLine($"{caminhoArquivo} adicionado ao -> grupo_02");
                    }

                    else if (partes[1] == "04")
                    {
                        grupo_04.Add(caminhoArquivo);
                        Console.WriteLine($"{caminhoArquivo} adicionado ao -> grupo_02");
                    }

                    else if (partes[1] == "05")
                    {
                        grupo_05.Add(caminhoArquivo);
                        Console.WriteLine($"{caminhoArquivo} adicionado ao -> grupo_02");
                    }

                    else if (partes[1] == "06")
                    {
                        grupo_06.Add(caminhoArquivo);
                        Console.WriteLine($"{caminhoArquivo} adicionado ao -> grupo_02");
                    }

                    else
                    {
                        Logger.LogErros.Add($"[ERRO] {caminhoArquivo} está com formato incorreto!");
                        Console.WriteLine($"[ERRO] {caminhoArquivo} está com formato incorreto!");
                    }
                }
                RenameFilesByOrder(grupo_01, pastaDestino);
                RenameFilesByOrder(grupo_02, pastaDestino);
                RenameFilesByOrder(grupo_03, pastaDestino);
                RenameFilesByOrder(grupo_04, pastaDestino);
                RenameFilesByOrder(grupo_05, pastaDestino);
                RenameFilesByOrder(grupo_06, pastaDestino);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void RenameFilesByOrder(List<string> arquivos, string pastaDestino)
        {
            var arquivosOrdenados = arquivos
                .OrderBy(file => File.GetLastWriteTime(file))
                .ThenBy(file => Path.GetFileName(file))
                .ToList();

            try
            {
                if (!Directory.Exists(pastaDestino))
                    Directory.CreateDirectory(pastaDestino);

                for (int i = 0; i < arquivosOrdenados.Count; i++)
                {
                    string caminhoOriginal = arquivosOrdenados[i];
                    string nomeOriginal = Path.GetFileNameWithoutExtension(caminhoOriginal);

                    string[] partes = nomeOriginal.Split('_');

                    int parte0int = int.Parse(partes[0]);
                    string parte0 = parte0int.ToString("00");

                    string novoNome = $"{parte0}_{partes[1]}_{i + 1}.png";
                    string novoCaminho = Path.Combine(pastaDestino, novoNome);

                    if (!File.Exists(novoCaminho))
                    {
                        File.Copy(caminhoOriginal, novoCaminho);
                        Console.WriteLine($"[OK] Copiado e renomeado: {Path.GetFileName(caminhoOriginal)} -> {novoNome}");
                    }
                    else
                    {
                        Logger.LogAvisos.Add($"[AVISO] Arquivo já existe: {novoCaminho}");
                        Console.WriteLine($"[AVISO] Arquivo já existe: {novoCaminho}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void DeleteAllSubpaths(string path)
        {
            foreach (string subpasta in Directory.GetDirectories(path))
            {
                try
                {
                    Console.WriteLine($"Deletando: {subpasta}");
                    Directory.Delete(subpasta, recursive: true);
                    Console.WriteLine($"Subpasta deletada: {subpasta}");
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Logger.LogErros.Add($"[Erro] - Erro ao deletar {subpasta}: {ex.Message}");
                    Console.WriteLine($"[Erro] - Erro ao deletar {subpasta}: {ex.Message}");
                }
            }
        }

        public static bool CompareQuantityOfFiles (string sourceDirectory, string outputDirectory)
        {
            int numberFilesServer = 0;
            int numberFilesLocal = 0;

            Console.WriteLine("Comparando quantidade de arquivos: Servidor | Backup Local");

            foreach (string subDirectory in Directory.GetDirectories(sourceDirectory))
            {
                foreach (string file in Directory.GetFiles(subDirectory, "*.png"))
                {
                    numberFilesServer++;
                }
            }

            foreach (string subDirectory in Directory.GetDirectories(outputDirectory))
            {
                foreach (string file in Directory.GetFiles(subDirectory, "*.png"))
                {
                    numberFilesLocal++;
                }
            }

            Console.WriteLine($"[Total Imagens Servidor]     -  {numberFilesServer}");
            Console.WriteLine($"[Total Imagens Backup Local] -  {numberFilesLocal}");
            Console.WriteLine($"\nDigte uma tecla para continuar.\n");
            Console.ReadKey();

            if ( numberFilesServer == numberFilesLocal) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
