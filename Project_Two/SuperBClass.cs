
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperProject
{
    //SuperBClass Object Class
    class SuperBClass
    {
        public DateTime Date { get; set; }
        public string romanNumeral { get; set; }
        public int attended { get; set; }
        public string WinQb { get; set; }
        public string WinCoach { get; set; }
        public string WinTeam { get; set; }
        public int WinPoints { get; set; }
        public string LoseQb { get; set; }
        public string LoseCoach { get; set; }
        public string losingTeam { get; set; }
        public int PointLoss { get; set; }
        public string Mvp { get; set; }
        public int Spread { get; set; }
        public string hostStadium { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        //Method helps assign values to values list
        public static SuperBClass FromCsv(string csvLine)
        {
            string[] Superlist = csvLine.Split(',');
            SuperBClass SuperBClass = new SuperBClass();
            SuperBClass.Date = Convert.ToDateTime(Superlist[0]);
            SuperBClass.romanNumeral = Superlist[1];
            SuperBClass.attended = Convert.ToInt32(Superlist[2]);
            SuperBClass.WinQb = Superlist[3];
            SuperBClass.WinCoach = Superlist[4];
            SuperBClass.WinTeam = Superlist[5];
            SuperBClass.WinPoints = Convert.ToInt32(Superlist[6]);
            SuperBClass.LoseQb = Superlist[7];
            SuperBClass.LoseCoach = Superlist[8];
            SuperBClass.losingTeam = Superlist[9];
            SuperBClass.PointLoss = Convert.ToInt32(Superlist[10]);
            SuperBClass.Mvp = Superlist[11];
            SuperBClass.Spread = Convert.ToInt32(Superlist[6]) - Convert.ToInt32(Superlist[10]);
            SuperBClass.hostStadium = Superlist[12];
            SuperBClass.city = Superlist[13];
            SuperBClass.state = Superlist[14];
            return SuperBClass;


        }
    }
}

