using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_HospitalManagement
{
    class adminMenu
    {

        private Boolean active = false;

        public adminMenu()
        {

        }

        //the ADMIN menu allows the administrator to list and add doctors and patients to the system 
        //any new data addedd will be appended to text files upon logging out or exiting the system. 
        public void run(admin a, fileManager _fileManager, menuSystem _menuSystem)
        {
            Console.Clear();
            header(a, true, "Admin menu");
            Console.WriteLine("1. List all doctors");
            Console.WriteLine("2. Check doctor details");
            Console.WriteLine("3. List all  patients");
            Console.WriteLine("4. Check patient details");
            Console.WriteLine("5. Add doctor");
            Console.WriteLine("6. Add patient");
            Console.WriteLine("7. Logout");
            Console.WriteLine("8. Exit application");
            Console.Write("Enter a numeric selection and then press enter: ");
            string Input = Console.ReadLine();
            if (int.TryParse(Input, out int key))
            {
                switch (key)
                {
                    case 1:
                        Console.Clear();
                        header(a, false, "All Doctors");
                        _menuSystem.headers.doctorHeader();
                        _fileManager.doctors.display();
                        _menuSystem.headers.doctorFooter();
                        Console.WriteLine("Press any key to return");
                        Console.ReadKey();
                        break;

                    case 2:

                        Console.Clear();
                        Boolean searchingDoc = true;
                        while (searchingDoc)
                        {

                            Console.Clear();
                            header(a, false, "Doctor Details");

                            Console.Write("Doctor ID: ");
                            Input = Console.ReadLine();

                            if (int.TryParse(Input, out key))
                            {
                                doctor search = _fileManager.doctors.findOne(Input);
                                if (search != null)
                                {
                                    _menuSystem.headers.doctorHeader();
                                    Console.WriteLine(search.ToString());
                                    _menuSystem.headers.doctorFooter();
                                }
                                else
                                {
                                    Console.WriteLine("No doctor by the ID: {0}", Input);
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid Input, please enter a valid doctor ID.");
                                _menuSystem.headers.dotdotdot();

                            }
                            Console.WriteLine();
                            Console.WriteLine("press any key to contine");
                            Console.WriteLine();
                            Console.WriteLine("press Q to return to admin menu");

                            ConsoleKeyInfo quit = Console.ReadKey();   // Handles the search loop by checking specific user input. 
                            if (quit.KeyChar == 'q' || quit.KeyChar == 'Q')
                            {
                                searchingDoc = false;
                            }
                        }
                        break;

                    case 3:
                        Console.Clear();
                        header(a, false, "All patients");
                        _menuSystem.headers.patientHeader();
                        _fileManager.patients.display(_fileManager);
                        _menuSystem.headers.patientFooter();
                        Console.WriteLine("Press any key to return");
                        Console.ReadKey();
                        break;

                    case 4:

                        Console.Clear();
                        Boolean searchingPat = true;
                        while (searchingPat)
                        {

                            Console.Clear();
                            header(a, false, "Patient Detials");

                            Console.Write("Patient ID: ");
                            Input = Console.ReadLine();

                            if (int.TryParse(Input, out key))
                            {
                                patient search = _fileManager.patients.findOne(Input);
                                if (search != null)
                                {
                                    _menuSystem.headers.patientHeader();
                                    Console.WriteLine(_fileManager.patients.PrintOne(_fileManager,search)); // wrapper function is needed to grab the doctors full name from ID
                                    _menuSystem.headers.patientFooter();
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
                            Console.WriteLine("press Q to return to admin menu");

                            ConsoleKeyInfo quit = Console.ReadKey();   // Handles the search loop by checking specific user input. 
                            if (quit.KeyChar == 'q' || quit.KeyChar == 'Q')
                            {
                                searchingPat = false;
                            }
                        }
                        break;

                    case 5:

                        AddSequence(_menuSystem, _fileManager, a, 1);
                        break;

                    case 6:
                        AddSequence(_menuSystem, _fileManager, a, 2);
                        break;

                    case 7:
                        _menuSystem.Active = true;
                        this.active = false;
                        _menuSystem.UserA = null;
                        Console.WriteLine("--- Logging you out ---");
                        _fileManager.writeQuit();
                        _menuSystem.headers.dotdotdot();
                        break;
                    case 8:
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

        }

        private void header(admin a, Boolean greeting, string MenuTitle)
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
                Console.WriteLine("Welcome to the Hospital Management Systems Administration Menu {0} {1}", a.FirstName, a.LastName);
            }
        }

        private bool testForSymbols(string Name)
        {
            foreach (char c in Name)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }

            return false;
        }
        private bool testForSymbolsWithSpaces(string Name)
        {
            foreach (char c in Name)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    if (c != ' ')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void AddSequence(menuSystem _menuSystem,fileManager _fileManager, admin a, int Type)
        {
            Boolean adding = true;
            string firstName = null;
            string lastName = null;
            string email = null;
            string phoneNum = null;
            string streetNum = null;
            string street = null;
            string city = null;
            string state = null;
            int step = 0;

            while (adding)
            {
                Console.Clear();
                if (Type == 1)
                {
                    header(a, false, "Add Doctor");
                }
                else
                {
                    header(a, false, "Add Patient");
                }
                Console.WriteLine("First Name: {0}", firstName);
                Console.WriteLine("First Last: {0}", lastName);
                Console.WriteLine("Email: {0}", email);
                Console.WriteLine("Phone Number: {0}", phoneNum);
                Console.WriteLine("Street Number: {0}", streetNum);
                Console.WriteLine("Street: {0}", street);
                Console.WriteLine("City: {0}", city);
                Console.WriteLine("State: {0}", state);
                Console.WriteLine();

                if (step == 0)
                {
                    Console.Write("Enter First Name: ");
                    String userInput = Console.ReadLine();
                    bool intTest = int.TryParse(userInput, out int numTest);
                    bool symbolTest = testForSymbols(userInput);
                    if (!string.IsNullOrEmpty(userInput) && userInput.Length > 1 && intTest == false && symbolTest == false)
                    {
                        firstName = userInput;
                        step++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid first name");
                        _menuSystem.headers.dotdotdot();
                    }
                }
                if (step == 1)
                {
                    Console.Write("Enter Last Name: ");
                    String userInput = Console.ReadLine();
                    bool Test = int.TryParse(userInput, out int numTest);
                    bool symbolTest = testForSymbols(userInput);
                    if (!string.IsNullOrEmpty(userInput) && userInput.Length > 1 && Test == false && symbolTest == false)
                    {
                        lastName = userInput;
                        step++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid last name");
                        _menuSystem.headers.dotdotdot();
                    }
                }
                if (step == 2)
                {
                    Console.Write("Enter Email: ");
                    String userInput = Console.ReadLine();
                    bool Test = int.TryParse(userInput, out int numTest);
                    if (!string.IsNullOrEmpty(userInput) && userInput.Length > 10 && Test == false && userInput.Contains('@') && userInput.Contains(".com"))
                    {
                        email = userInput;
                        step++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid email name");
                        _menuSystem.headers.dotdotdot();
                    }
                }
                if (step == 3)
                {
                    Console.Write("Enter Phone Number: ");
                    String userInput = Console.ReadLine();
                    bool Test = int.TryParse(userInput, out int numTest);
                    if (!string.IsNullOrEmpty(userInput) && userInput.Length > 7 && Test == true)
                    {
                        phoneNum = userInput;
                        step++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid phone Number");
                        _menuSystem.headers.dotdotdot();
                    }
                }
                if (step == 4)
                {
                    Console.Write("Enter Street Number: ");
                    String userInput = Console.ReadLine();
                    bool Test = int.TryParse(userInput, out int numTest);
                    if (!string.IsNullOrEmpty(userInput) && userInput.Length < 6 && Test == true)
                    {
                        streetNum = userInput;
                        step++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid street Number");
                        _menuSystem.headers.dotdotdot();
                    }

                }
                if (step == 5)
                {
                    Console.Write("Enter Street Name: ");
                    String userInput = Console.ReadLine();
                    bool Test = int.TryParse(userInput, out int numTest);
                    bool symbolTest = testForSymbolsWithSpaces(userInput);
                    if (!string.IsNullOrEmpty(userInput) && userInput.Length > 1 && Test == false && symbolTest == false)
                    {
                        street = userInput;
                        step++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid Street name");
                        _menuSystem.headers.dotdotdot();
                    }

                }
                if (step == 6)
                {
                    Console.Write("Enter City: ");
                    String userInput = Console.ReadLine();
                    bool Test = int.TryParse(userInput, out int numTest);
                    bool symbolTest = testForSymbols(userInput);
                    if (!string.IsNullOrEmpty(userInput) && userInput.Length > 3 && Test == false && symbolTest == false)
                    {
                        city = userInput;
                        step++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid City name");
                        _menuSystem.headers.dotdotdot();
                    }
                }
                if (step == 7)
                {
                    Console.Write("Enter State: ");
                    String userInput = Console.ReadLine();
                    bool Test = int.TryParse(userInput, out int numTest);
                    bool symbolTest = testForSymbols(userInput);
                    if (!string.IsNullOrEmpty(userInput) && userInput.Length > 2 && Test == false && symbolTest == false)
                    {
                        state = userInput;
                        step++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid State");
                        _menuSystem.headers.dotdotdot();
                    }
                }
                if (step == 8 && Type == 1)
                {
                    string Address = streetNum + " " + street + " " + city + " " + state;
                    _fileManager.doctors.addOne(firstName, lastName, email, Address, phoneNum, "12345");
                    Console.WriteLine("doctor {0} {1} has been added to the system.", firstName, lastName);
                    Console.WriteLine("press any key to return");
                    Console.ReadKey();
                    break;
                }
                if (step == 8 && Type == 2)
                {
                    string Address = streetNum + " " + street + " " + city + " " + state;
                    _fileManager.patients.addOne(firstName, lastName, email, Address, phoneNum, "12345", null);
                    Console.WriteLine("patient {0} {1} has been added to the system.", firstName, lastName);
                    Console.WriteLine("press any key to return");
                    Console.ReadKey();
                    break;
                }
            }
        }

        public bool Active { get => active; set => active = value; }
    }

}