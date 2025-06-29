using System;
using System.IO;

namespace RenameTorpedoByOrder
{
    public static class BackupFiles
    {
        public static bool BackupImages(string sourceDirectory, string outputDirectory)
        {
            string backupDirectory = @"C:\CT_Images_Backup\";

            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            Directory.CreateDirectory(outputDirectory);
            Console.WriteLine($"Diretório de Backup - {outputDirectory}");

            int numberOfFiles = 0;

            // Copia todas imagens pelas subpastas de cada CT
            foreach (string subDirectory in Directory.GetDirectories(sourceDirectory))
            {
                string nameDirectory = Path.GetFileName(subDirectory);
                string destinySubDirectory = Path.Combine(outputDirectory, nameDirectory);

                Directory.CreateDirectory(destinySubDirectory);

                foreach (string file in Directory.GetFiles(subDirectory))
                {
                    string fileName = Path.GetFileName(file);
                    string destinyFile = Path.Combine(destinySubDirectory, fileName);
                    File.Copy(file, destinyFile);
                    Console.WriteLine("[OK] - " + fileName);
                    numberOfFiles++;
                }
            }

            Console.WriteLine($"\nBackup Realizado com sucesso! \nTotal de imagens: {numberOfFiles} \n\n{outputDirectory}");
            Console.WriteLine("\n\n[OBS] - Verifique se o Backup foi feito corretamente! \n\nDigite uma tecla para continuar");
            Console.ReadKey();
            Console.Clear();

            while (true)
            {
                Console.WriteLine("[Y] - Ordenar todas imagens do servidor com base no Backcup feito");
                Console.WriteLine("[N] - Fechar aplicação\n");

                string input = Console.ReadLine()?.ToLower();
                
                switch (input)
                {
                    case "y":
                        return true;

                    case "n":
                        return false;

                    default:
                        Console.WriteLine("Input incorreto.");
                        break;
                }
            }
        }
    }
}
