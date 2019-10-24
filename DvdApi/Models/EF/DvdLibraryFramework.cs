namespace DvdApi.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DvdLibraryFramework : DbContext
    {
        public DvdLibraryFramework()
            : base("DvdLibraryFramework")
        {
        }

        public virtual DbSet<DVD> DVDs { get; set; }
    }
}
