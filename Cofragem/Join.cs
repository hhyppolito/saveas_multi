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

            //var classOrder = new List<int> { wallOrder, beamOrder, columnOrder, floorOrder, genericmOrder };

            //select element

            //var elementClasses = new List<FilteredElementCollector> { wall, beam, column, floor, genericmodel};

            // Switch joint order

            //var count = 0;
            //TaskDialog.Show("class order size equal to ", classOrder.Count.ToString());
            
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
            
            //for (int i = 0; i < classOrder.Count - 1; i++)
            //{
            //    foreach (Element principalClass in elementClasses[classOrder.IndexOf(i + 1)])
            //    {
            //        for (int j = 0; j < classOrder.Count - i - 1; j++)
            //        {
            //            foreach (Element secondaryClass in elementClasses[classOrder.IndexOf(j + i + 2)])
            //            {
            //                bool areJoined = JoinGeometryUtils.AreElementsJoined(doc, secondaryClass, principalClass);

            //                //VERIFICA SE O PILAR ESTA ATTACHED A VIGA, SE SIM N�O FAZ NADA
            //                if (classOrder.IndexOf(i + 1) == 2 & classOrder.IndexOf(j + i + 2) == 1)
            //                {
            //                    FamilyInstance famInst = principalClass.Document.GetElement(principalClass.Id) as FamilyInstance;
            //                    var targetElement = ColumnAttachment.GetColumnAttachment(famInst, secondaryClass.Id);
            //                    bool areAttached = true;
            //                    if (targetElement != null)
            //                    {
            //                        areAttached = Equals(targetElement.TargetId, secondaryClass.Id);

            //                    }
            //                    else
            //                    {
            //                        areAttached = false;
            //                    }
            //                    if (areAttached)
            //                    {
            //                        areJoined = false;
            //                    }
            //                }
            //                if (classOrder.IndexOf(j + i + 2) == 2 & classOrder.IndexOf(i + 1) == 1)
            //                {
            //                    FamilyInstance famInst = secondaryClass.Document.GetElement(secondaryClass.Id) as FamilyInstance;
            //                    var targetElement = ColumnAttachment.GetColumnAttachment(famInst, principalClass.Id);
            //                    bool areAttached = true;
            //                    if (targetElement != null)
            //                    {
            //                        areAttached = Equals(targetElement.TargetId, principalClass.Id);
            //                    }
            //                    else
            //                    {
            //                        areAttached = false;
            //                    }
            //                    if (areAttached)
            //                    {
            //                        areJoined = false;
            //                    }
            //                }

            //                if (areJoined)
            //                {
            //                    bool iscut = JoinGeometryUtils.IsCuttingElementInJoin(doc, secondaryClass, principalClass);
            //                    if (iscut)
            //                    {
            //                        try
            //                        {
            //                            JoinGeometryUtils.SwitchJoinOrder(doc, principalClass, secondaryClass);
            //                        }
            //                        catch
            //                        {
            //                            Console.WriteLine("Error found.");
            //                        }
            //                        count++;
            //                    }

            //                }

            //            }
            //        }
            //    }

            }

            //return Result.Succeeded;
        }
    }
    
