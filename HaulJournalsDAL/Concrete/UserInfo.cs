using HaulJournalsDAL.Entities;

namespace HaulJournalsDAL.Concrete
{
    /// <summary>
    ///  Вся неконфиденциальная информация о пользователе
    /// </summary>
    public class UserInfo
    {
        public int Id { get; }

        public short AccessLevel { get; }

        public string Username { get; }

        public string Note { get; }

        public UserInfo(Users user)
        {
            Id = user.Id;
            AccessLevel = user.AccessLevel;
            Username = user.Username;
            Note = user.Note;
        }
    }
}
