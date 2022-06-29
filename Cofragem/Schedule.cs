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
    //public void CreateSingleCategorySchedule(Document doc, ParameterTypeId)
    //{
    //    using (Transaction t = new Transaction(doc, "Create single-category"))
    //    {
    //        t.Start();

    //        // Create schedule
    //        ViewSchedule vs = ViewSchedule.CreateSchedule(doc, new ElementId(BuiltInCategory.OST_GenericModel));

    //        doc.Regenerate();

    //        // Add fields to the schedule
    //        vs.Definition.AddField(ScheduleFieldType.Count);


    //        t.Commit();
    //    }
    //}

    ///// 

    ///// Adds a single parameter field to the schedule
    ///// 

    //public static void AddRegularFieldToSchedule(ViewSchedule schedule, ElementId paramId)
    //{
    //    ScheduleDefinition definition = schedule.Definition;

    //    // Find a matching SchedulableField
    //    SchedulableField schedulableField =
    //        definition.GetSchedulableFields().FirstOrDefault(sf => sf.ParameterId == paramId);

    //    if (schedulableField != null)
    //    {
    //        // Add the found field
    //        definition.AddField(schedulableField);
    //    }
    //}

}
    

