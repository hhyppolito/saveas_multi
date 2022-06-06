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

namespace Cofragem
{
    class SharePar
    {
        public static void CreateShare(Document doc, Application revitApp, string group)
        {
            try
            {

                DefinitionFile defFile = doc.Application.OpenSharedParameterFile();

                string modulePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string paramFile = modulePath + "\\AreasSharedParameters.txt";

                // check whether shared parameter exists
                //if (defFile == null)
                //{
                // create shared parameter file
                if (File.Exists(paramFile))
                {

                    File.Delete(paramFile);

                }
                FileStream fs = File.Create(paramFile);
                fs.Close();
                //}


                //else
                //{
                // cache application 
                //Autodesk.Revit.ApplicationServices.Application revitApp = doc.Application;

                // prepare shared parameter file
                revitApp.SharedParametersFilename = paramFile;

                // open shared parameter file
                DefinitionFile parafile = revitApp.OpenSharedParameterFile();

                // create a group
                DefinitionGroup apiGroup = parafile.Groups.Create(group);


                ExternalDefinitionCreationOptions options = new ExternalDefinitionCreationOptions("FormWork Area", ParameterType.Area);




                // create parameter
                Definition areaSharedParamDef = apiGroup.Definitions.Create(options);

                // get Areas category
                Autodesk.Revit.DB.Category areaCat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_GenericModel);

                CategorySet categories = revitApp.Create.NewCategorySet();

                categories.Insert(areaCat);


                // insert the new parameter
                InstanceBinding binding = revitApp.Create.NewInstanceBinding(categories);
                doc.ParameterBindings.Insert(areaSharedParamDef, binding, BuiltInParameterGroup.PG_AREA);

                //}

            }
            catch
            {
                //throw new Exception("Failed to create shared parameter: " + ex.Message);
            }


            // Finalize transaction
            //TransactionManager.Instance.TransactionTaskDone();

        }
    }
}


