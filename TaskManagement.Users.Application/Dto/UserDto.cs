namespace TaskManagement.Users.Application.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string? MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}