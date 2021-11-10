using AutoMapper;
using CoreCodeCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName))
                .ReverseMap();
            this.CreateMap<Talk, TalkModel>()
                .ReverseMap()
                .ForMember(t => t.TalkId, opt => opt.Ignore())      // Since During PUT Operation, we already have the ID, this particular mapping can be ignored during Reverse Map.
                .ForMember(t => t.Camp, opt => opt.Ignore())        //Ignore these Mappings for the Reverse Map i.e from TalkModel to Talk hence placed after Reverse Map.
                .ForMember(t => t.Speaker, opt => opt.Ignore());
            this.CreateMap<Speaker, SpeakerModel>();                                
        }
    }
}
