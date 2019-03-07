using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Text;

namespace SuperProject
{
    public class Program
    {
        static void Main(string[] args)
        {
           //Program should read the file from where the project folder exist.
         
            string CDPath = Directory.GetCurrentDirectory();
            string stepBackOne = Directory.GetParent(CDPath).ToString();
            string stepBackTwo = Directory.GetParent(stepBackOne).ToString();
            string stepBackThree = Directory.GetParent(stepBackTwo).ToString();
            string FileCsv = $@"{stepBackThree}\Super_Bowl_Project.csv";

            Console.WriteLine("Hi Here's a ton of Super Bowl Facts You may not have know about ! ");
            Console.WriteLine("Go ahead and enter the name of the text file you want your facts to be saved as.  ");

            // User enters the file name they want the statSheet to be stored
            string Input = Console.ReadLine();
         

            // Created a variable to hold the file path and name of the fact sheet. ( note the user chose the input text file name. 
            string statSheet = $@"{stepBackThree}\{Input}.txt";

            // Read in the CSV and the text file the user created
            ReadWriteFiles(FileCsv , statSheet);

        }

        
        static void ReadWriteFiles(string FileCsv , string statSheet)

        {
            // Created a list with the <t> Object. All values in the list now must be object Stats
            //Create list of objects from reading all the lines in the csv
            // Name of list I created to hold the SuperBClass Objects is Called SuperList This was required because you need it to go over with linq

            // Stack overflow code  listOfObjects = File.ReadLines( "theFile.csv" ).Select( line => new Item( line ) ).ToList();
            List<SuperBClass> Superlist = File.ReadAllLines(FileCsv)
                                             .Skip(1)
                                             .Select(line => SuperBClass.FromCsv(line))
                                             .ToList();
            // Open StreamWriter method and pass in the statSheet to write into            
            StreamWriter writeStatSheet = new StreamWriter(statSheet);

            //Write a list of super bowl winners
                                                                 
            writeStatSheet.WriteLine("-Super Bowl Winners -");

            // Creating a Variable to save the linq Goes over your list of objects to find the dates 

            var SuperBClassWinners =
                from x in Superlist
                where x.Date != null
                select x;

            // loop thru the variable SuperBClassWinner and select our the object SuperBClass 
            foreach (SuperBClass x in SuperBClassWinners)
            {
                string output = String.Format("    {0,-19} {1,-4} {2,-28} {3,-15} {4,-27} {5,-10}",
                  x.WinTeam, x.Date.Year, x.WinQb,
                    x.WinCoach, x.Spread);
                writeStatSheet.WriteLine(output);


            }

            //Generate a list of the top five attended super bowl’s
          
        
            writeStatSheet.WriteLine(" Most atttended super bowls ");
            var AttendFive =
               (from x in Superlist
                orderby x.attended descending
                select x).Take(5);

            foreach (SuperBClass x in AttendFive)
            {
                string output = String.Format("      {0,-19} {1,-4} {2,-28} {3,-15} {4,-27} {5,-13}",
                    x.Date.Year, x.WinTeam, x.losingTeam,
                    x.city, x.state, x.hostStadium);
                writeStatSheet.WriteLine(output);

            }

            //Output the state that hosted the  most super bowls
       
            writeStatSheet.WriteLine("        State with that has hosted the most SuperBowls    ");


            var stateMostHosted = Superlist.GroupBy(x => x.state)
                                            .Select(group => new { state = group.Key, Count = group.Count() })
                                            .OrderByDescending(x => x.Count);
            var item = stateMostHosted.First();

            var mostFrequent = item.state;
            var mostFrequentCount = item.Count;

            foreach (SuperBClass x in Superlist)
            {
                if (x.state.Equals(mostFrequent))
                {
                    string output = String.Format("      {0,-12} {1,-12} {2,-13}",
                    x.city, x.state, x.hostStadium);
                    writeStatSheet.WriteLine(output);
                }
            }

            // a list of Mvp players that have won more than once
       
            writeStatSheet.WriteLine(" PLAYERS WHO WON THE SUPER BOWL Mvp TITLE MORE THAN ONCE ");
            writeStatSheet.WriteLine("--Mvp's-Teams that Won-Losing Teams--");

            var Mvp = from x in Superlist
                      group x by x.Mvp into MvpGroups
                      orderby MvpGroups.Count() descending
                      select MvpGroups;
            foreach (var Mvpgroup in Mvp)
            {
                if (Mvpgroup.Count() > 1)
                {
                    foreach (var x in Mvpgroup)
                    {
                        string output = String.Format("      {0,-17} {1,-21} {2,-21}",
                        x.Mvp, x.WinTeam, x.losingTeam);
                        writeStatSheet.WriteLine(output);
                    }
                }

            }

            //Losing Coaches
            writeStatSheet.WriteLine("");
            writeStatSheet.WriteLine("Coaches who have lost the most SuperBowls");

            var LoseCoach = Superlist.GroupBy(x => x.LoseCoach)
                                      .Select(group => new { LoseCoach = group.Key, Count = group.Count() })
                                      .OrderByDescending(x => x.Count);
            var clm = LoseCoach.First();
            var mostLosses = clm.LoseCoach;
            var bigloss = clm.Count;

            foreach (SuperBClass x in Superlist)
            {
                if (x.LoseCoach.Equals(mostLosses))
                {
                    writeStatSheet.WriteLine("  " + x.LoseCoach + "  ");
                    break;
                }
            }

            //Which coach won the most super bowls?
            writeStatSheet.WriteLine("");
            writeStatSheet.WriteLine("Mvp Coach . This Coach has won the most SuperBowls in History");

            var CoachWin = Superlist.GroupBy(x => x.WinCoach)
                                     .Select(group => new { WinCoach = group.Key, Count = group.Count() })
                                     .OrderByDescending(x => x.Count);
            var cwm = CoachWin.First();
            var mostWins = cwm.WinCoach;
            var mostFrequentWins = cwm.Count;

            foreach (SuperBClass x in Superlist)
            {
                if (x.WinCoach.Equals(mostWins))
                {
                    writeStatSheet.WriteLine("  " + x.WinCoach + "  ");
                    break;
                }
            }

            //Which team(s) won the most super bowls?
            writeStatSheet.WriteLine("");
            writeStatSheet.WriteLine("The most winning teams in Superbowl History");

            var bestTeams = Superlist.GroupBy(x => x.WinTeam)
                                    .Select(group => new { WinTeam = group.Key, Count = group.Count() })
                                    .OrderByDescending(SuperBClass => SuperBClass.Count);
            var mostWinsTeam = bestTeams.FirstOrDefault();
            var tMostWins = mostWinsTeam.WinTeam;
            var tMostFrequentWins = mostWinsTeam.Count;

            foreach (SuperBClass x in Superlist)
            {
                if (x.WinTeam.Equals(tMostWins))
                {
                    writeStatSheet.WriteLine("  " + x.WinTeam + "  ");
                    break;
                }
            }

            //Teams who have lost the most Superbowls in History 
             //Which team(s) lost the most super bowls?
            writeStatSheet.WriteLine("");
            writeStatSheet.WriteLine("THE TEAM(s) THAT LOST THE MOST SUPER BOWLS");

            var teamLostMost = Superlist.GroupBy(x => x.losingTeam)
                                     .Select(group => new { losingTeam = group.Key, Count = group.Count() })
                                     .OrderByDescending(x => x.Count);
            var tlm = teamLostMost.FirstOrDefault();
            var tMostLosses = tlm.losingTeam;
            var tMostFrequentLosses = tlm.Count;

            foreach ( SuperBClass  x in Superlist)
            {
                if (x.losingTeam.Equals(tMostLosses))
                {
                    writeStatSheet.WriteLine("  " + x.losingTeam + "  ");
                    break;
                }
            }

            //Which Super bowl had the greatest point Spreadference?
            writeStatSheet.WriteLine("");
            writeStatSheet.WriteLine(" Superbowl with the largest Spreadferance in points");

            var pointSpread = Superlist.GroupBy(x => x.Spread)
                                                .Select(group => new { Spread = group.Key, Count = group.Count() })
                                                .OrderBy(x => x.Spread);
            var gpd = pointSpread.Last();
            var greatestSpreadference = gpd.Spread;
            var SpreadCount = gpd.Count;

            foreach (SuperBClass x in Superlist)
            {
                if (x.Spread.Equals(greatestSpreadference))
                {
                    writeStatSheet.WriteLine("Super Bowl " + x.romanNumeral + " had the greatest point Spreadfernce of " + x.Spread + " in " + x.Date.Year + ".");
                }
            }

            // Average attended 
            writeStatSheet.WriteLine("");
            writeStatSheet.WriteLine("The Average attended ");
            double avgAttended = Superlist.Average(x => x.attended);
            writeStatSheet.WriteLine("The average attended per Super Bowls after 1967 has been " + avgAttended.ToString("n0") + " attendees.");

            writeStatSheet.Close();


        }

   
    }
}



