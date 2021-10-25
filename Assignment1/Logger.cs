using System;
using System.IO;
using System.Collections;

public class Logger
{
    private string FileName
    {
        get;
        set;
    }
    private string FilePath
    {
        get;
        set;
    }
    public Logger()
    {
        this.FileName = "Log.txt";
        this.FilePath = "C:/Users/Friends/Desktop/" + this.FileName;
    }
    public void Log(long x, int y, int z)
    {
        using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
        {
            w.WriteLine("Log Entry: ");
            w.WriteLine("---------------------------------");
            w.WriteLine("Total Execution Time: " + x + "ms");
            w.WriteLine("Total Valid Rows: " + y);
            w.WriteLine("Total Skipped Rows: " + z);
        }
    }
}