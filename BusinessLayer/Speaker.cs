using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    
    public class Speaker
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int? Experiencia { get; set; }
		public bool HasBlog { get; set; }
		public string BlogURL { get; set; }
		public WebBrowser Browser { get; set; }
		public IEnumerable<string> Certifications { get; set; }
		public string Employer { get; set; }
		public int RegistrationFee { get; set; }
		public IEnumerable<BusinessLayer.Session> Sessions { get; set; }

        const int ExperienciaMinima = 10;
        const int CertificadoMinimo = 3;
        const int VersionMinima = 9;

        
        public int? Registerspeaker(IRepository repository)
		{
			
			int? speakerId = null;
			bool approved = false;

			var oldtecnologies  = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };

			var domains = new List<string>() { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };

            void ValidarDatos(string dato)
            {
                if (string.IsNullOrEmpty(dato))
                    throw new ArgumentNullException($"{dato.GetType().Name} is required.");

            }

            bool ValidarExtras()
            {
                var empresas = new string[] { "Microsoft", "Google", "Fog Creek Software", "37Signals" };

                var topEmpresas = empresas.Contains(Employer);
                var certificadosMinimos = Certifications.Count() > CertificadoMinimo;
                string emailDomain = Email.Split('@').Last();
                var dominiovalido = domains.Contains(emailDomain);
                var browservalido = Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion >= VersionMinima;

                return Experiencia> ExperienciaMinima && topEmpresas && certificadosMinimos && !dominiovalido && browservalido;
            }

            ValidarDatos(FirstName);
            ValidarDatos(LastName);
            ValidarDatos(Email);

           

            if (ValidarExtras()) throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.");
            {
                if (Sessions.Count() != 0) throw new ArgumentException("Can't register speaker with no sessions to present.");
                {
                    foreach (var session in Sessions)
                    {
                        

                        foreach (var tech in oldtecnologies)
                        {
                            if (session.Title.Contains(tech) || session.Description.Contains(tech))
                            {
                                session.Approved = false;
                                break;
                            }
                            else
                            {
                                session.Approved = true;
                                approved = true;
                            }
                        }
                    }
                }
                



                if (approved) throw new NoSessionsApprovedException("No sessions approved.");
                {
                    Experiencia = 0;

                    switch (Experiencia)
                    {
                        case 1:
                            RegistrationFee = 500;
                            break;
                        case 2: case 3:
                            RegistrationFee = 250;
                            break;
                        case 4:  case 5:
                            RegistrationFee = 100;
                            break;
                        case 6: case 7: case 8: case 9:
                            RegistrationFee = 50;
                            break;
                    }

                    
                    
                               

                    
                    try
                    {
                        speakerId = repository.SaveSpeaker(this);
                    }
                    catch 
                    {
                       throw new NoSessionsApprovedException("error al intentar grabar.");
                    }
                }
                
            }
            

            
            return speakerId;
		}

		#region Custom Exceptions
		public class SpeakerDoesntMeetRequirementsException : Exception
		{
            public SpeakerDoesntMeetRequirementsException()
            {

            }
			public SpeakerDoesntMeetRequirementsException(string message)
				: base(message)
			{
			}

			public SpeakerDoesntMeetRequirementsException(string format, params object[] args)
				: base(string.Format(format, args)) { }
		}

		public class NoSessionsApprovedException : Exception
		{
			public NoSessionsApprovedException(string message)
				: base(message)
			{
			}
		}
		#endregion
	}
}