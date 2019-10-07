namespace OrderManagement.Addin.Utilities {
    using System;
    using System.IO;
    using System.Security.AccessControl;
    using System.Security.Principal;

    public  class DirectoryHelper {
        private const string ExtensionConfigFile = "AddInConfig.xml";
        private const string OrderManagementAddInKey = "\\OrderManagementAddIn";

        public static string AddInLocalFolder {
            get {
                var appData = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                if (appData == null) {
                    return null;
                }

                var localFolder = $"{appData.FullName}\\Roaming";
                if (!HasWritePermission(localFolder)) {
                    throw new Exception($"{Environment.UserDomainName}\\{Environment.UserName} does not have permission on {localFolder}\\{OrderManagementAddInKey}");
                }

                localFolder += OrderManagementAddInKey;
                if (!Directory.Exists(localFolder)) {
                    Directory.CreateDirectory(localFolder);
                }

                return localFolder;
            }
        }

        public static string ConfigurationXmlPath => Path.Combine(AddInLocalFolder, ExtensionConfigFile);

        private static bool HasWritePermission(string path) {
            var writeAllow = false;
            var writeDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(SecurityIdentifier));

            foreach (FileSystemAccessRule rule in accessRules) {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = true;
            }

            return writeAllow && !writeDeny;
        }
    }
}