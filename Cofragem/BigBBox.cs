#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.Creation;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace Cofragem
{
    [Transaction(TransactionMode.Manual)]
    public class BounderyBox
    {
        //public static Extrusion CreatingBigBox (Document doc)
        //{
        //    View view = doc.ActiveView;
        //    View3D view3D = (View3D)view;

        //    string message = "BoundingBoxXYZ : ";
        //    message += "\nView name : " + view3D.Name;
        //    BoundingBoxXYZ boundingBox = view3D.GetSectionBox();

        //    FamilyItemFactory factory = doc.FamilyCreate;

        //    //Application application = Application.Cu


        //    // The section box of the 3D view can cut the model.

        //    BoundingBoxXYZ sectionBox = boundingBox;
        //    // Note that the section box can be rotated and transformed.  
        //    // So the min/max corners coordinates relative to the model must be computed via the transform.
        //    Transform trf = sectionBox.Transform;

        //    XYZ maxXY = sectionBox.Max; //Maximum coordinates (upper-right-front corner of the box before transform is applied).
        //    XYZ minXY = sectionBox.Min; //Minimum coordinates (lower-left-rear corner of the box before transform is applied).
        //    XYZ P1 = new XYZ(maxXY.X, maxXY.Y, minXY.Z);
        //    XYZ P2 = new XYZ(minXY.X, maxXY.Y, minXY.Z);
        //    XYZ P3 = new XYZ(maxXY.X, minXY.Y, minXY.Z);
        //    double height = maxXY.Z - minXY.Z;

        //    XYZ minXYmc = trf.OfPoint(minXY);
        //    XYZ P1mc = trf.OfPoint(P1);
        //    XYZ P2mc = trf.OfPoint(P2);
        //    XYZ P3mc = trf.OfPoint(P3);



        //    CurveArray profile = new CurveArray();
        //    profile.Append(Line.CreateBound(P1mc, P2mc));
        //    profile.Append(Line.CreateBound(P2mc, minXYmc));
        //    profile.Append(Line.CreateBound(minXYmc, P3mc));
        //    profile.Append(Line.CreateBound(P3mc, P1mc));

        //    CurveArrArray curveArrArray = new CurveArrArray();
        //    curveArrArray.Append(profile);

        //    Document fdoc = app.NewFamilyDocument(templateFileName);

        //    if (null == fdoc)
        //    {
        //        message = "Cannot create family document.";
        //        return Result.Failed;
        //    }

        //    Transaction t = new Transaction(fdoc,
        //      "Create structural stiffener family");


        //    //CurveLoop curveLoop = CurveLoop.Create(profile);
        //    //IList<CurveLoop> loopList = new List<CurveLoop> ();
        //    //loopList.Add(curveLoop);

        //    //SolidOptions options = new SolidOptions(ElementId.InvalidElementId, ElementId.InvalidElementId);
        //    //Solid preTransformBox = GeometryCreationUtilities.CreateExtrusionGeometry(loopList, XYZ.BasisZ, height, options) ;

        //    //DirectShape ds = DirectShape.CreateElement(doc, new ElementId(BuiltInCategory.OST_GenericModel));

        //    //ds.ApplicationId = "Application id";
        //    //ds.ApplicationDataId = "Geometry object id";
        //    //ds.SetShape(new GeometryObject[] { preTransformBox });

        //    //return ds;

        //    //TaskDialog.Show("Revit", message);
        }
    }
//}

