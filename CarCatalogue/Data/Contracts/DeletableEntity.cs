namespace CarCatalogue.Data.Contracts
{
    public abstract class DeletableEntity
    {
        public DateTime CreatedOrModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
