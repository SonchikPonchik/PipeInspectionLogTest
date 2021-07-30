namespace Infrastructure.Data
{
    using Core;
    using System;
    using System.Data.Entity;
    using System.Linq;

    //Класс контекста БД
    public class PipeInspectionLogContext : DbContext
    {
        public PipeInspectionLogContext()
            : base("name=PipeInspectionLogContext")
        {
        }

        public virtual DbSet<PipeLog> PipeLogs { get; set; }
        public virtual DbSet<Nomenclature> Nomenclatures { get; set; }
        public virtual DbSet<Diameter> Diameters { get; set; }
        
    }
}