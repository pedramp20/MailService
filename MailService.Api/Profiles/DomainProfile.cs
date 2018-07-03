namespace MailService.Api.Profiles
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using MailService.Api.Models;

    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // creating a profile to map mail model to sendgrid mail model
            CreateMap<MailModel, SendGridMailModel>()
                .ForMember(sg => sg.Personalizations, opt => opt.MapFrom((m) => ConstructPersonalizationObject(m)))
                .ForMember(sg => sg.From, opt => opt.MapFrom(m => new Contact { Email = m.From }))
                .ForMember(sg => sg.Subject, opt => opt.MapFrom(m => m.Subject)).ForMember(
                    sg => sg.Content,
                    opt => opt.MapFrom(m => ConstructContentObject(m)));
        }

        private Content[] ConstructContentObject(MailModel mailModel)
        {
            var contents = new Content[1];

            contents[0] = new Content { Type = "text/plain", Value = mailModel.Body };

            return contents;
        }

        private Personalization[] ConstructPersonalizationObject(MailModel mailModel)
        {
            var personalizations = new Personalization[1];

            personalizations[0] = new Personalization();

            var toContacts = new List<Contact>();
            foreach (var to in mailModel.To)
            {
                toContacts.Add(new Contact { Email = to });
            }

            personalizations[0].To = toContacts.ToArray();

            if (mailModel.Cc?.Count() > 0)
            {
                var ccContacts = new List<Contact>();
                foreach (var cc in mailModel.Cc)
                {
                    ccContacts.Add(new Contact { Email = cc });
                }

                personalizations[0].Cc = ccContacts.ToArray();
            }

            if (mailModel.Bcc?.Count() > 0)
            {
                var bccContacts = new List<Contact>();
                foreach (var bcc in mailModel.Bcc)
                {
                    bccContacts.Add(new Contact { Email = bcc });
                }

                personalizations[0].Bcc = bccContacts.ToArray();
            }

            return personalizations;
        }
    }
}