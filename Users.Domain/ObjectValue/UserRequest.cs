namespace Users.Domain.ObjectValue
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(Name)
            && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
    }
}
