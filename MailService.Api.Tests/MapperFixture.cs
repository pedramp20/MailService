using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Api.Tests
{
    using AutoMapper;

    using MailService.Api.Profiles;

    public class MapperFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public MapperFixture()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DomainProfile());
                });
            Mapper = config.CreateMapper();
        }
        public void Dispose()
        {

        }
    }
}
