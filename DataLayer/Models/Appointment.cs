namespace ColicheGassose
{
    public partial class Appointment
    {
        public Appointment() { }

        public Appointment(AppointmentTransport transport)
        {
            this.App_Id = transport.App_Id;
            this.Info = transport.Info;
            this.When = transport.When;
        }

        public void update(AppointmentTransport appointment)
        {
            this.App_Id = appointment.App_Id;
            this.Info = appointment.Info;
            this.When = appointment.When;
        }
    }
}
