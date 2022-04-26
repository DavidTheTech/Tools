using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Auto_delete
{
    class Program
    {
        static void Main(string[] args)
        {
            string TEMP_FOLDER = @"D:\TEMP AUTO DELETE\";

            string[] FILES = Directory.GetFiles(TEMP_FOLDER, "*.*", SearchOption.AllDirectories);

            foreach (var CUR_FILE in FILES)
            {
                FileInfo FI = new FileInfo(CUR_FILE);
                if (FI.LastWriteTime < DateTime.Now.AddMonths(-1))
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Deleting : " + CUR_FILE);
                        File.Delete(CUR_FILE);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully Deleted : " + CUR_FILE);
                        Console.ResetColor();
                    }
                    catch(Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed : " + ex.ToString());
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Newer File : " + CUR_FILE);
                    Console.ResetColor();

                }
            }
            ProcessDirectory(TEMP_FOLDER);
            Console.ReadKey();
        }

        public static void ProcessDirectory(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                ProcessDirectory(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }
    }
}
