namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;

    public partial class Symptom
    {
        public Symptom() { }

        public Symptom(SymptomTransport transport)
        {
            this.App_Id = transport.ID;
            this.Agitazione = transport.Agitazione == 1;
            this.Duration = transport.Duration;
            this.Intensity = transport.Intensity;
            this.Pianto = transport.Pianto == 1;
            this.Rigurgito = transport.Rigurgito == 1;
            this.When = transport.When;
        }


        public void update(SymptomTransport symptom)
        {
            this.App_Id = symptom.ID;
            this.Agitazione = symptom.Agitazione == 1;
            this.Duration = symptom.Duration;
            this.Intensity = symptom.Intensity;
            this.Pianto = symptom.Pianto == 1;
            this.Rigurgito = symptom.Rigurgito == 1;
            this.When = symptom.When;
        }
    }
}
