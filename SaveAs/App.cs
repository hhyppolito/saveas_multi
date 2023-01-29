#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

#endregion

namespace Saveas_Mult
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            //get location
            string curAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string curAssemblyPath = System.IO.Path.GetDirectoryName(curAssembly);

            //Ribbon tab creation
            string thisNewTabName = "VE-Menu";
            string thisNewPanelName = "Publish";

            try
            {
                a.CreateRibbonTab(thisNewTabName);
            }
            catch (Autodesk.Revit.Exceptions.ArgumentException)
            {
            }
            //Button creation
            PushButtonData pb1 = new PushButtonData("Multiple Save", "Multiple Save", curAssembly, "Saveas_Mult.Command");
            pb1.LargeImage = new BitmapImage(new Uri(System.IO.Path.Combine(curAssemblyPath, "saveasLogo.ico")));
            pb1.ToolTip = "Rotina para envio de ficheiros .rvt.";
            
            try
            {
                //Add ribbon panel
                RibbonPanel curPanel = a.CreateRibbonPanel(thisNewTabName, thisNewPanelName);
                PushButton pushButton1 = (PushButton)curPanel.AddItem(pb1);
            }
            catch (Autodesk.Revit.Exceptions.ArgumentException)
            {
                //Add button to panel
                List<RibbonPanel> list = a.GetRibbonPanels(thisNewTabName);

                foreach (RibbonPanel curPanel in list)
                {
                    string panelName = curPanel.Name;

                    if (panelName == thisNewPanelName)
                    {
                        PushButton pushButton1 = (PushButton)curPanel.AddItem(pb1);
                    }
                }
            }
            return Result.Succeeded;

        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
