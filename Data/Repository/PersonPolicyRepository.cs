using Core.Entities;
using Core.IRepository;
using Core.IService;
using Dto.ApiPanelDtos.PersonPolicyDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PersonPolicyRepository : GenericRepository<PersonPolicy>, IPersonPolicyRepository
    {
        Context _context;
        DbSet<PersonPolicy> _personPolicies;
        public PersonPolicyRepository(Context context) : base(context)
        {
            _context = context;
            _personPolicies = _context.PersonPolicies;
        }

        public void AddPersonPolicy(AddPersonPolicyDto addPersonPolicyDto)
        {
            PersonPolicy personPolicy = new PersonPolicy()
            {
                FromPerson = addPersonPolicyDto.FromPerson,
                ToPerson = addPersonPolicyDto.ToPerson,
                Order = addPersonPolicyDto.Order
            };

            _personPolicies.Add(personPolicy);
        }

        public void EditPersonPolicy(EditPersonPolicyDto editPersonPolicyDto)
        {
            PersonPolicy personPolicy = _personPolicies.Find(editPersonPolicyDto.PersonPolicyID);

            personPolicy.Order = editPersonPolicyDto.Order;
            personPolicy.FromPerson = editPersonPolicyDto.FromPerson;
            personPolicy.ToPerson = editPersonPolicyDto.ToPerson;

            _personPolicies.Update(personPolicy);
        }

        public EditPersonPolicyDto GetEditPersonPolicyDto(Guid id)
        {
            PersonPolicy personPolicy = _personPolicies.Find(id);

            EditPersonPolicyDto editPersonPolicyDto = new EditPersonPolicyDto()
            {
                PersonPolicyID = personPolicy.Id,
                Order = personPolicy.Order,
                FromPerson = personPolicy.FromPerson,
                ToPerson = personPolicy.ToPerson
            };
            return editPersonPolicyDto;
        }

        public List<PersonPolicyListDto> GetPersonPolicyListDto()
        {
            var personPolicyListDtos = (from personPolicy in _personPolicies.ToList()
                                        where !personPolicy.IsDeleted
                                        select new PersonPolicyListDto()
                                        {
                                            PersonPolicyID = personPolicy.Id,
                                            Status = personPolicy.Status,
                                            Order = personPolicy.Order,
                                            FromPerson = personPolicy.FromPerson,
                                            ToPerson = personPolicy.ToPerson

                                        }).ToList();
            return personPolicyListDtos;
        }

        public List<PersonPolicyDto> PersonPolicyDtoList()
        {
            var personPolicyDtos = (from personPolicy in _personPolicies.ToList()
                                    where !personPolicy.IsDeleted && personPolicy.Status
                                    select new PersonPolicyDto()
                                    {
                                        PersonPolicyID = personPolicy.Id,
                                        Order = personPolicy.Order,
                                        FromPerson = personPolicy.FromPerson,
                                        ToPerson = personPolicy.ToPerson

                                    }).ToList();
            return personPolicyDtos;
        }
    }
}
