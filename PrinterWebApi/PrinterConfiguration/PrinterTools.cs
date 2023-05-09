using System.ComponentModel;
using System.Reflection;
using PTIRelianceLib;
using PTIRelianceLib.Flash;

namespace PrinterConfiguration
{
    public abstract class PrinterTools
    {
        public static void FlashConfiguration()
        {
            var file = BinaryFile.From(Path.GetFullPath("pnf-printer-config.rfg"));
            
            if(file.Empty)
                Console.WriteLine("ERROR: Could not find config file.");

            else
            {
                using (var printer = new ReliancePrinter())
                {
                    var result = printer.SendConfiguration(file);


                    Console.WriteLine("\nConfiguration Update Result: {0}", result);
                }
            }
        }
    }
}