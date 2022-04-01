using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary52
{
    public class Class1
    {
        public static List<Pipe> NumberOfPipes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var pipes = new FilteredElementCollector(doc)
                .OfClass(typeof(Pipe))
                .Cast<Pipe>()
                .ToList();
            return pipes;
        }

        public static void VolumeOfWalls(ExternalCommandData commandData, out double volume, out int count)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            volume = 0;
            count = 0;

            var walls = new FilteredElementCollector(doc)
                .OfClass(typeof(Wall))
                .Cast<Wall>()
                .ToList();

            foreach (Wall wall in walls)
            {
                var wallVolume = wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble();
                volume += Math.Round(UnitUtils.ConvertFromInternalUnits(wallVolume, UnitTypeId.CubicMeters), 2);
                count++;
            }
        }

        public static List<FamilyInstance> NumberOfDoors(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var doors = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType()
                .Cast<FamilyInstance>()
                .ToList();
            return doors;
        }

        public static List<Element> PickObjects(ExternalCommandData commandData, string messege = "Выберите элементы")
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedObjects = uidoc.Selection.PickObjects(ObjectType.Element, messege);
            List<Element> elementsList = selectedObjects.Select(selectedObject => doc.GetElement(selectedObject)).ToList();
            return elementsList;
        }
    }
}
