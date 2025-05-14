using System;

public class ProcessManagerClass
{
    public ProcessManagerClass()
    {
    }

    public List<string> GetProcessList()
    {
        List<string> processList = new List<string>();
        Process[] processes = Process.GetProcesses();

        foreach (var process in processes)
        {
            processList.Add(process.ProcessName);
        }

        // Sort the process list alphabetically  
        processList.Sort();

        // Log the process list to the console  
        foreach (var processName in processList)
        {
            Console.WriteLine(processName);
        }

        return processList;
    }
}
