# College IT Admin + CITL Development Environment Scaffold
# Author: FLEX Coding GPT
# Purpose: Initialize standard folders and README.md templates for future automation projects

import os

# Base project structure
BASE_DIR = "C:\\Users\\armcdowell\\STACK_SharepointAdmin"

# Stack definitions
admin_stacks = [
    "STACK_SharePointAdmin",
    "STACK_PowerAutomateTools",
    "STACK_GraphAPIManagement",
    "STACK_OutlookExchangeAutomation",
    "STACK_TeamsBotIntegration",
    "STACK_AIIntegrations",
    "STACK_CanvasIntegration",
    "STACK_SharedLibraries",
    "DEV_SandboxTools"
]

citl_stacks = [
    "STACK_SharePointForms",
    "STACK_FlowAutomationTranscripts",
    "STACK_StudentTemplates",
    "STACK_CITL_ReportsAndMacros",
    "CITL_Reference_Guides"
]

GITIGNORE_CONTENT = """bin/
obj/
.vscode/
*.user
*.suo
*.dll
*.pdb
*.cache
.env
rtc_cred.xml
*.log
"""

README_TEMPLATE = """# {stack_name}

This is a root stack folder prepared for future development projects.

## Structure:
- `src/`: Source code
- `docs/`: Documentation
- `tests/`: Unit and integration tests
"""

def create_stack(path, stack_name):
    stack_path = os.path.join(path, stack_name)
    os.makedirs(os.path.join(stack_path, "src"), exist_ok=True)
    os.makedirs(os.path.join(stack_path, "docs"), exist_ok=True)
    os.makedirs(os.path.join(stack_path, "tests"), exist_ok=True)
    os.makedirs(os.path.join(stack_path, ".vscode"), exist_ok=True)

    with open(os.path.join(stack_path, "README.md"), "w") as f:
        f.write(README_TEMPLATE.format(stack_name=stack_name))

    with open(os.path.join(stack_path, ".gitignore"), "w") as f:
        f.write(GITIGNORE_CONTENT)

    # Create Git repo (optional - safe for local use)
    os.system(f"git -C \"{stack_path}\" init")
    os.system(f"git -C \"{stack_path}\" add .")
    os.system(f"git -C \"{stack_path}\" commit -m \"Initial commit for {stack_name}\"")

    # Add starter code to selected admin stacks
    if stack_name == "STACK_SharePointAdmin":
        with open(os.path.join(stack_path, "src", "SharePointListProvisioner.cs"), "w") as f:
            f.write("""
using Microsoft.SharePoint.Client;
using System;
using System.Security;

namespace SharePointAdmin
{
    class SharePointListProvisioner
    {
        static void Main(string[] args)
        {
            string siteUrl = "https://rtcedu.sharepoint.com/sites/ITAdmin";
            string listTitle = "CourseRequests";

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            Console.Write("Enter your password: ");
            SecureString password = GetSecurePassword();

            using (ClientContext context = new ClientContext(siteUrl))
            {
                context.Credentials = new SharePointOnlineCredentials(username, password);

                ListCreationInformation creationInfo = new ListCreationInformation
                {
                    Title = listTitle,
                    TemplateType = (int)ListTemplateType.GenericList
                };

                List list = context.Web.Lists.Add(creationInfo);
                context.ExecuteQuery();

                Console.WriteLine($"✅ Created list: {listTitle}");
            }
        }

        private static SecureString GetSecurePassword()
        {
            SecureString secureStr = new SecureString();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key != ConsoleKey.Enter)
                {
                    secureStr.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            secureStr.MakeReadOnly();
            return secureStr;
        }
    }
}
""")

# Create base folders
admin_root = os.path.join(BASE_DIR, "AdminProjects")
citl_root = os.path.join(BASE_DIR, "CITL_AutomationProjects")
doc_root = os.path.join(BASE_DIR, "SOLUTION_DOCUMENTATION")

os.makedirs(admin_root, exist_ok=True)
os.makedirs(citl_root, exist_ok=True)
os.makedirs(doc_root, exist_ok=True)

# Create stacks
for stack in admin_stacks:
    create_stack(admin_root, stack)

for stack in citl_stacks:
    create_stack(citl_root, stack)

# Create top-level documentation files
with open(os.path.join(doc_root, "IntegrationMatrix.xlsx"), "w") as f:
    f.write("")  # Placeholder
with open(os.path.join(doc_root, "StackRoles.md"), "w") as f:
    f.write("# Stack Roles\n\nDescribe ownership and responsibilities for each stack.")
with open(os.path.join(doc_root, "DevSetupInstructions.md"), "w") as f:
    f.write("# Dev Setup Instructions\n\nDocument your environment, SDK installs, and dependencies here.")

print(f"✅ Scaffold complete. Navigate to '{BASE_DIR}' to begin development.")