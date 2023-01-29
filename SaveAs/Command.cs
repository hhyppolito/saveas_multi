#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.IO;
using System.Windows.Forms;


#endregion

namespace Saveas_Mult
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            // Show the OpenFileDialog to let the user select the Revit files
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Files.";
            openFileDialog.Filter = "Revit Files (*.rvt)|*.rvt|All Files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //
                Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
                //
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Select folder to save the model";
                string selectedPath = null;
                
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = folderBrowserDialog.SelectedPath;
                    // Use the selected path to save the files
                }

                // Loop through each selected file
                foreach (string fileName in openFileDialog.FileNames)
                {
                    try
                    {

                        // Open the selected file
                        Document doc = app.OpenDocumentFile(fileName);

                        // Define the path to save the detached file
                        string filePath = Path.Combine(selectedPath, Path.GetFileNameWithoutExtension(fileName) + "_detached.rvt");

                        // Save the current document as detached
                        SaveAsOptions saveAs = new SaveAsOptions();
                        //WorksharingSaveAsOptions worksharing = new WorksharingSaveAsOptions();
                        DetachFromCentralOption detach = new DetachFromCentralOption();
                        //worksharing.SaveAsCentral = false;
                        detach.Equals(true);
                        doc.SaveAs(filePath, saveAs); ;

                        // Close the document
                        doc.Close(false);
                    }
                    catch (Exception ex)
                    {
                        // Show an error message if there's an issue with saving the file
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }  
            }
            MessageBox.Show("Finished.");
            return Result.Succeeded;
        }
    }
 }
