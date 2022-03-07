using DotNet6Mediator.DomainLayer.Entities;
using System.Collections.Generic;

namespace DotNet6Mediator.DomainLayer.Seeds
{
    public class UserSeeds
    {
        public static readonly List<User> Seeds = new List<User>()
        {
            new User(){
                Id=1,
                Name = "Tommaso",
                Surname="Zazzaretti",
                BirthDate= new System.DateTime(1996,11,24),
                Username="Tom96",
                Password="tomPwd",
                FK_Role=2
            },
            new User(){
                Id=2,
                Name = "Giorgio",
                Surname="Zazzaretti",
                BirthDate= new System.DateTime(1996,11,24),
                Username="Gio96",
                Password="gioPwd",
                FK_Role=1
            }
        };
    }
}
