using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Model.Entities
{
    public class Role:IdentityRole<Guid>
    {
    }
}
