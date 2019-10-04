using AutoMapper;
using System.Linq;

namespace Jp.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings(params Profile[] customProfiles)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
                customProfiles.ToList().ForEach(cfg.AddProfile);
            });
        }
    }
}
