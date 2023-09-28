namespace Assignment1_HospitalManagement
{
    //single class
    class patient
    {
        private string id;

        private string firstName;

        private string lastName;

        private string email;

        private string address;

        private string phoneNumber;

        private string password;

        private string doctorId; // the ID of the doctor they are assigned too.

        
        
        public patient(string id, string firstName, string lastName, string email, string address, string phoneNumber, string password, string doctorId)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.doctorId = doctorId;
            this.password = password;
        }

        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string Id { get => id; set => id = value; }
        public string Password { get => password; set => password = value; }
        public string DoctorID { get => doctorId; set => doctorId = value; }


        public override string ToString()
        {
            return $"│ {firstName + " " + lastName,-20} │ {email,-25} │ {phoneNumber,-12} │ {address,-30} │";
        }
        //name\
        //phonenum
        //etc

        ////methods
        
        //get
        //set
        //etc

    }


}
