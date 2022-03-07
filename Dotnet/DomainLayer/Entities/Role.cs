namespace DotNet6Mediator.DomainLayer.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;

        //@OneToMany Navigation Properties WITH User
        public virtual ICollection<User>? Users { get; set; }
    }
}
