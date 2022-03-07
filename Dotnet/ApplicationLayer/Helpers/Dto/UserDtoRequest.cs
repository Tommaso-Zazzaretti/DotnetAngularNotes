namespace DotNet6Mediator.ApplicationLayer.Helpers.Dto
{
    public class UserDtoRequest //Return UserDTO does not have Password!
    {
        public string Name      { get; set; } = string.Empty;
        public string Surname   { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Username  { get; set; } = string.Empty;
        public string Password  { get; set; } = string.Empty;
        public string RoleName  { get; set; } = string.Empty;
    }
}
