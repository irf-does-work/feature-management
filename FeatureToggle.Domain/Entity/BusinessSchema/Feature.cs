namespace FeatureToggle.Domain.Entity.BusinessSchema
{
    public class Feature
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<BusinessFeatureFlag> BusinessFeatures { get; private set; }
    }
}
