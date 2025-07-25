﻿using AutoMapper;
using MultiDictionary.Domain.Entities;
using MultiDictionary.Shared.ViewModels;

namespace MultiDictionary.WebAPI.MappingProfiles
{
    public class MultiDictionaryMappingProfile : Profile
    {
        public MultiDictionaryMappingProfile()
        {
            CreateMap<Glossary, GlossaryViewModel>().ReverseMap();
            CreateMap<Word, WordViewModel>().ReverseMap();
        }
    }
}
