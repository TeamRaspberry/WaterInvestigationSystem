namespace AuthService.Persistance.Utils
{
    public class ErrorMessages
    {
        public const string RoleNotFoundError = "Роль с таким ID не найдена";
        public const string SingleDefaultRoleError = "Нельзя удалить единственную роль по умолчанию";
        public const string RoleIsDefaultError = "Роль уже является ролью по умолчанию";
        public const string SingleAdminRoleError = "Нельзя удалить единственную роль администратора";
        public const string RoleIsAdminError = "Роль уже является ролью администратора";
        public const string UserNotFoundError = "Пользователь с таким ID не найден";
    }
}
