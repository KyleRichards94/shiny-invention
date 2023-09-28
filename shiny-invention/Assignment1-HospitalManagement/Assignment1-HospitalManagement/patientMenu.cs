using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_HospitalManagement
{
    class patientMenu
    {
        private Boolean active = false;

        public patientMenu() { }

        public void run(patient p, fileManager _fileManager,
                        menuSystem _menuSystem)
        {
            Console.Clear();
            header(p, true, "Patient Menu");
            Console.WriteLine("1. List my details");
            Console.WriteLine("2. List my doctors details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Exit application");
            Console.Write("Enter a numeric selection and then press enter: ");
            string Input = Console.ReadLine();
            if (int.TryParse(Input, out int key))
            {
                switch (key)
                {
                    case 1:
                        Console.Clear();
                        header(p, false, "My Details");
                        Console.WriteLine();
                        Console.WriteLine("{0} {1}'s Details", p.FirstName, p.LastName);
                        Console.WriteLine();
                        Console.WriteLine("Patient ID: {0}", p.Id);
                        Console.WriteLine("Full Name: {0} {1} ", p.FirstName, p.LastName);
                        Console.WriteLine("Address: {0}", p.Address);
                        Console.WriteLine("Email: {0}", p.Email);
                        Console.WriteLine("phone: {0}", p.PhoneNumber);
                        _menuSystem.headers.dotdotdot();
                        Console.WriteLine("Any key to return...");
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        header(p, false, "My Doctor");

                        if (_fileManager.doctors.findOne(p.DoctorID) != null)
                        {
                            Console.WriteLine("");
                            _menuSystem.headers.doctorHeader();
                            Console.WriteLine(
                                _fileManager.doctors.findOne(p.DoctorID).ToString());
                            _menuSystem.headers.doctorFooter();
                            Console.WriteLine("Any key to return...");
                            Console.ReadKey();
                        }
                        else
                        {
                            _fileManager.doctors.assignPatient(p, this, _menuSystem);
                        }
                        break;

                    case 3:
                        Console.Clear();
                        header(p, false, "My Appointments");
                        if (_fileManager.doctors.findOne(p.DoctorID) != null)
                        {
                            List<appointment> patientAppointments = _fileManager.appointments.findMyPatientAppointmentsByID(p.Id, p.DoctorID);
                            if (patientAppointments.Count == 0)
                            {
                                Console.WriteLine(
                                    "You do not have any appointments, feel free to create a new one from the main menu");
                            }
                            else
                            {
                                _menuSystem.headers.appointmentHeader();
                                _fileManager.appointments.PrintList(_fileManager,
                                                                    patientAppointments);
                                _menuSystem.headers.appointmentFooter();
                                Console.WriteLine("Any key to return...");
                            }
                        }
                        else  //if the patient has no doctor send them to the assign patient form.
                        {
                            _fileManager.doctors.assignPatient(p, this, _menuSystem);
                        }

                        Console.ReadKey();
                        break;

                    case 4:
                        doctor d = _fileManager.doctors.findOne(p.DoctorID);
                        Console.Clear();
                        header(p, false, "Book Appointment");
                        if (_fileManager.doctors.findOne(p.DoctorID) != null)
                        {
                            try
                            {
                                bool valid = false;
                                bool quit = false;
                                string userInput = "";
                                while (!valid && !quit)
                                {
                                    Console.Clear();
                                    header(p, false, "Book Appointment");
                                    Console.WriteLine(
                                        "You are booking an appointment with {0} {1}",
                                        d.FirstName, d.LastName);
                                    Console.Write("Reason for booking this appointment: ");
                                    userInput = Console.ReadLine();
                                    if(userInput.Equals("quit"))
                                    {
                                        quit = true;
                                        break;
                                    }else
                                    if (!string.IsNullOrEmpty(userInput) &&
                                        userInput.Length > 3)
                                    {
                                        valid = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("enter a valid reason.");

                                        Console.WriteLine("or type any quit to quit.");
                                        _menuSystem.headers.dotdotdot();
                                        _menuSystem.headers.dotdotdot();

                                    }
                                }
                                if (valid)
                                {
                                    _fileManager.appointments.addOne(p, userInput);
                                    Console.Clear();
                                    header(p, false, "Book Appointment");
                                    Console.WriteLine("You are booked with {0} {1} for:",
                                                      d.FirstName, d.LastName);
                                    Console.WriteLine(userInput);

                                    Console.WriteLine();

                                    _menuSystem.headers.dotdotdot();
                                }

                                Console.WriteLine("press any key to return...");

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(
                                    "please breifly describe your appointment details.");
                            }
                        }
                        else //no doctor assigned so send them to the assign form
                        {
                            _fileManager.doctors.assignPatient(p, this, _menuSystem);
                        }

                        Console.ReadKey();
                        break;

                    case 5:
                        _menuSystem.Active = true;
                        this.active = false;
                        _menuSystem.UserP = null;
                        Console.WriteLine("--- Logging you out ---");
                        _fileManager.writeQuit();
                        _menuSystem.headers.dotdotdot();
                        break;

                    case 6:
                        Console.WriteLine("cya chump");
                        _fileManager.writeQuit();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Pelase enter a valid number, 1-6");
                        _menuSystem.headers.dotdotdot();
                        break;
                }
            }
        }

        public void header(patient p, Boolean greeting, string MenuTitle)
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
                Console.WriteLine(
                    "Welcome to the Hospital Management Systems Patient Menu {0} {1}",
                    p.FirstName, p.LastName);
            }
        }

        public bool Active
        {
            get => active;
            set => active = value;
        }
    }

}