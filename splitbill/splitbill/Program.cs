using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace splitbill
{
    public class Program
    {
        //calculate cost for each person
        public List<decimal> cal_cost(List<decimal> current)
        {
            decimal average = current.Average();
            List<decimal> temp_output = new List<decimal>();
            foreach (var i in current)
            {
                decimal split = Math.Round((average - i), 2);

                temp_output.Add(split);
            }
            return temp_output;
        }

        static void Main(string[] args)
        {   
            
            //get the file name
            string file = Console.ReadLine();
            //get the project folder filepath 
            string cwp = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filepath = Path.Combine(cwp, file);
            Program program = new Program();
            //clean everything that exist before in output file.
            if (File.Exists(filepath + ".out"))
            {
                File.WriteAllText(filepath + ".out", String.Empty);
            }

            try
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line = sr.ReadLine();
                    
                    while (line != null)
                    {
                        List<decimal> temp_output = new List<decimal>();
                        int total_p = Convert.ToInt32(line);
                        List<decimal> current = new List<decimal>();
                        //n = number of participants, p = number of receipts
                        int p = 0;
                        
                        while (p != total_p)
                        {
                            int n = 0;
                            line = sr.ReadLine();
                            int total_n = Convert.ToInt32(line);
                            decimal sum = 0;
                            while (n != total_n)
                            {
                                line = sr.ReadLine();
                                sum = sum + decimal.Parse(line);
                                n++;
                            }
                            current.Add(sum);
                            p++;
                            
                        }
                        //calculate cost and add to the output file
                        if (p != 0)
                        {
                            temp_output = program.cal_cost(current);

                            try
                            {

                                string outputFile = filepath + ".out";
                                StreamWriter sw = new StreamWriter(outputFile, true);
                                

                                using (sw)
                                {
                                    
                                    foreach (var item in temp_output)
                                    {
                                        if (item >= 0)
                                            sw.WriteLine("$"+ item.ToString());
                                        else
                                            sw.WriteLine("($" + (Math.Abs(item)).ToString() + ")");
                                    }
                                    sw.WriteLine("");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("The file could not be written:");
                                Console.WriteLine(e.Message);
                            }
                            

                        }
                        
                        //refresh all data for next trip.
                        line = sr.ReadLine();
                        
                    }
                    

                    sr.Close();
                }




            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Job Done check output file in project folder ");
        }
        
    }
}
