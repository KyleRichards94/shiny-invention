namespace Assignment1_HospitalManagement
{
    //single class
    class appointment
    {
        //list of all appointments
        //id
        private string id;
        private string doctorId;
        private string patientId;
        private string description;

        public appointment(string aptId, string dId, string pId, string description)
        {
            this.id = aptId;
            this.doctorId = dId;
            this.patientId = pId;
            this.description = description;
        }

        public string Id { get => id; set => id = value; }
        public string DoctorId { get => doctorId; set => doctorId = value; }
        public string PatientId { get => patientId; set => patientId = value; }
        public string Description { get => description; set => description = value; }
        //patient id
        //doctor id
        //time
        //date

        //get by id 
        //get by patient id
        //get by doctor id
        //etc

        // 
    }


}
