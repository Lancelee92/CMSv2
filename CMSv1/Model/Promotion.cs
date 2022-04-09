using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSv1.Model
{
    public class PromotionRequest : Promotion
    {
        public string type { get; set; }

        public Promotion getPromotion(Promotion Data)
        {
            return new Promotion()
            {
                ID = Data.ID,
                Title = Data.Title,
                Description = Data.Description,
                ImageUrl = Data.ImageUrl,
                ImageCaption = Data.ImageCaption,
                IfShowTitle = Data.IfShowTitle
            };
        }
    }

    public class Promotion
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ImageCaption { get; set; }
        public DateTime DateCreated { get; set; }
        public Boolean IfShowTitle { get; set; }
    }
}
