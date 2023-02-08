using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem
{
    public interface IUserUpdate
    {
        void UpdateName(User user);
        void UpdatePhoneNumber(User user);
        void UpdateEmail(User user);
        void UpdateAddress(User user);
        void UpdateDateOfBirth(User user);
        void UpdateAll(User user);
    }
}
