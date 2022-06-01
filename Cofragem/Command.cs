#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace Cofragens
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //selecionar os elementos a pintar

            Selection selection = uidoc.Selection;
            ICollection<ElementId> selectedIds = selection.GetElementIds();
            FilteredElementCollector walls = new FilteredElementCollector(doc, selectedIds).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType();
            FilteredElementCollector floors = new FilteredElementCollector(doc, selectedIds).OfCategory(BuiltInCategory.OST_Floors).WhereElementIsNotElementType();
            FilteredElementCollector beams = new FilteredElementCollector(doc, selectedIds).OfCategory(BuiltInCategory.OST_StructuralFraming).WhereElementIsNotElementType();
            FilteredElementCollector columns = new FilteredElementCollector(doc, selectedIds).OfCategory(BuiltInCategory.OST_StructuralColumns).WhereElementIsNotElementType();



            Transaction curTrans = new Transaction(doc, "Cofragem");
            curTrans.Start();


            foreach (Element wallElement in walls)
            {
                //GeometryElement geometryElement = wallElement.get_Geometry(new Options());
                Cofragem.FrameWall(wallElement, app);
            }
            foreach (Element floorElement in floors)
            {
                //GeometryElement geometryElement = floorElement.get_Geometry(new Options());
                Cofragem.FrameFloor(floorElement, app);

            }
            foreach (Element beamElement in beams)
            {
                //GeometryElement geometryElement = beamElement.get_Geometry(new Options());
                Cofragem.FrameBeam(beamElement, app);

            }
            foreach (Element columnElement in columns)
            {
                //GeometryElement geometryElement = columnElement.get_Geometry(new Options());
                Cofragem.FrameColumn(columnElement, app);

            }
            curTrans.Commit();

            return Result.Succeeded;
        }
    }
}
