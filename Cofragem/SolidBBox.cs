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

namespace Cofragem
{
    [Transaction(TransactionMode.Manual)]
    public class SolidBoundingBox
    {

        public static DirectShape CreateSolidFromBoundingBox(Face face, Document doc, Application app)

        {
            double height = 0.05;

            //CurveLoop baseLoop = CurveLoop.Create(edges);
            IList<CurveLoop> loopList = face.GetEdgesAsCurveLoops();
            XYZ topNormal = face.ComputeNormal(new UV(0.5, 0.5));
            topNormal = topNormal.Multiply(-1);
            //TaskDialog.Show("",topNormal.ToString());

            SolidOptions options = new SolidOptions(ElementId.InvalidElementId, ElementId.InvalidElementId);
            Solid preTransformBox = GeometryCreationUtilities.CreateExtrusionGeometry(loopList, topNormal, height, options);

            DirectShape ds = DirectShape.CreateElement(doc, new ElementId(BuiltInCategory.OST_GenericModel));

            ds.ApplicationId = "Application id";
            ds.ApplicationDataId = "Geometry object id";
            ds.SetShape(new GeometryObject[] { preTransformBox });

            Parameter parameter = ds.LookupParameter("FormWork Area");

            if (parameter == null)
            {
                SharePar.CreateShare(doc, app, "Grupo");
            }
            //assing area to shareparameter
            Parameter par = ds.LookupParameter("FormWork Area");
            double faceArea = face.Area;
            par.Set(faceArea);

            return ds;
        }


    }
}

