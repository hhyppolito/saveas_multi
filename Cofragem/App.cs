#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

#endregion

namespace Cofragem
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            //get location
            string curAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string curAssemblyPath = System.IO.Path.GetDirectoryName(curAssembly);

            //Ribbon tab creation
            string thisNewTabName = "VE_Menu";
            string thisNewPanelName = "Tools";

            try
            {
                a.CreateRibbonTab(thisNewTabName);
            }
            catch (Autodesk.Revit.Exceptions.ArgumentException)
            {
            }
            //Button creation
            PushButtonData pb1 = new PushButtonData("Cofragens", "Cofragens", curAssembly, "Cofragem.Command");
            pb1.LargeImage = new BitmapImage(new Uri(System.IO.Path.Combine(curAssemblyPath, "cf_logo.ico")));

            //Add ribbon panel
            RibbonPanel curPanel = a.CreateRibbonPanel(thisNewTabName, thisNewPanelName);

            //Add button to panel
            PushButton pushButton1 = (PushButton)curPanel.AddItem(pb1);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
