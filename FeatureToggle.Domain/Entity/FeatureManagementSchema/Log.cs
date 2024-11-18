namespace FeatureToggle.Domain.Entity.FeatureManagementSchema
{
    public class Log
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }        
        public int FeatureId { get; private set; }
        public int? BusinessId { get; private set; }
        public DateTime Time { get; private set; }
        public Actions Action { get; private set; }
        
    }
}
