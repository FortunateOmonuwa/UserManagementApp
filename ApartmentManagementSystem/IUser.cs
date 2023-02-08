namespace UserManagementSystem
{
    public interface IUser
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        Task<IEnumerable<User>> GetAllUsers(User user);
        Task<User> GetUserByID(int id);

        Task<string> DeleteUser(User user);


    }
}