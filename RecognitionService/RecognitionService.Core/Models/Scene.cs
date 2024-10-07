namespace RecognitionService.Core.Models
{
    public class Scene
    {
        private readonly string _Name;
        public string Name
        {
            get => _Name;
        }

        private readonly string _Description;
        public string Description 
        { 
            get => _Description; 
        }

        private readonly DateTime _CreationDate;
        public DateTime CreationDate
        {
            get => _CreationDate;
        }

        public Scene(string name, string description, DateTime creationDate)
        {
            this._Name = name;
            this._Description = description;
            this._CreationDate = creationDate;
        }

        public Scene UpdateName(string name)
        {
            return new(name, this._Description, this._CreationDate);
        }
    }
}
