using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Model.EntityBuilders
{
    public class FormFieldBuilder
    {
        public FormFieldBuilder(EntityTypeBuilder<FormField> entityBuilder)
        {
            entityBuilder.HasKey(a => a.Id);
            entityBuilder.HasOne(f => f.Form).WithMany(ff => ff.FormFields).HasForeignKey(f => f.FormId).OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}
