using Microsoft.Xna.Framework;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace csproject2024.src
{
    internal class DataLogger
    {
        float elapsedTime;

        //Device information

        string OS;
        string CPU;
        string GPU;
        string VRAM;
        string memory;
        

        public DataLogger()
        {
            elapsedTime = 0f;

            OS = "Loading Hardware Info. Please Wait...";
            CPU = "";
            GPU = "";
            VRAM = "";
            memory = "";

            LoadDeviceInformation();

        }

        private void ParseDxDiagXml(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CPU = "Loading DXDiag XML file failed. Check logs for details";
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode osNode = doc.SelectSingleNode("/DxDiag/SystemInformation/OperatingSystem");
            OS = osNode?.InnerText ?? "Unknown OS";


            XmlNode processorNode = doc.SelectSingleNode("/DxDiag/SystemInformation/Processor");
            CPU = processorNode?.InnerText ?? "Unknown CPU";

            XmlNode memoryNode = doc.SelectSingleNode("/DxDiag/SystemInformation/Memory");
            memory = memoryNode?.InnerText ?? "Unknown Total Memory";

            XmlNodeList displayDevices = doc.GetElementsByTagName("DisplayDevice");
            foreach (XmlNode device in displayDevices)
            {
                GPU = device.SelectSingleNode("CardName")?.InnerText ?? "Unknown GPU";
                VRAM = device.SelectSingleNode("DedicatedMemory")?.InnerText ?? "Unknown VRAM";
                break;
            }
        }

        private async void LoadDeviceInformation()
        {
            await Task.Run(() =>
            {
                ProcessStartInfo processInfo = new ProcessStartInfo("dxdiag.exe", "/x dxdiag_output.xml")
                {
                    RedirectStandardOutput = false,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process process = Process.Start(processInfo);
                process.WaitForExit();

                ParseDxDiagXml("dxdiag_output.xml");
                Console.Clear();

            });
        }

        public void Update()
        {
            //#####################
            bool doLogging = true;
            //#####################

            if (doLogging)
            {
                float FPS = 1 / Globals.ElapsedSeconds;
                long memoryUsage = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                long peakMemory = Process.GetCurrentProcess().PeakWorkingSet64 / (1024 * 1024);
                long privateMemory = Process.GetCurrentProcess().PrivateMemorySize64 / (1024 * 1024);
                long virtualMemory = Process.GetCurrentProcess().VirtualMemorySize64 / (1024 * 1024);

                List<string> data = new List<string> {
                "System Info:",
                "--------------------------------",
                "OS: " + OS,
                "CPU: " + CPU,
                "GPU: " + GPU,
                "VRAM: " + VRAM,
                "Total Memory: " + memory,
                "",
                "Base:",
                "--------------------------------",
                "FPS: " + FPS,
                "",
                "Memory:",
                "--------------------------------",
                "Memory Usage: " + memoryUsage + "MB",
                "Peak Memory Usage: " + peakMemory + "MB",
                "Private Memory: " + privateMemory + "MB",
                "Virtual Memory: " + virtualMemory + "MB"  };

                elapsedTime += Globals.ElapsedSeconds;

                if (elapsedTime > 0.1)
                {
                    LogData(data);
                    elapsedTime = 0;
                }
            }
        }

        public async void LogData(List<string> data)
        {
            await Task.Run(() => Console.SetCursorPosition(0, 0));
            await Task.Run(() => PrintData(data));
        }

        private void PrintData(List<string> data)
        {
            foreach (string dataLine in data)
            {
                Console.WriteLine(dataLine);
            }
        }
    }
}
