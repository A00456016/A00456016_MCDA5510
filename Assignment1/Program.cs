using System;
using System.IO;
using System.Collections;


namespace Assignment1
{
    public class DirWalker
    {
        public ArrayList files = new ArrayList();

        public ArrayList custlist = new ArrayList();
        public int totalvalid = 0;
        public int totalskip = 0;
        public void walk(String path)
        {
            string[] list = Directory.GetDirectories(path);

            if (list == null) return;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                    Console.WriteLine("Dir:" + dirpath);
                }
            }

            string[] fileList = Directory.GetFiles(path);
         
            foreach (string filepath in fileList)
            {
                /*  files.Add(filepath); */
                /* Console.WriteLine("Files:" + filepath); */
                    using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var values = line.Split(',');
                                bool insert = true;
                                bool header = false;
                            
                                Customer cust = new Customer();

                            if (values.Length < 10)
                            {
                                insert = false;
                            }
                            else
                            {
                                for (int i = 0; i < values.Length; i++)
                                {
                                    if (values[i] == "" ||
                                        values[i] == "\"\"" ||
                                        values[i] == null ||
                                        values[i] == " " ||
                                       values[i] == "First Name")
                                    {
                                        insert = false;
                                        if(values[i] == "First Name")
                                        {
                                            header = true;
                                        }
                                    }
                                }
                            }
                                if (insert == true)
                                {
                                    cust.fname = values[0];
                                    cust.lname = values[1];
                                    cust.stnum = values[2];
                                    cust.strt = values[3];
                                    cust.city = values[4];
                                    cust.prov = values[5];
                                    cust.pcod = values[6];
                                    cust.ctry = values[7];
                                    cust.phno = values[8];
                                    cust.email = values[9];
                                    custlist.Add(cust);
                                    totalvalid++;
                                }
                                else
                                {
                                if (header == false)
                                {
                                    totalskip++;
                                }
                                }
                            }
                        }
                    }
                
              

            }
        }
        public void getdata(ArrayList A)
        {
            foreach (Customer cu in custlist)
            {
                A.Add(cu);
            }
        }

        public static void Main(String[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            DirWalker fw = new DirWalker();
            fw.walk(@"C:\Users\Friends\Downloads\Sample Data");
            ArrayList file = new ArrayList();
            int tvalid = fw.totalvalid;
            int tskip = fw.totalskip;
            fw.getdata(file);

            using (StreamWriter sw = new StreamWriter(@"C:\Users\Friends\Desktop\output.csv"))
            {
                sw.WriteLine("First Name,Last Name,Street Number,Street,City,Province,Postal Code,Country,Phone Number,email Address");

                foreach (Customer curc in file)
                {
                    sw.Write(curc.fname + ",");
                    sw.Write(curc.lname + ",");
                    sw.Write(curc.stnum + ",");
                    sw.Write(curc.strt + ",");
                    sw.Write(curc.city + ",");
                    sw.Write(curc.prov + ",");
                    sw.Write(curc.pcod + ",");
                    sw.Write(curc.ctry + ",");
                    sw.Write(curc.phno + ",");
                    sw.WriteLine(curc.email);
                }
            }
            watch.Stop();
            long time = watch.ElapsedMilliseconds;

            var logger = new Logger();
            logger.Log(time, tvalid, tskip);
        }
    }

}



