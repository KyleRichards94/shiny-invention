using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1_HospitalManagement
{
    //container class
    class appointments
    {
        //list of all appointments
        private List<appointment> appointmentList;

        public appointments()
        {
            appointmentList = new List<appointment>();
        }

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
                        appointment apt = new appointment(
                            data[0],  //the apt ID
                            data[1], // the doctor ID 11000
                            data[2], // the patient ID 21000
                            data[3] // the appointment description
                            );

                        appointmentList.Add(apt);
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }

        public List<appointment> findDoctorAppointments(string doctorId)
        {
            return appointmentList.FindAll(appointment => appointment.DoctorId == doctorId);
        }

        public List<appointment> findPatientAppointments(string patientId)
        {
            return appointmentList.FindAll(appointment => appointment.PatientId == patientId);
        }

        public List<appointment> findMyPatientAppointmentsByID(string patientId, string doctorId)
        {
            List<appointment> returnList = appointmentList.Where(appointment => appointment.DoctorId == doctorId && appointment.PatientId == patientId).ToList();
            return returnList;
        }

        public void PrintList(fileManager _fileManager, List<appointment> apts)
        {
            foreach(appointment apt in apts)
            {
                String DoctorName = _fileManager.doctors.findOne(apt.DoctorId).FirstName + " " + _fileManager.doctors.findOne(apt.DoctorId).LastName;
                String PatientName = _fileManager.patients.findOne(apt.PatientId).FirstName + " " + _fileManager.patients.findOne(apt.PatientId).LastName;


                Console.WriteLine($"│ {DoctorName,-20} │ { PatientName,-20} │ {apt.Description,-50} │");
            }
            
            
        }
        public void printOne(fileManager _fileManager, appointment apt)
        {
            String DoctorName = _fileManager.doctors.findOne(apt.DoctorId).FirstName + " " + _fileManager.doctors.findOne(apt.DoctorId).LastName;
            String PatientName = _fileManager.patients.findOne(apt.PatientId).FirstName + " " + _fileManager.patients.findOne(apt.PatientId).LastName;

            Console.WriteLine(DoctorName + " " + PatientName + " " + apt.Description);
        }

        public void addOne(patient pat, string description)
        {
            if(pat.DoctorID != null)
            {
                String id;
                if(appointmentList.Count > 0)
                {
                    id = appointmentList.Last().Id;
                    int idNum = Convert.ToInt32(id.Substring(1)) + 1;
                    id = "4" + Convert.ToString(idNum);
                }
                else
                {
                    id = "41000";
                }
                appointment apt = new appointment(id, pat.DoctorID, pat.Id, description);
                appointmentList.Add(apt);
                writeOne(apt);
            } 
        }

        public static void writeOne(appointment apt)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(fileManager.appointmentsPath))
                {
                    string appointmentData = $"{apt.Id},{apt.DoctorId},{apt.PatientId},{apt.Description}";
                    writer.WriteLine(appointmentData);
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
            }
        }

        // the appointment class will not have a delete method. 
        public void OverWrite()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileManager.appointmentsPath))
                {
                    foreach (appointment apt in appointmentList)
                    {
                        string appointmentData = $"{apt.Id},{apt.DoctorId},{apt.PatientId},{apt.Description}";
                        writer.WriteLine(appointmentData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
            }
        }
        

        //read from txt file.
        // create appointment
        // add delete


        //pass the create method the _filemanager such that it can cross validate the user and doctor id's before writing.
    }


}
