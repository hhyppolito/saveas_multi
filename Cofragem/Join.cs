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
    class JoinElement
    {
        public static void Join (FilteredElementCollector foundation, FilteredElementCollector wall, FilteredElementCollector column, FilteredElementCollector beam, FilteredElementCollector floor, FilteredElementCollector genericmodel, Document doc)
        {
            //start

            
            foreach (Element geneic in genericmodel)
            {
                try
                {
                    foreach (Element walls in wall)
                    {
                        JoinGeometryUtils.JoinGeometry(doc, walls, geneic);
                    }
                    foreach (Element beams in beam)
                    {
                        JoinGeometryUtils.JoinGeometry(doc, beams, geneic);
                    }
                    foreach (Element columns in column)
                    {
                        JoinGeometryUtils.JoinGeometry(doc, columns, geneic);
                    }
                    foreach (Element floors in floor)
                    {
                        JoinGeometryUtils.JoinGeometry(doc, floors, geneic);
                    }
                    foreach (Element foundations in foundation)
                    {
                        JoinGeometryUtils.JoinGeometry(doc, foundations, geneic);
                    }
                }
                catch
                {
                    Console.WriteLine("Error on join elements.");
                }
            }
            
        
            }

            //return Result.Succeeded;
        }
    }
    

