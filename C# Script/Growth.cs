using System;
using System.Collections;
using System.Collections.Generic;

using Rhino;
using Rhino.Geometry;

using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;



/// <summary>
/// This class will be instantiated on demand by the Script component.
/// </summary>
public class Script_Instance : GH_ScriptInstance
{
#region Utility functions
  /// <summary>Print a String to the [Out] Parameter of the Script component.</summary>
  /// <param name="text">String to print.</param>
  private void Print(string text) { __out.Add(text); }
  /// <summary>Print a formatted String to the [Out] Parameter of the Script component.</summary>
  /// <param name="format">String format.</param>
  /// <param name="args">Formatting parameters.</param>
  private void Print(string format, params object[] args) { __out.Add(string.Format(format, args)); }
  /// <summary>Print useful information about an object instance to the [Out] Parameter of the Script component. </summary>
  /// <param name="obj">Object instance to parse.</param>
  private void Reflect(object obj) { __out.Add(GH_ScriptComponentUtilities.ReflectType_CS(obj)); }
  /// <summary>Print the signatures of all the overloads of a specific method to the [Out] Parameter of the Script component. </summary>
  /// <param name="obj">Object instance to parse.</param>
  private void Reflect(object obj, string method_name) { __out.Add(GH_ScriptComponentUtilities.ReflectType_CS(obj, method_name)); }
#endregion

#region Members
  /// <summary>Gets the current Rhino document.</summary>
  private RhinoDoc RhinoDocument;
  /// <summary>Gets the Grasshopper document that owns this script.</summary>
  private GH_Document GrasshopperDocument;
  /// <summary>Gets the Grasshopper script component that owns this script.</summary>
  private IGH_Component Component; 
  /// <summary>
  /// Gets the current iteration count. The first call to RunScript() is associated with Iteration==0.
  /// Any subsequent call within the same solution will increment the Iteration count.
  /// </summary>
  private int Iteration;
#endregion

  /// <summary>
  /// This procedure contains the user code. Input parameters are provided as regular arguments, 
  /// Output parameters as ref arguments. You don't have to assign output parameters, 
  /// they will have a default value.
  /// </summary>
  private void RunScript(List<Point3d> Point_ori, List<Point3d> Point_eye, List<int> index_eye, List<Point3d> Point_nose, List<int> index_nose, List<Point3d> Point_mouth, List<int> index_mouth, ref object A)
  {
        for (int j = 0; j < index_eye.Count; j++)
    {
      Point_ori[index_eye[j]] = Point_eye[j];

    }
    
    for (int j = 0; j < index_nose.Count; j++)
    {
      Point_ori[index_nose[j]] = Point_nose[j];

    }
    
    for (int j = 0; j < index_mouth.Count; j++)
    {
      Point_ori[index_mouth[j]] = Point_mouth[j];

    }

    A = Point_ori;
  }

  // <Custom additional code> 
  
  // </Custom additional code> 

  private List<string> __err = new List<string>(); //Do not modify this list directly.
  private List<string> __out = new List<string>(); //Do not modify this list directly.
  private RhinoDoc doc = RhinoDoc.ActiveDoc;       //Legacy field.
  private IGH_ActiveObject owner;                  //Legacy field.
  private int runCount;                            //Legacy field.
  
  public override void InvokeRunScript(IGH_Component owner, object rhinoDocument, int iteration, List<object> inputs, IGH_DataAccess DA)
  {
    //Prepare for a new run...
    //1. Reset lists
    this.__out.Clear();
    this.__err.Clear();

    this.Component = owner;
    this.Iteration = iteration;
    this.GrasshopperDocument = owner.OnPingDocument();
    this.RhinoDocument = rhinoDocument as Rhino.RhinoDoc;

    this.owner = this.Component;
    this.runCount = this.Iteration;
    this. doc = this.RhinoDocument;

    //2. Assign input parameters
        List<Point3d> Point_ori = null;
    if (inputs[0] != null)
    {
      Point_ori = GH_DirtyCaster.CastToList<Point3d>(inputs[0]);
    }
    List<Point3d> Point_eye = null;
    if (inputs[1] != null)
    {
      Point_eye = GH_DirtyCaster.CastToList<Point3d>(inputs[1]);
    }
    List<int> index_eye = null;
    if (inputs[2] != null)
    {
      index_eye = GH_DirtyCaster.CastToList<int>(inputs[2]);
    }
    List<Point3d> Point_nose = null;
    if (inputs[3] != null)
    {
      Point_nose = GH_DirtyCaster.CastToList<Point3d>(inputs[3]);
    }
    List<int> index_nose = null;
    if (inputs[4] != null)
    {
      index_nose = GH_DirtyCaster.CastToList<int>(inputs[4]);
    }
    List<Point3d> Point_mouth = null;
    if (inputs[5] != null)
    {
      Point_mouth = GH_DirtyCaster.CastToList<Point3d>(inputs[5]);
    }
    List<int> index_mouth = null;
    if (inputs[6] != null)
    {
      index_mouth = GH_DirtyCaster.CastToList<int>(inputs[6]);
    }


    //3. Declare output parameters
      object A = null;


    //4. Invoke RunScript
    RunScript(Point_ori, Point_eye, index_eye, Point_nose, index_nose, Point_mouth, index_mouth, ref A);
      
    try
    {
      //5. Assign output parameters to component...
            if (A != null)
      {
        if (GH_Format.TreatAsCollection(A))
        {
          IEnumerable __enum_A = (IEnumerable)(A);
          DA.SetDataList(1, __enum_A);
        }
        else
        {
          if (A is Grasshopper.Kernel.Data.IGH_DataTree)
          {
            //merge tree
            DA.SetDataTree(1, (Grasshopper.Kernel.Data.IGH_DataTree)(A));
          }
          else
          {
            //assign direct
            DA.SetData(1, A);
          }
        }
      }
      else
      {
        DA.SetData(1, null);
      }

    }
    catch (Exception ex)
    {
      this.__err.Add(string.Format("Script exception: {0}", ex.Message));
    }
    finally
    {
      //Add errors and messages... 
      if (owner.Params.Output.Count > 0)
      {
        if (owner.Params.Output[0] is Grasshopper.Kernel.Parameters.Param_String)
        {
          List<string> __errors_plus_messages = new List<string>();
          if (this.__err != null) { __errors_plus_messages.AddRange(this.__err); }
          if (this.__out != null) { __errors_plus_messages.AddRange(this.__out); }
          if (__errors_plus_messages.Count > 0) 
            DA.SetDataList(0, __errors_plus_messages);
        }
      }
    }
  }
}