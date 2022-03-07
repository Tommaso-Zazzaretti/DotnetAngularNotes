using DotNet6Mediator.DomainLayer.Entities;
using System.Collections.Generic;

namespace DotNet6Mediator.DomainLayer.Seeds
{
    public class RoleSeeds
    {
        public static readonly List<Role> Seeds = new List<Role>()
        {
            new Role(){
                Id=1,
                RoleName="User"
            },
            new Role(){
                Id=2,
                RoleName="Admin"
            }
        };
    }
}
