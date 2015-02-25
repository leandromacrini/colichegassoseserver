namespace ColicheGassose
{
    public partial class Appointment
    {
        public Appointment() { }

        public Appointment(AppointmentTransport transport)
        {
            this.App_Id = transport.ID;
            this.Info = transport.Info;
            this.When = transport.When;
        }

        public void update(AppointmentTransport appointment)
        {
            this.App_Id = appointment.ID;
            this.Info = appointment.Info;
            this.When = appointment.When;
        }
    }
}
