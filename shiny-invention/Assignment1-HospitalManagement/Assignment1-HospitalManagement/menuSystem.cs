using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1_HospitalManagement
{
    class menuSystem
    {
        // menu system begins its state as active
        private Boolean active = true;

        // track User account. 
        private doctor userD;
        private patient userP;
        private admin userA;

        private doctorMenu doctorMenu;
        private patientMenu patientMenu;
        private adminMenu adminMenu;

        //Headers file 
        public headerFile headers = new headerFile();

        public menuSystem()
        {

        }
        
        public Boolean Active {get => active; set => active = value;}
        public doctor UserD {get => userD; set => userD = value;}
        public patient UserP {get => userP; set => userP = value;}
        public admin UserA {get => userA; set => userA = value;}
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///// Menu system documentation /////////////////////////////////////////////////////////////////////////////
        /////// the menu systems work through a switch of "active" and are called through the menu systems main class. 
        /// for congruency purposes no menu system class will have personal memory allocation for _filemanager 
        /// as it will be passed down the chain as the user progrossess. 
        ///                     Menu system 
        ///                     /    |    \
        ///              Patient   doctor   admin
        ///           The menu systems core loop operates in the program.CS file
        ///           where the menusystem is called each cpu cycle given access to the filesystem class and the menusystem. 
        public void run(fileManager _fileManager)
        {

            //Each Frame run the specific menu that is currently marked as active
            if(doctorMenu != null && doctorMenu.Active)
            {
                doctorMenu.run(userD, _fileManager, this);
            }

            if(patientMenu != null && patientMenu.Active)
            {
                patientMenu.run(userP, _fileManager, this);
            }

            if(adminMenu != null && adminMenu.Active)
            {
                adminMenu.run(userA, _fileManager, this);
            }
            //if the menu system is active run the login sequence
            if (active)
            {
                Console.Clear();
                //Login sequence
                header();
                Console.WriteLine("");
                Console.WriteLine("Welcome to the KCR .NET hospital Management System");
                Console.WriteLine("");
                Console.WriteLine("login to Contine");
                Console.WriteLine("");
                Console.Write("     Enter your Login ID: ");

                    // Login sequence; 
                    try { 
                        string id = Console.ReadLine();
                        Char Profile = id[0];
            
                        // Determine the profile from the user ID's first number
                        if(Profile == '1')
                         {
                            userD = _fileManager.doctors.loginSequence(id);

                            if(userD == null)
                                {   
                                    Console.WriteLine("Invalid Credential");
                                    headers.dotdotdot();

                                }
                                else
                                {
                                    doctorMenu = new doctorMenu();

                                    Console.WriteLine("     Valid login");

                                    doctorMenu.Active = true;

                                     headers.dotdotdot();

                                    active = false;
                                }
                         } 

                         else if (Profile == '2')
                        {
                            userP = _fileManager.patients.loginSequence(id);

                            if(userP == null)
                            {
                                Console.WriteLine("     Invalid Credential");
                                headers.dotdotdot(); 
                            }
                            else
                            {
                                patientMenu = new patientMenu();

                                Console.WriteLine("     Valid login");

                                patientMenu.Active = true;

                                headers.dotdotdot();

                                active = false;
                            }
                                ////
                            } 
                            else if( Profile == '3')
                            {
                                userA = _fileManager.admins.loginSequence(id);

                                if (userA == null)
                                {
                                    Console.WriteLine("     Invalid Credential");
                                    headers.dotdotdot();
                                }
                                else
                                {
                                    adminMenu = new adminMenu();

                                    Console.WriteLine("     Valid login");

                                    adminMenu.Active = true;

                                    headers.dotdotdot();

                                    active = false;
                                }
                            /////
                        }
                        else
                        {
                            Console.WriteLine("Invalid Credential");
                            headers.dotdotdot();

                        }
                    } catch (Exception ex)
                    {
                        Console.WriteLine("No input detected" + ex.Message);
                        headers.dotdotdot();
                    }
            }
        }


 

        private void header()
        {
            Console.WriteLine("                    ┌────────────────────────────────────────────────┐ ");
            Console.WriteLine("                    │                                     v.1-2023   │ ");
            Console.WriteLine("                    │      .NET Hosptial Management System           │ ");
            Console.WriteLine("                    │    ───────────────────────────────────────     │ ");
            Console.WriteLine("                    │                  Login Menu                    │ ");
            Console.WriteLine("                    │                                   K.c.R  tech  │ ");
            Console.WriteLine("                    └────────────────────────────────────────────────┘ ");
        }

    }
}