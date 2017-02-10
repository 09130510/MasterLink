using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Permissions;


namespace Capital.RTD
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        /// <summary>
        /// 
        /// </summary>
        public Installer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeploymentState"></param>
        [SecurityPermission(SecurityAction.Demand)]
        public override void Commit(System.Collections.IDictionary DeploymentState)
        {
            base.Commit(DeploymentState);
            DirectorySecurity dirSec = Directory.GetAccessControl(DeploymentState["DeploymentDirectory"].ToString());
            FileSystemAccessRule fsar = new FileSystemAccessRule("Users", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
            dirSec.AddAccessRule(fsar);
            Directory.SetAccessControl(DeploymentState["DeploymentDirectory"].ToString(), dirSec);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateSaver"></param>
        [SecurityPermission(SecurityAction.Demand)]
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            stateSaver.Add("DeploymentDirectory", Context.Parameters["rtd_DeploymentDirectory"].ToString());
            RegAsm("/codebase");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateSaver"></param>
        [SecurityPermission(SecurityAction.Demand)]
        public override void Uninstall(System.Collections.IDictionary stateSaver)
        {
            RegAsm("/u");
            base.Uninstall(stateSaver);
        }
 
        private static void RegAsm(string parameters)
        {
            var deployment_directory_partial = Path.GetFullPath(Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), @"..\.."));
            var target_regasm_path_array = new[]
            {
                string.Concat(deployment_directory_partial, "\\Framework\\", RuntimeEnvironment.GetSystemVersion(), "\\regasm.exe"),
                string.Concat(deployment_directory_partial, "\\Framework64\\", RuntimeEnvironment.GetSystemVersion(), "\\regasm.exe")
            };
            var pathToCompiledCode = Assembly.GetExecutingAssembly().Location;
 
            foreach (var target_regasm_path in target_regasm_path_array)
            {
                if (!File.Exists(target_regasm_path))
                    continue;
                var process = new Process
                {
                    StartInfo =
                    {
                        CreateNoWindow = true,
                        ErrorDialog = false,
                        UseShellExecute = false,
                        FileName = target_regasm_path,
                        Arguments = string.Format("\"{0}\" {1}", pathToCompiledCode, parameters)
                    }
               };
                using (process)
                {
                    process.Start();
                    process.WaitForExit();
                }
            }
        }
    }
}
