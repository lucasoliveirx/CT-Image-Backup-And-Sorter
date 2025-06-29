using System;
using System.IO;

namespace RenameTorpedoByOrder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sourceDirectory = @"\\SourceDirectory";
            string outputDirectory = @"C:\CT_Images_Backup\CT_Backup_" + DateTime.Now.ToString("dd_MM_yyyy");

            bool renameFiles = BackupFiles.BackupImages(sourceDirectory, outputDirectory);

            if (renameFiles)
            {
                string[] subpastas = Directory.GetDirectories(outputDirectory, "*", SearchOption.AllDirectories);

                bool compareFiles = TreatmentFiles.CompareQuantityOfFiles(sourceDirectory, outputDirectory);

                if (compareFiles)
                {
                    TreatmentFiles.DeleteAllSubpaths(sourceDirectory);

                    foreach (var subpasta in subpastas)
                    {
                        Console.WriteLine($"\nProcessando: {subpasta}");
                        TreatmentFiles.ProcessSubpaths(subpasta, sourceDirectory);
                    }
                    Console.WriteLine("\nProcesso concluído!");

                    TreatmentFiles.CompareQuantityOfFiles(sourceDirectory, outputDirectory);

                    Console.Clear();
                    Logger.ConsoleLogErro();
                    Logger.ConsoleLogAviso();
                }
                else
                {
                    Console.WriteLine("A comparação de arquivos está diferente, cancelando operação...");
                }
            }
            Console.ReadKey();
        }
    }
}
