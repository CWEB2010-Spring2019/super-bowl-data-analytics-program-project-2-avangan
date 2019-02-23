Skip to content
 
Search or jump to…

Pull requests
Issues
Marketplace
Explore
 @avangan Sign out
1
0 0 CWEB2010-Spring2019/super-bowl-data-analytics-program-project-2-avangan
 Code  Issues 0  Pull requests 0  Projects 0  Wiki  Insights
super-bowl-data-analytics-program-project-2-avangan/Project_Two/Program.cs
90fb573  just now
@avangan avangan Update Program.cs
@instructorc @avangan
     
49 lines (31 sloc)  1.24 KB
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Text;


namespace SuperBowl
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Program should read the file from where the project folder exist.
           
            string Path ="C:\\Users\\kozjohn\\Desktop\\superBowlSuper_Bowl_Project.csv ";

            Console.WriteLine("Welcome to the Super Bowl Stats Sheet  ");
        

      
            //textPath variable establishes where the text file will be saved
            string textPath = "C:\\Users\\kozjohn\\Desktop\\.txt";

            ReadWriteFiles(textPath);

        }

        //Method that writes to textPath(includes all queries)
        static void ReadWriteFiles(string adjustedFilePath, string textPath)
        {
            //Create list of objects
            List<SuperBowl> values = File.ReadAllLines(adjustedFilePath)
                                    
                                        
                     
            StreamWriter sw = new StreamWriter(textPath);

            }

            


    }
}


            }

            


    }
}

