using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1_HospitalManagement
{
    //containter class
    class doctors 
    {
        // local storage for use of c# functions
        private List<doctor> doctorsList;

        public doctors()
        {
            doctorsList = new List<doctor>();
        }
        
        //Loads all Doctors from the txt database into local memory. 
        public void loadFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                if(lines != null) {
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');

                        doctor d = new doctor(
                                data[0],             // ID
                                data[1],            //First Name
                                data[2],            //Last Name
                                data[3],            //Email
                                data[4],            //Address
                                data[5],             //PhoneNumber
                                data[6]              //password
                        );

                       doctorsList.Add(d);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        
        // guides the user to enter their password upon providing an ID, returns null if invalid 
        // returns a doctor class if is valid. 
        public doctor loginSequence(String id)
        {
           Boolean valid = false;
           string password = "";
           doctor d =  findOne(id);
            if (d != null)
            {
                try
                {

                    Console.WriteLine();
                    Console.Write("     Enter your password: ");
                    do
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
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
                                if (validate(d, password) == false)
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
                    return d;
                }

            }
            return null;
        }

        public void assignPatient(patient p, patientMenu PM, menuSystem MS)
        {
            Random rand = new Random();
            int len = (doctorsList.Count < 10) ? doctorsList.Count : 10;
            int n = doctorsList.Count;
            List<doctor> tempList = doctorsList;

            //Shuffle the doctor list within a new temporary list if there are more than 10 doctors

            if (n > 10)
            {
                while (n > 1)
                {
                    n--;
                    int next = rand.Next(n + 1);
                    doctor tempD = tempList[n];
                    tempList[n] = tempList[next];
                    tempList[next] = tempD;

                }
            }
            

            // and then show 10 or all the doctors randomized
            while (findOne(p.DoctorID) == null)
            {
                Console.Clear();
                PM.header(p, false, "Assign Doctor");
                Console.WriteLine("You currently do not have an assigned doctor");
                MS.headers.dotdotdot();
                Console.WriteLine("enter a number corresponding to the doctor of your choice ");
                Console.WriteLine("");

                MS.headers.doctorHeader();
                for (int i = 0; i < len; i++)
                {
                    Console.WriteLine( tempList[i].ToString() + ": " + i + 1 );
                }
                MS.headers.doctorFooter();
                Console.Write("Choice: ");
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out int key))
                {
                    doctor d = tempList[Convert.ToInt32(Input)-1];
                    p.DoctorID = d.Id;
                    Console.WriteLine();
                    Console.WriteLine("Your doctor has been assigned to {0} {1} with ID: {2}.", d.FirstName, d.LastName, d.Id);
                    Console.WriteLine();
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                } 
                else
                {
                    Console.WriteLine("Enter a valid number from the doctors list to chose a doctor");
                    MS.headers.dotdotdot();
                }
            }

        }

        private Boolean validate(doctor d, string password)
        {
            return d.Password == password;
        }

        //Calls the to string method for each doctor in d. 
        public void display()
        {
            foreach(doctor d in doctorsList)
            {
                Console.WriteLine(d.ToString());
            }
        }
        //displays the string method for 1 valid doctor via its ID
        public void displayOne(String id)
        {
          
            doctor displayOne = doctorsList.Find(doctor => doctor.Id == id);

            if (displayOne != null)
            {
                displayOne.ToString();
            } 
            else
            {
                Console.WriteLine("no Such doctor by ID = {0}", id);
            }

        }
        public doctor findOne(String id)
        {
            doctor findOne = doctorsList.Find(doctor => doctor.Id == id);

            return findOne;
        }

        //Adds one and writes one doctor to the local and contingent storage systems. 
        // ID is auto validated using +1 methodology. 
        // might switch to a numeric only system IE 333 == doctor. 
        // maybe even a check sum system

        public void addOne(string firstName, string lastName, string email, string address, string phoneNumber, string password)
        {
            String id;
            if (doctorsList.Count > 0)
            {
                id = doctorsList.Last().Id; // auto incriment
                int idNum = Convert.ToInt32(id.Substring(1)) + 1;
                id = "1" + Convert.ToString(idNum);

            } else
            {
                id = "11000";
            }
            doctor d = new doctor(id, firstName, lastName, email, address, phoneNumber, password);
            doctorsList.Add(d);
            writeOne(d);
            
        }
        public static void writeOne(doctor d)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(fileManager.doctorsPath))
                {
                    string doctorData = $"{d.Id},{d.FirstName},{d.LastName},{d.Email},{d.Address},{d.PhoneNumber},{d.Password}";
                    writer.WriteLine(doctorData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void deleteOne(doctor d)
        {

            if(d != null)
            {
                doctorsList.Remove(d);
            } else
            {
                Console.WriteLine("no such doctor exists");
            }
            //
            
        }

        // Completely overwrite the file in the system with the contents of the local storage. 
        // should be called upon exit.
        public void OverWrite()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileManager.doctorsPath))
                {
                    foreach (doctor d in doctorsList)
                    {
                        string doctorData = $"{d.Id},{d.FirstName},{d.LastName},{d.Email},{d.Address},{d.PhoneNumber},{d.Password}";
                        writer.WriteLine(doctorData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }


}
