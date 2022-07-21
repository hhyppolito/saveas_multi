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

        public static void CreateSolidFromBoundingBox(Element el, Face face, Document doc, Application app)

        {
            //double height = 0.05;

            ////CurveLoop baseLoop = CurveLoop.Create(edges);
            //IList<CurveLoop> loopList = face.GetEdgesAsCurveLoops();
            //XYZ topNormal = face.ComputeNormal(new UV(0.5, 0.5));
            //topNormal = topNormal.Multiply(-1);
            ////TaskDialog.Show("",topNormal.ToString());

            //SolidOptions options = new SolidOptions(ElementId.InvalidElementId, ElementId.InvalidElementId);
            //Solid preTransformBox = GeometryCreationUtilities.CreateExtrusionGeometry(loopList, topNormal, height, options);
            ////não pode ser por extrusão precisamos incluir qqr coisa em cada face.

            //DirectShape ds = DirectShape.CreateElement(doc, new ElementId(BuiltInCategory.OST_GenericModel));

            //ds.ApplicationId = "Application id";
            //ds.ApplicationDataId = "Geometry object id";
            //ds.SetShape(new GeometryObject[] { preTransformBox });

            //Reference reference = face.Reference;

            //Element el = doc.GetElement(reference.ElementId);

            try
            {
                Mesh mesh = face.Triangulate();

                var familyInstance = el as FamilyInstance;

                if (null != familyInstance)
                {
                    var t = familyInstance.GetTotalTransform();

                    mesh = mesh.get_Transformed(t);
                }

                using (Transaction trans = new Transaction(doc))
                {
                    TessellatedShapeBuilder builder= new TessellatedShapeBuilder();

                    builder.OpenConnectedFaceSet(false);

                    List<XYZ> args = new List<XYZ>(3);

                    XYZ[] triangleCorners = new XYZ[3];

                    for (int i = 0; i < mesh.NumTriangles; ++i)
                    {
                        MeshTriangle triangle = mesh.get_Triangle(i);

                        triangleCorners[0] = triangle.get_Vertex(0);
                        triangleCorners[1] = triangle.get_Vertex(1);
                        triangleCorners[2] = triangle.get_Vertex(2);

                        TessellatedFace tesseFace= new TessellatedFace(triangleCorners,ElementId.InvalidElementId);

                        if (builder.DoesFaceHaveEnoughLoopsAndVertices(tesseFace))
                        {
                            builder.AddFace(tesseFace);
                        }
                    }

                    builder.CloseConnectedFaceSet();

                    //TessellatedShapeBuilderResult result
                    //  = builder.Build(
                    //    TessellatedShapeBuilderTarget.AnyGeometry,
                    //    TessellatedShapeBuilderFallback.Mesh,
                    //    ElementId.InvalidElementId ); // 2016

                    builder.Target = TessellatedShapeBuilderTarget.AnyGeometry; // 2018
                    builder.Fallback = TessellatedShapeBuilderFallback.Mesh; // 2018

                    builder.Build(); // 2018

                    TessellatedShapeBuilderResult result = builder.GetBuildResult(); // 2018

                    ElementId categoryId = new ElementId(BuiltInCategory.OST_GenericModel);

                    //DirectShape ds = DirectShape.CreateElement(
                    //  doc, categoryId,
                    //  Assembly.GetExecutingAssembly().GetType()
                    //    .GUID.ToString(), Guid.NewGuid().ToString() ); // 2016

                    DirectShape ds = DirectShape.CreateElement(doc, categoryId); // 2018

                    ds.ApplicationId = "Application id";

                    ds.ApplicationDataId = "Geometry object id";

                    ds.SetShape(result.GetGeometricalObjects());

                    ds.Name = "MyShape";

                    Parameter parameter = ds.LookupParameter("FormWork Area");

                    if (parameter == null)
                    {
                        SharePar.CreateShare(doc, app, "Grupo");
                    }
                    //assing area to shareparameter
                    Parameter par = ds.LookupParameter("FormWork Area");
                    double faceArea = face.Area;
                    par.Set(faceArea);

                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }


            //DirectShape ds = DirectShape.CreateElement(doc, new ElementId(BuiltInCategory.OST_GenericModel));

            //ds.ApplicationId = "Application id";
            //ds.ApplicationDataId = "Geometry object id";
            //ds.SetShape(new GeometryObject[] { preTransformBox });

        }


    }
}

