using Rhino;
using Rhino.Commands;
using Xilium.CefGlue.EtoForms;

namespace MyRhinoPlugin1
{
    public class MyRhinoCommand1 : Rhino.Commands.Command
    {
        public MyRhinoCommand1()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static MyRhinoCommand1 Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "CefTest";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: start here modifying the behaviour of your command.
            // ---
            RhinoApp.WriteLine("The {0} command will add a line right now.", EnglishName);

            var db = new Eto.Forms.Drawable()
            {

            };

            new Eto.Forms.Form()
            {
                Width = 600,
                Height = 600,
                Content = new EtoCefBrowser()
                {
                    Address = "https://www.google.com",
                }
            }.Show();

            RhinoApp.WriteLine("The {0} command added one line to the document.", EnglishName);

            // ---
            return Result.Success;
        }
    }
}
