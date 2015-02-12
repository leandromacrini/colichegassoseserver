namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;

    public partial class Symptom
    {
        public Symptom() { }

        public Symptom(SymptomTransport transport)
        {
            this.App_Id = transport.App_Id;
            this.Agitazione = transport.Agitazione;
            this.Duration = transport.Duration;
            this.Intensity = transport.Intensity;
            this.Pianto = transport.Pianto;
            this.Rigurgito = transport.Rigurgito;
            this.When = transport.When;
        }


        public void update(SymptomTransport symptom)
        {
            this.App_Id = symptom.App_Id;
            this.Agitazione = symptom.Agitazione;
            this.Duration = symptom.Duration;
            this.Intensity = symptom.Intensity;
            this.Pianto = symptom.Pianto;
            this.Rigurgito = symptom.Rigurgito;
            this.When = symptom.When;
        }
    }
}
