namespace CarCatalogue.Data.Contracts
{
    public abstract class DeletableEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
