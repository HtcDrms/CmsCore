using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Model.EntityBuilders
{
    public class SliderBuilder
    {
        public SliderBuilder(EntityTypeBuilder<Slider> entityBuilder)
        {
            entityBuilder.HasKey(s => s.Id);
        }
    }
}
