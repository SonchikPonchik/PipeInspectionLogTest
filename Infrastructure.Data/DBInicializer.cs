using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DBInicializer : DropCreateDatabaseIfModelChanges<PipeInspectionLogContext>
    {
        protected override void Seed(PipeInspectionLogContext context) 
        {
            context.Diameters.Add(new Diameter
            {
                Value = 1200
            });
            context.Diameters.Add(new Diameter
            {
                Value = 560
            });
            context.Diameters.Add(new Diameter
            {
                Value = 1850
            });
            context.Diameters.Add(new Diameter
            {
                Value = 200
            });

            context.Nomenclatures.Add(new Nomenclature
            {
                DiameterDeviation = 50
            });
            context.Nomenclatures.Add(new Nomenclature
            {
                DiameterDeviation = 25
            });
            context.Nomenclatures.Add(new Nomenclature
            {
                DiameterDeviation = 10
            });
            context.Nomenclatures.Add(new Nomenclature
            {
                DiameterDeviation = 9
            });

            context.PipeLogs.Add(new PipeLog
            {
                PipeNumber = "547GH3616",
                DiameterId = 1,
                NomenclatureId = 2,
                EndExternalDiameter1 = 1250,
                EndExternalDiameter2 = 1230,
                InspectionDate = new DateTime (2021, 7, 28),
                CenterExternalDiameter = 1240,
                Note = "Осмотр выполнен в соотвествии с нормами БЖ",
                MaxDiameterDeviation = 50
            });

            context.PipeLogs.Add(new PipeLog
            {
                PipeNumber = "547GH3616",
                DiameterId = 2,
                NomenclatureId = 3,
                EndExternalDiameter1 = 563,
                EndExternalDiameter2 = 556,
                InspectionDate = new DateTime(2021, 7, 28),
                Note = "Осмотр выполнен в соотвествии с нормами БЖ",
                MaxDiameterDeviation = -4
            });
            context.PipeLogs.Add(new PipeLog
            {
                PipeNumber = "547GH3616",
                DiameterId = 2,
                NomenclatureId = 3,
                EndExternalDiameter1 = 563,
                EndExternalDiameter2 = 556,
                InspectionDate = DateTime.Now,
                Note = "Осмотр выполнен в соотвествии с нормами БЖ",
                MaxDiameterDeviation = -4
            });
            base.Seed(context);
        }
    }
}
