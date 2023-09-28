using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assignment1_HospitalManagement
{
    class doctorMenu
    {
        //private doctor user;
        //private fileManager _fileManager;

        private Boolean active = false;

        public doctorMenu()
        {

        }
        
        // Doctor menu 
        // the doctors menu will find and load a new list of patients and appointments specific to the logged in doctor D. 
        // the doctor has 5 main options all selectable by pressing any corresponding number. 
        // when the doctor logs out or exits the system their information will be persisted in text files. 
        public void run(doctor d, fileManager _fileManager, menuSystem _menuSystem )
        {
            List<patient> patientSearch = _fileManager.patients.findMyPatients(d.Id);
            List<appointment> appointmentSearch = _fileManager.appointments.findDoctorAppointments(d.Id);

            Console.Clear();
            header(d, true, "Doctor Menu");

            Console.WriteLine("1. List My Details");
            Console.WriteLine("2. List My Patients");
            Console.WriteLine("3. List My Appointments");
            Console.WriteLine("4. Check patient by ID");
            Console.WriteLine("5. List appointments with patient by ID");
            Console.WriteLine("6. Logout");
            Console.WriteLine("7. Exit application");
            Console.Write("Enter a numeric selection and then press enter: ");

                string Input = Console.ReadLine();

                if(int.TryParse(Input, out int key)) {
                    switch (key)
                    {
                        case 1:
                            // list the details of the current user
                            Console.Clear();
                            header(d, false, "My Details");

                            _menuSystem.headers.doctorHeader();
                            Console.WriteLine(d.ToString());
                            _menuSystem.headers.doctorFooter();
                            Console.WriteLine("Any key to return...");
                            Console.ReadKey();

                            break;

                        case 2:
                            // List all patient where patient.doctorID == d.Id;
                            
                            Console.Clear();
                            header(d, false, "My Patients");
                        if (patientSearch == null)
                        {
                            Console.WriteLine("no patients to show.");
                        }
                        else
                        {

                            _menuSystem.headers.patientHeaderNoDoc();
                            foreach (patient p in patientSearch)
                            {
                                Console.WriteLine(p.ToString());
                            }
                            _menuSystem.headers.patientFooterNoDoc();
                        }

                        Console.WriteLine("Any key to return...");
                        Console.ReadKey();
                            break;

                        case 3:

                            Console.Clear();
                            header(d, false, "All Appointments");
                        // Print appointment header
                             _menuSystem.headers.appointmentHeader();
                             _fileManager.appointments.PrintList(_fileManager, appointmentSearch);
                            _menuSystem.headers.appointmentFooter();

                            Console.WriteLine("any key to continue");
                            Console.ReadKey();
                        // List all appointments were appointment.doctorID == d.Id;
                        break;

                        case 4:
                        // accept and test user input; 
                        
                            Boolean searchingPat = true;
                            while (searchingPat)
                            {

                                Console.Clear();
                                header(d, false, "Search Patient");

                                Console.Write("Patient ID: ");
                                Input = Console.ReadLine();

                                if (int.TryParse(Input, out key))
                                { 
                                    patient search = _fileManager.patients.findOne(Input);
                                    if (search != null)
                                    {
                                    _menuSystem.headers.patientHeaderNoDoc();
                                        Console.WriteLine(search.ToString());
                                    _menuSystem.headers.patientFooterNoDoc();
                                    }
                                    else
                                    {
                                        Console.WriteLine("No patient by the ID: {0}", Input);
                                    }
                                }
                                else
                                {
                                Console.WriteLine();
                                    Console.WriteLine("Invalid Input, please enter a valid patient ID.");
                                    _menuSystem.headers.dotdotdot();
                                    
                                }
                                Console.WriteLine();
                                Console.WriteLine("press any key to contine");
                                Console.WriteLine();
                                Console.WriteLine("press Q to return to doctor menu");
                                 
                                ConsoleKeyInfo quit = Console.ReadKey();   // Handles the search loop by checking specific user input. 
                                if (quit.KeyChar == 'q' || quit.KeyChar == 'Q')
                                {
                                    searchingPat = false;
                                }
                            }

                            // then 
                            // show patient where patient id = user input.
                            break;

                        case 5:
                            Boolean searchingApt = true;
                            while (searchingApt)
                            {

                                Console.Clear();
                                header(d, false, "Appointments With");
                                Console.WriteLine("To find specific appointments for your appointed patients, please eanter: ");
                                Console.Write("Patient ID: ");
                                Input = Console.ReadLine();

                                if (int.TryParse(Input, out key))
                                {
                                    patient CheckValidPatient = _fileManager.patients.findOne(Input);
                                    if (CheckValidPatient != null)
                                    {
                                        List<appointment> searchApts = _fileManager.appointments.findMyPatientAppointmentsByID(Input, d.Id);
                                        if (searchApts.Count != 0)
                                        {
                                            _menuSystem.headers.appointmentHeader();
                                            _fileManager.appointments.PrintList(_fileManager, searchApts);
                                            _menuSystem.headers.appointmentFooter();
                                        } 
                                        else
                                        {
                                            Console.WriteLine("you have no appointments booked with patient {0} {1} {2}", CheckValidPatient.Id, CheckValidPatient.FirstName, CheckValidPatient.LastName);
                                        }
                                    } 
                                    else
                                    {
                                    Console.WriteLine("No patient by the ID: {0}", Input);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid Input, please enter a valid patient ID.");
                                    _menuSystem.headers.dotdotdot();

                                }
                                Console.WriteLine();
                                Console.WriteLine("press any key to contine");
                                Console.WriteLine();
                                Console.WriteLine("press Q to return to doctor menu");
                                ConsoleKeyInfo quit = Console.ReadKey();   
                                if (quit.KeyChar == 'q' || quit.KeyChar == 'Q')
                                {
                                    searchingApt = false;
                                }
                        }
                        break;
                
                        case 6:
                            _menuSystem.Active = true;
                            this.active = false; 
                            _menuSystem.UserD = null;
                            Console.WriteLine("--- Logging you out ---");
                            _fileManager.writeQuit();
                            _menuSystem.headers.dotdotdot();
                        break;
                
                        case 7:
                            Console.WriteLine("cya chump");
                            _fileManager.writeQuit();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Pelase enter a valid number, 1-7");
                            Console.ReadKey();
                        break;

                    }
                } 
                else
                {
                      Console.WriteLine("Pelase enter a valid number, 1-7, letters will not be accepted.");
                      _menuSystem.headers.dotdotdot();
            }     

        }

        private void header(doctor d, Boolean greeting, string MenuTitle)
        {
            Console.WriteLine("                    ┌────────────────────────────────────────────────┐ ");
            Console.WriteLine("                    │                                     v.1-2023   │ ");
            Console.WriteLine("                    │      .NET Hosptial Management System           │ ");
            Console.WriteLine("                    │    ───────────────────────────────────────     │ ");
            Console.WriteLine($"{"                    │                  "} { MenuTitle,-18}{ "           │ "}");
            Console.WriteLine("                    │                                   K.c.R  tech  │ ");
            Console.WriteLine("                    └────────────────────────────────────────────────┘ ");
            Console.WriteLine("");
            if (greeting)
            {
                Console.WriteLine("Welcome to the Hospital Management Systems Doctor Menu {0} {1}", d.FirstName, d.LastName);
            }
        }
        
        public bool Active { get => active; set => active = value;}
    }

}