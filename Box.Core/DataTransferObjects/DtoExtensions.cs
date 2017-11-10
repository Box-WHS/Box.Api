namespace Box.Core.DataTransferObjects
{
    public static class DtoExtensions
    {
        public static Data.User ToUser(this User user)
        {
            return new Data.User
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}