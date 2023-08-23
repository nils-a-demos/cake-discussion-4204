var target = Argument("target", "Default");
var settings = new ProcessSettings
{
 Arguments = "/I task build.cake"
};

Task("NoRedirect")
.Does(() => {
   Information("Running no redirect:");
   var exitCode = StartProcess("findstr", settings);
   Information("exited with code: " + exitCode);
});

Task("Redirect")
.Does(() => {
   Information("Running redirect:");
   settings.RedirectStandardOutput = true;
   settings.RedirectedStandardOutputHandler = (x) => {
      Information("--> " + x);
      return x;
   };
   var exitCode = StartProcess("findstr", settings);
   Information("exited with code: " + exitCode);
});

Task("Default")
   .IsDependentOn("NoRedirect")
   .IsDependentOn("Redirect");

RunTarget(target);