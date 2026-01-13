using PHBAPI.Model;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PHBAPI.Connection
{
    public class PBHDbContext: DbContext
    {
        public PBHDbContext(DbContextOptions<PBHDbContext> opt) : base(opt) { }

        public DbSet<DocCaptureImages> DocCaptureImages { get; set; }
    }
}
