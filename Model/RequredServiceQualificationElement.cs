using System.Collections.Generic;
using HaloBiz.Model.ManyToManyRelationship;

namespace HaloBiz.Model
{
    public class RequredServiceQualificationElement : SetupBaseModel
    {
        public ServiceCategory ServiceCategory  { get; set; }
        public long ServiceCategoryId { get; set; }
    }
}