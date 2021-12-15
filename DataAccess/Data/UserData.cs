using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;

        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<User>> GetUsers() =>
            _db.LoadData<User, dynamic>("dbo.spUser_GetAll", new { });

        public async Task<User?> GetUser(int id)
        {
            var result = await _db.LoadData<User, dynamic>("dbo.spUser_Get", new { Id = id });
            return result.FirstOrDefault();
        }

        public Task InsertUser(User user) =>
            _db.SaveData("dbo.spUser_Insert", new { user.FirstName, user.LastName });

        public Task UpdateUser(User user) =>
            _db.SaveData("dbo.spUser_Update", user);

        public Task DeleteUser(int id) =>
            _db.SaveData("dbo.spUser_Delete", new { Id = id });
    }
}
