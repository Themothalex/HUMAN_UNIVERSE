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
  private void RunScript(List<Point3d> Points_All, List<Point3d> Points_Eye, List<Point3d> Points_Mouth, List<Point3d> Points_Nose, ref object num_eye, ref object num_mouth, ref object num_nose)
  {
        List<int> j_eye = new List<int>();
    List<int> j_mouth = new List<int>();
    List<int> j_nose = new List<int>();


    for(int i = 0; i < Points_All.Count; i++)
    {
      for (int j = 0; j < Points_Eye.Count; j++)
      {
        if ( Points_All[i].X == Points_Eye[j].X &&
          Points_All[i].Y == Points_Eye[j].Y &&
          Points_All[i].Z == Points_Eye[j].Z )
        {
          j_eye.Add(i);

        }
      }
    }


    for(int i = 0; i < Points_All.Count; i++)
    {
      for (int j = 0; j < Points_Mouth.Count; j++)
      {
        if ( Points_All[i].X == Points_Mouth[j].X &&
          Points_All[i].Y == Points_Mouth[j].Y &&
          Points_All[i].Z == Points_Mouth[j].Z )
        {
          j_mouth.Add(i);

        }
      }
    }


    for(int i = 0; i < Points_All.Count; i++)
    {
      for (int j = 0; j < Points_Nose.Count; j++)
      {
        if ( Points_All[i].X == Points_Nose[j].X &&
          Points_All[i].Y == Points_Nose[j].Y &&
          Points_All[i].Z == Points_Nose[j].Z )
        {
          j_nose.Add(i);

        }
      }
    }



    num_eye = j_eye;
    num_mouth = j_mouth;
    num_nose = j_nose;


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
        List<Point3d> Points_All = null;
    if (inputs[0] != null)
    {
      Points_All = GH_DirtyCaster.CastToList<Point3d>(inputs[0]);
    }
    List<Point3d> Points_Eye = null;
    if (inputs[1] != null)
    {
      Points_Eye = GH_DirtyCaster.CastToList<Point3d>(inputs[1]);
    }
    List<Point3d> Points_Mouth = null;
    if (inputs[2] != null)
    {
      Points_Mouth = GH_DirtyCaster.CastToList<Point3d>(inputs[2]);
    }
    List<Point3d> Points_Nose = null;
    if (inputs[3] != null)
    {
      Points_Nose = GH_DirtyCaster.CastToList<Point3d>(inputs[3]);
    }


    //3. Declare output parameters
      object num_eye = null;
  object num_mouth = null;
  object num_nose = null;


    //4. Invoke RunScript
    RunScript(Points_All, Points_Eye, Points_Mouth, Points_Nose, ref num_eye, ref num_mouth, ref num_nose);
      
    try
    {
      //5. Assign output parameters to component...
            if (num_eye != null)
      {
        if (GH_Format.TreatAsCollection(num_eye))
        {
          IEnumerable __enum_num_eye = (IEnumerable)(num_eye);
          DA.SetDataList(1, __enum_num_eye);
        }
        else
        {
          if (num_eye is Grasshopper.Kernel.Data.IGH_DataTree)
          {
            //merge tree
            DA.SetDataTree(1, (Grasshopper.Kernel.Data.IGH_DataTree)(num_eye));
          }
          else
          {
            //assign direct
            DA.SetData(1, num_eye);
          }
        }
      }
      else
      {
        DA.SetData(1, null);
      }
      if (num_mouth != null)
      {
        if (GH_Format.TreatAsCollection(num_mouth))
        {
          IEnumerable __enum_num_mouth = (IEnumerable)(num_mouth);
          DA.SetDataList(2, __enum_num_mouth);
        }
        else
        {
          if (num_mouth is Grasshopper.Kernel.Data.IGH_DataTree)
          {
            //merge tree
            DA.SetDataTree(2, (Grasshopper.Kernel.Data.IGH_DataTree)(num_mouth));
          }
          else
          {
            //assign direct
            DA.SetData(2, num_mouth);
          }
        }
      }
      else
      {
        DA.SetData(2, null);
      }
      if (num_nose != null)
      {
        if (GH_Format.TreatAsCollection(num_nose))
        {
          IEnumerable __enum_num_nose = (IEnumerable)(num_nose);
          DA.SetDataList(3, __enum_num_nose);
        }
        else
        {
          if (num_nose is Grasshopper.Kernel.Data.IGH_DataTree)
          {
            //merge tree
            DA.SetDataTree(3, (Grasshopper.Kernel.Data.IGH_DataTree)(num_nose));
          }
          else
          {
            //assign direct
            DA.SetData(3, num_nose);
          }
        }
      }
      else
      {
        DA.SetData(3, null);
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