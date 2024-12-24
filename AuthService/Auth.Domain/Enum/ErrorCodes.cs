using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Enum
{
    public enum ErrorCodes
    {
      


        InternalServerError = 10,

        UserNotFound = 11,
        UserAlreadyExists = 12,
        Unauthorized = 13,
        UserAlreadyExistsThisRole = 14,

        PasswordNotEqualPasswordConfirm = 21,
        PasswordIsWrong = 22,

        RoleAlreadyExists = 31,
        RoleNotFound = 32
    }
}
