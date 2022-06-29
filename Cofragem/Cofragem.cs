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
    public class Cofragem
    {
        //paint walls

        public static void FrameWall(Element wall, Application app)
        {
            Document doc = wall.Document;
            Wall newWall = wall as Wall;
            GeometryElement geometryElement = newWall.get_Geometry(new Options());

            foreach (GeometryObject geoObject in geometryElement)
            {
                if (geoObject is Solid)
                {
                    Solid solid = geoObject as Solid;
                    foreach (Face face in solid.Faces)
                    {
                        if (face.ComputeNormal(new UV(0.5, 0.5)).Z == 0)
                        {
                            //PaintFace.paintFace(columns, face, doc);
                            SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
                        }
                    }
                }
            }

        //    IList<Reference> intFace = HostObjectUtils.GetSideFaces(newWall, ShellLayerType.Interior);
        //    IList<Reference> extFace = HostObjectUtils.GetSideFaces(newWall, ShellLayerType.Exterior);
        //    wallFaces.AddRange(extFace);
        //    wallFaces.AddRange(intFace);

        //    //paint wall side
        //    foreach (Reference f in wallFaces)
        //    {
        //        Face face = doc.GetElement(f).GetGeometryObjectFromReference(f) as Face;
        //        SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
        //        //PaintFace.paintFace(wall, face, doc);
        //    }

        }
        // paint floor
        public static void FrameFloor(Element floor, Application app)
        {
            Document doc = floor.Document;
            //GeometryElement geometryElement = floor.get_Geometry(new Options());
            Floor newFloor = floor as Floor;
            //IList<Reference> infFace = HostObjectUtils.GetBottomFaces(newFloor);
            GeometryElement geometryElement = newFloor.get_Geometry(new Options());

            foreach (GeometryObject geoObject in geometryElement)
            {
                if (geoObject is Solid)
                {
                    Solid solid = geoObject as Solid;
                    foreach (Face face in solid.Faces)
                    {
                        if (face.ComputeNormal(new UV(0.5, 0.5)).Z != 1)
                        {
                            //PaintFace.paintFace(columns, face, doc);
                            SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
                        }
                    }
                }
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
                        if ((face.ComputeNormal(new UV(0.5, 0.5)).Z == 0))
                            {
                             facesAreas.Add(face.Area);
                            }
                    }
                    facesAreas.Sort();
                    if (facesAreas.Count > 0)
                    {
                        double largestFaceArea = facesAreas[facesAreas.Count - 1];
                        XYZ normalVector = null;
                        //double vectorAngle = null;
                        foreach (Face face in solid.Faces)
                        {
                            if (face.Area == largestFaceArea)
                            {
                                normalVector = face.ComputeNormal(new UV(0.5, 0.5));
                            }
                        }

                        foreach (Face face in solid.Faces)
                        {
                            if ((normalVector.AngleTo(face.ComputeNormal(new UV(0.5, 0.5))) == 0 | normalVector.AngleTo(face.ComputeNormal(new UV(0.5, 0.5)))==Math.PI) & face.Area>= largestFaceArea*0.2 | face.ComputeNormal(new UV(0.5, 0.5)).Z<0)
                            {
                                //PaintFace.paintFace(beam, face, doc);
                                SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
                            }
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
                            //PaintFace.paintFace(columns, face, doc);
                            SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
                        }
                    }
                }
            }
        }   

        public static void GenericElements(Element genericElement, Application app)
        {
            Document doc = genericElement.Document;
            GeometryElement geometryElement = genericElement.get_Geometry(new Options());

            foreach (GeometryObject geoObject in geometryElement)
            {
                //Solid solid1 = geoObject as Solid;
                //if (solid1 != null)
                //{
                //    foreach (Face face in solid1.Faces)
                //    {
                //        if (face.ComputeNormal(new UV(0.5, 0.5)).Z != -1)
                //            SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
                //    }
                //}

                GeometryInstance geomInst = geoObject as GeometryInstance;
                if (null != geomInst)
                {
                    GeometryElement transformedGeomElem = geomInst.GetInstanceGeometry(geomInst.Transform);
                    foreach (GeometryObject geotransObject in transformedGeomElem)
                    {
                        Solid solid2 = geotransObject as Solid;
                        foreach (Face face in solid2.Faces)
                        {
                            if (face.ComputeNormal(new UV(0.5, 0.5)).Z != -1)
                                SolidBoundingBox.CreateSolidFromBoundingBox(face, doc, app);
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

