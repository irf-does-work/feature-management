using FeatureToggle.Domain.Entity.Enum;

namespace FeatureToggle.Domain.Entity.FeatureManagementSchema
{
    public class Log(string userId,
                     string userName,
                     int featureId,
                     string featureName,
                     int? businessId,
                     string? businessName,
                     Actions action)
    {
        public int Id { get; private set; }
<<<<<<< HEAD
        public string UserId { get; private set; } = userId;
        public string UserName { get; private set; } = userName;
        public int FeatureId { get; private set; } = featureId;

        public string FeatureName { get; private set; } = featureName;
        public int? BusinessId { get; private set; } = businessId;
=======
        public string UserId { get; private set; }  
        public User User { get; private set; }
        public int FeatureId { get; private set; }
        public string FeatureName { get; private set; }
        public int? BusinessId { get; private set; }
       
        public string? BusinessName { get; private set; }  
        public DateTime Time { get; private set; }
        public Actions Action { get; private set; }

        public Log(string userId, int featureId, string featureName, int? businessId, string? businessName ,Actions action)
        {
            UserId = userId;
            FeatureId = featureId;
            FeatureName = featureName;
            BusinessId = businessId;
            BusinessName = businessName;
            Action = action;
            Time = DateTime.Now;

        }
>>>>>>> irfan/corrections

        public string? BusinessName { get; private set; } = businessName;
        public DateTime Time { get; private set; } = DateTime.UtcNow;
        public Actions Action { get; private set; } = action;
    }
}
