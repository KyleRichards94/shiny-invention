namespace Assignment1_HospitalManagement
{
    class fileManager
    {
        public const string doctorsPath = "doctors.txt";
        public const string patientsPath = "patients.txt";
        public const string adminsPath = "admins.txt";
        public const string appointmentsPath = "appointments.txt";

        public doctors doctors = new doctors();
        public patients patients = new patients();
        public admins admins = new admins();
        public appointments appointments = new appointments();

        /////////////////////////////////////////////////////////////////////////
        //// File manager documentation 
        //// the file manager class creates and contains memory allocation for 
        ///         doctors       patients          admins          appointments
        ///  the file manager does so through a heirarchy of menu to one relation ships 
        ///  between the pluralized and singular class defininitions for each data structure
        ///  the pluralized data structure (eg doctors)  read, write and validate between local storage and contigent txt files. 
        ///  the singular data structures contain the definintion for the account and nothing else. 

        public fileManager()
        {

        }  
        
        //Each time the user opens the application each data structure is populated using txt files
        public void initializeFM()
        {
            doctors.loadFromFile(doctorsPath);
            patients.loadFromFile(patientsPath);
            admins.loadFromFile(adminsPath);
            appointments.loadFromFile(appointmentsPath);
        }

        //each time the application is closed the txt files are re-written to contain updated data
        public void writeQuit()
        {
            doctors.OverWrite();
            patients.OverWrite();
            admins.OverWrite();
            appointments.OverWrite();
        }

    }


}
