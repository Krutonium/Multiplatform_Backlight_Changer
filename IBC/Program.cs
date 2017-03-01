using System;
using System.IO;
namespace IBC
{
    class Program
    {
        static void Main(string[] args)
        {
            int wantBright=100;
            try
            {
                Int32.TryParse(args[0], out wantBright);
            } catch
            {
                Console.WriteLine("Bad Argument. Must be a number.");
                Environment.Exit(1);
            }
            

            if (System.Environment.OSVersion.Platform == PlatformID.Unix)
            {
                foreach (var controller in Directory.GetDirectories("/sys/class/backlight/"))
                {
                    int max_brightness;
                    Int32.TryParse(File.ReadAllText(controller + "/max_brightness"), out max_brightness);
                    int percent = max_brightness * wantBright;
                    percent = percent / 100;
                    try
                    {
                        File.WriteAllText(controller + "/brightness", percent.ToString());
                    }
                    catch
                    {
                        Console.WriteLine("You may need to run this as sudo/root.");
                    }
                }
            }
            else if(System.Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");
                System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightnessMethods");
                System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);
                System.Management.ManagementObjectCollection moc = mos.Get();
                foreach (System.Management.ManagementObject o in moc)
                {
                    o.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, wantBright });
                    break;
                }
                moc.Dispose();
                mos.Dispose();
            }
        }
    }
}
