using Application.Features.ContestEntries.Command;
using Application.Features.ContestEntries.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles;

public class ContestEntryProfile : Profile
{
	public ContestEntryProfile()
	{
		CreateMap<ContestEntry, ContestEntryDto>();
		// You can add mappings for Contest and Video DTOs if you intend to include them
		// CreateMap<Contest, ContestDto>();
		// CreateMap<Video, VideoDto>();
		CreateMap<CreateContestEntryCommand, ContestEntry>();
		CreateMap<UpdateContestEntryCommand, ContestEntry>();
	}
}
