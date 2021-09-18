namespace Jeto.Basel.Domain.Models
{
    public class User : IEntity, IHasCompanyId
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}