using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Model.EntityBuilders
{
    public class ApplicationUserBuilder
    {
        public ApplicationUserBuilder(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasKey(a => a.Id);
            
        }
    }
}
