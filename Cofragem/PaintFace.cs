#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

#endregion

namespace Cofragens
{
    class PaintFace
    {
        public static void paintFace(Element element, Face face, Document doc)
        {
            FilteredElementCollector elementCollector = new FilteredElementCollector(doc);
            elementCollector.WherePasses(new ElementClassFilter(typeof(Material)));
            IList<Element> materials = elementCollector.ToElements();

            string materialName = "Blue, Solid";

            foreach (Element materialElement in materials)
            {
                Material material = materialElement as Material;
                if (materialName == material.Name)
                {
                    Material mat = material;
                    doc.Paint(element.Id, face, mat.Id);
                    break;
                }
            }
        }
    }
}


