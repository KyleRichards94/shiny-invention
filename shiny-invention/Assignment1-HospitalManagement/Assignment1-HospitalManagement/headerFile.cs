using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1_HospitalManagement
{
    //single class
    class headerFile
    {
        public headerFile()
        {

        }

        public void patientHeaderNoDoc()
        {
            Console.WriteLine("┌──────────────────────┬───────────────────────────┬──────────────┬────────────────────────────────┐");
            Console.WriteLine("│ Patient Name         │ Email                     │ Phone Number │ Address                        │");
            Console.WriteLine("├──────────────────────┼───────────────────────────┼──────────────┼────────────────────────────────┤");
        }
        public void patientFooterNoDoc()
        {
            Console.WriteLine("└──────────────────────┴───────────────────────────┴──────────────┴────────────────────────────────┘");
        }
        public void patientHeader()
        {
            Console.WriteLine("┌──────────────────────┬──────────────────────┬────────────────────────────────┬──────────────┬────────────────────────────────┐");
            Console.WriteLine("│ Patient Name         │ Doctor Name          │ Email                          │ Phone Number │ Address                        │");
            Console.WriteLine("├──────────────────────┼──────────────────────┼────────────────────────────────┼──────────────┼────────────────────────────────┤");
        }
        public void patientFooter()
        {
            Console.WriteLine("└──────────────────────┴──────────────────────┴────────────────────────────────┴──────────────┴────────────────────────────────┘");
        }
        public void doctorHeader()
        {
            Console.WriteLine("┌──────────────────────┬────────────────────────────────┬──────────────┬────────────────────────────────┐");
            Console.WriteLine($"│ {"Full Name",-20} │ { "email",-30} │ {"phoneNumber",-12} │ {"address", -30} │");
            Console.WriteLine("├──────────────────────┼────────────────────────────────┼──────────────┼────────────────────────────────┤");
        }
        public void doctorFooter()
        {
            Console.WriteLine("└──────────────────────┴────────────────────────────────┴──────────────┴────────────────────────────────┘");
        }
        public void appointmentHeader()
        {
            Console.WriteLine("┌──────────────────────┬──────────────────────┬────────────────────────────────────────────────────┐");
            Console.WriteLine($"│ {"Doctor",-20} │ { "Patient",-20} │ {"Description",-50} │");
            Console.WriteLine("├──────────────────────┼──────────────────────┼────────────────────────────────────────────────────┤");

        }
        public void appointmentFooter()
        {
            Console.WriteLine("└──────────────────────┴──────────────────────┴────────────────────────────────────────────────────┘");
        }
        
        public void dotdotdot()
        {
            Console.Write(" .");
            System.Threading.Thread.Sleep(300);
            Console.Write(" .");
            System.Threading.Thread.Sleep(300);
            Console.Write(" .");
            System.Threading.Thread.Sleep(300);
        }
    }
}