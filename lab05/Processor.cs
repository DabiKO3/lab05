using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Lab05
{
    public class Processor
    {

        public string name { get; set; }
        public string manufacturer { get; set; }
        public int core { get; set; }
        public double frequency { get; set; }
        public double tdp { get; set; }
        public double performancePerCore { get; set; }
        public bool multiPrecision { get; set; }
        public bool energySaving { get; set; }

        public double CalculateTotalPerformance()
        {
            return core * performancePerCore;
        }

        public double yearIncomePerInhabitant
        {
            get
            {
                return CalculateTotalPerformance();
            }

        }
        public Processor()
        {
        
        }

        public Processor(string Name, string Manufacturer, int Core, double Frequency, double TDP, double PerformancePerCore, bool MP,
                bool ES )
            {
                name = Name;
                manufacturer = Manufacturer;
                core = Core;
                frequency = Frequency;
                tdp = TDP;
                performancePerCore = PerformancePerCore;
                multiPrecision = MP;
                energySaving = ES;
            }
        }
    
}
