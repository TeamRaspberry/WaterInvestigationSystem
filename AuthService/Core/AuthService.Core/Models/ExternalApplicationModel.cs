namespace AuthService.Core.Models
{
    public class ExternalApplicationModel
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public string Name { get; private init; }
        public string Token { get; private init; }

        public ExternalApplicationModel(Guid id, string name, string token)
        {
            Id = id;
            Name = name;
            Token = token;
        }
    }
}
