using AutoMapper;
using Company.Khloud.DAL.Models;
using Company.Khloud.PL.Dtos;

namespace Company.Khloud.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
           .ForMember(d => d.Name, o => o.MapFrom(s => s.EmpName))
           .ForMember(d => d.Department, o => o.Ignore());

            CreateMap<Employee, CreateEmployeeDto>()
            .ForMember(d => d.EmpName, o => o.MapFrom(s => s.Name));


           

        }
    }
}
