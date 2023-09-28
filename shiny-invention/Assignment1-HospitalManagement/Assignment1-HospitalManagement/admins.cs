using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1_HospitalManagement
{
    //containter class
    class admins
    {
        // local storage for use of c# functions
        private List<admin> adminsList;

        public admins()
        {
            adminsList = new List<admin>();
        }

        //Loads all Doctors from the txt database into local memory. 
        public void loadFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines != null)
                {
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');

                        admin a = new admin(
                                data[0],             // ID
                                data[1],            //First Name
                                data[2],            //Last Name
                                data[3],            //Email
                                data[4],            //Address
                                data[5],             //PhoneNumber
                                data[6]              //password
                        );

                        adminsList.Add(a);
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
        public admin loginSequence(String id)
        {
            Boolean valid = false;
            string password = "";
            admin a = findOne(id);
            if (a != null)
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
                                if (validate(a, password) == false)
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
                    return a;
                }

            }
            return null;
        }

        private Boolean validate(admin a, string password)
        {
            return a.Password == password;
        }

        //Calls the to string method for each doctor in d. 
        public void display()
        {
            foreach (admin a in adminsList)
            {
                Console.WriteLine(a.ToString());
            }
        }
        //displays the string method for 1 valid doctor via its ID
        public void displayOne(String id)
        {

            admin displayOne = adminsList.Find(admin => admin.Id == id);

            if (displayOne != null)
            {
                displayOne.ToString();
            }
            else
            {
                Console.WriteLine("no Such admin by ID = {0}", id);
            }

        }
        public admin findOne(String id)
        {
            admin findOne = adminsList.Find(admin => admin.Id == id);

            return findOne;
        }

        //Adds one and writes one doctor to the local and contingent storage systems. 
        // ID is auto validated using +1 methodology. 
        // might switch to a numeric only system IE 333 == doctor. 
        // maybe even a check sum system

        public void addOne(string firstName, string lastName, string email, string address, string phoneNumber, string password)
        {
            String id;
            if (adminsList.Count > 0)
            {
                id = adminsList.Last().Id; // auto incriment
                int idNum = Convert.ToInt32(id.Substring(1)) + 1;
                id = "3" + Convert.ToString(idNum);

            }
            else
            {
                id = "31000";
            }
            admin a = new admin(id, firstName, lastName, email, address, phoneNumber, password);
            adminsList.Add(a);
            writeOne(a);

        }
        public static void writeOne(admin a)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(fileManager.adminsPath))
                {
                    string doctorData = $"{a.Id},{a.FirstName},{a.LastName},{a.Email},{a.Address},{a.PhoneNumber},{a.Password}";
                    writer.WriteLine(doctorData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void deleteOne(admin a)
        {

            if (a != null)
            {
                adminsList.Remove(a);
            }
            else
            {
                Console.WriteLine("no such admin exists");
            }
            //

        }

        // Completely overwrite the file in the system with the contents of the local storage. 
        // should be called upon exit.
        public void OverWrite()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileManager.adminsPath))
                {
                    foreach (admin a in adminsList)
                    {
                        string adminData = $"{a.Id},{a.FirstName},{a.LastName},{a.Email},{a.Address},{a.PhoneNumber},{a.Password}";
                        writer.WriteLine(adminData);
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
