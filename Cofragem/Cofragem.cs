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
    public class Cofragem
    {
        //paint walls

        public static void FrameWall(Element wall, Application app)
        {
            Document doc = wall.Document;
            Wall newWall = wall as Wall;
            // Cria uma lista com todas as faces internas e externas das paredes
            List<Reference> wallFaces = new List<Reference>();
            IList<Reference> intFace = HostObjectUtils.GetSideFaces(newWall, ShellLayerType.Interior);
            IList<Reference> extFace = HostObjectUtils.GetSideFaces(newWall, ShellLayerType.Exterior);
            wallFaces.AddRange(extFace);
            wallFaces.AddRange(intFace);

            //paint wall side
            foreach (Reference f in wallFaces)
            {
                Face face = doc.GetElement(f).GetGeometryObjectFromReference(f) as Face;
                //SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
                PaintFace.paintFace(wall, face, doc);
            }

        }
        // paint floor
        public static void FrameFloor(Element floor, Application app)
        {
            Document doc = floor.Document;
            //GeometryElement geometryElement = floor.get_Geometry(new Options());
            Floor newFloor = floor as Floor;
            IList<Reference> infFace = HostObjectUtils.GetBottomFaces(newFloor);
            foreach (Reference f in infFace)
            {
                Face face = doc.GetElement(f).GetGeometryObjectFromReference(f) as Face;
                //SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
                PaintFace.paintFace(floor, face, doc);
            }
        }
        //paint beams
        public static void FrameBeam(Element beam, Application app)
        {
            Document doc = beam.Document;
            GeometryElement geometryElement = beam.get_Geometry(new Options());

            foreach (GeometryObject geoObject in geometryElement)
            {
                if (geoObject is Solid)
                {
                    Solid solid = geoObject as Solid;
                    List<double> facesAreas = new List<double>();
                    foreach (Face face in solid.Faces)
                    {
                        facesAreas.Add(face.Area);

                    }
                    facesAreas.Sort();
                    foreach (Face face in solid.Faces)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            facesAreas.RemoveAt(i);
                        }
                        if ((face.ComputeNormal(new UV(0.5, 0.5)).Z <= 0) & facesAreas.Contains(face.Area))
                        {
                            PaintFace.paintFace(beam, face, doc);
                        }
                    }
                }
            }
        }


        //Paint Columns
        public static void FrameColumn(Element columns, Application app)
        {
            Document doc = columns.Document;
            GeometryElement geometryElement = columns.get_Geometry(new Options());

            foreach (GeometryObject geoObject in geometryElement)
            {
                if (geoObject is Solid)
                {
                    Solid solid = geoObject as Solid;
                    foreach (Face face in solid.Faces)
                    {
                        if (face.ComputeNormal(new UV(0.5, 0.5)).Z == 0)
                        {
                            PaintFace.paintFace(columns, face, doc);
                        }
                    }
                }
            }



            //        // Get the geometry instance which contains the geometry information
            //        Autodesk.Revit.DB.GeometryInstance instance = geoObject as Autodesk.Revit.DB.GeometryInstance;
            //if (null != instance)
            //{
            //    foreach (GeometryObject instObj in instance.GetInstanceGeometry())
            //    {
            //        Solid solid = instObj as Solid;
            //        if (null == solid || 0 == solid.Faces.Size || 0 == solid.Edges.Size)
            //        {
            //            continue;
            //        }

            //        //Transform instTransform = instance.Transform;
            //        // Get the faces and edges from solid, and transform the formed points
            //        foreach (Face face in solid.Faces)
            //        {
            //            if (face.ComputeNormal(new UV(0.5, 0.5)).Z == 0)
            //            {
            //                //SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
            //                PaintFace.paintFace(columns, face, doc);
            //            }
            //        }
            //    }
            //}

        }
    }
}

