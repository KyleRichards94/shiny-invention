using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1_HospitalManagement
{
    //container class
    class patients
    {

        private List<patient> patientList;
        // list of all patients

        public patients()
        {
            patientList = new List<patient>();
        }

        public void loadFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                if(lines != null) {
                    foreach(string line in lines)
                    {
                        string[] data = line.Split(',');
                        if(data.Length == 7)
                        {
                            patient p = new patient(
                                data[0],             //ID
                                data[1],             //Fname
                                data[2],             //Lname
                                data[3],             //email
                                data[4],             // address
                                data[5],             //phone number
                                data[6],            // password
                                null               //doctorID
                            );
                            patientList.Add(p);

                        }       //// if the patient already has a doctorID assigned to them. 
                        else if(data.Length == 8)  
                        {
                            patient p = new patient(
                                data[0],
                                data[1],
                                data[2],
                                data[3],
                                data[4],
                                data[5],
                                data[6],
                                data[7]            
                            );
                            patientList.Add(p);

                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine("An error accured: " + ex.Message);
            }
        }

        public patient loginSequence(String id)
        {
            Boolean valid = false;
            string password ="";
            patient p = findOne(id);
            if (p != null)
            {
                try
                {

                    Console.WriteLine();
                    Console.Write("     Enter your password: ");
                    do
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        // Backspace Should Not Work  
                        if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                        {
                            password += key.KeyChar;
                            Console.Write("*");
                        }
                        else
                        {
                            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                            {
                                password = password.Substring(0, (password.Length - 1));
                                Console.Write("\b \b");
                            }
                            else if (key.Key == ConsoleKey.Enter)
                            {
                                if (validate(p, password) == false)
                                {
                                    Console.WriteLine("");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("");
                                    valid = true;
                                    break;
                                }
                            }
                        }
                    } while (true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (valid)
                {
                    return p;
                }

            }
            return null;
        }

        private Boolean validate(patient p, string password)
        {
            return p.Password == password;
        }

        public void display(fileManager _fileManager)
        {
            foreach(patient p in patientList)
            {
                Console.WriteLine(PrintOne(_fileManager,p));
            }
        }

        public void displayOne(String id)
        {
            patient displayOne = patientList.Find(patient => patient.Id == id);

            if(displayOne != null)
            {
                displayOne.ToString();
            }
            else
            {
                Console.WriteLine("no Such patient by ID = {0}", id);
            }

        }

        public patient findOne(string id)
        {
            patient p = patientList.Find(patient => patient.Id == id);

            return p;
        }

        public List<patient> findMyPatients(string doctorId)
        {
            return patientList.FindAll(patient => patient.DoctorID == doctorId);
        }
        //adds one new patient to to local class storage and then writes it to .txt
        public void addOne(string firstName, string lastName, string email, string address, string phoneNumber, string password, string doctorId)
        {
            String id;
            if (patientList.Count  > 0)
            {
                id = patientList.Last().Id; // auto incriment
                int idNum = Convert.ToInt32(id.Substring(1)) + 1;
                id = "2" + Convert.ToString(idNum);

            } 
            else
            {
                id = "21000";
            }
            patient p;
            if(doctorId == null)
            {
                
                 p = new patient(id, firstName, lastName, email, address, phoneNumber, password,  null);
            }
            else
            {
                p = new patient(id, firstName, lastName, email, address, phoneNumber, password, doctorId);
            }
            patientList.Add(p);
            writeOne(p);
        }

        //writes one new patient to the patients.txt file. 
        public static void writeOne(patient p)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(fileManager.patientsPath))
                {
                    string patientData;
                    if(p.DoctorID == null) { 
                        patientData = $"{p.Id},{p.FirstName},{p.LastName},{p.Email},{p.Address},{p.PhoneNumber},{p.Password}";
                    } 
                    else
                    {
                         patientData = $"{p.Id},{p.FirstName},{p.LastName},{p.Email},{p.Address},{p.PhoneNumber},{p.Password},{p.DoctorID}";
                    }
                    writer.WriteLine(patientData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }

        public void deleteOne(patient p)
        {

            if(p != null)
            {
                patientList.Remove(p);
            } 
            else
            {
                Console.WriteLine("no such patient exists");
            }
   
        }
        // Overwrites the patients .txt file
        public void OverWrite()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileManager.patientsPath))
                {
                    foreach (patient p in patientList)
                    {
                        string patientData;
                        patientData = $"{p.Id},{p.FirstName},{p.LastName},{p.Email},{p.Address},{p.PhoneNumber},{p.Password},{p.DoctorID}";
                        
                        writer.WriteLine(patientData);
                     }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public string PrintOne(fileManager _fileManager, patient p)
        {
            doctor d = _fileManager.doctors.findOne(p.DoctorID);
            if (d != null)
            {
                return $"│ {p.FirstName + " " + p.LastName,-20} │ {d.FirstName + " " + d.LastName, -20} │ {p.Email,-30} │ {p.PhoneNumber,-12} │ {p.Address,-30} │";
            }
            return $"│ {p.FirstName + " " + p.LastName,-20} │ {"No Assigned Doctor" ,-20} │ {p.Email,-30} │ {p.PhoneNumber,-12} │ {p.Address,-30} │";
        }



        //read from txt file.
        // loop methods
    }


}
