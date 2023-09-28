using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment1_HospitalManagement
{
    

    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 25);
            bool systemRunning = true;
           
            fileManager _fileManager = new fileManager();  //Instanciate a new local storage system
            menuSystem _menuSystem = new menuSystem();   //Instanciate a new menu system for the session
            
            _fileManager.initializeFM(); //Inintialize the program from text files

            // Main loop will call the menu system each run cycle and pass it the file
            // management system in order to avoid stacking file manager instances
            while (systemRunning)
            {
                _menuSystem.run(_fileManager);
            }
               
            //Write all data to text
             _fileManager.writeQuit();

        }

    }

}


