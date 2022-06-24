// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AWS.Deploy.Common.Extensions;
using AWS.Deploy.Common.IO;
using AWS.Deploy.Orchestration.Utilities;

namespace AWS.Deploy.Orchestration
{
    public interface IDeployToolWorkspaceMetadata
    {
        /// <summary>
        /// Deployment tool workspace directory to create CDK app during the deployment.
        /// </summary>
        string DeployToolWorkspaceDirectoryRoot { get; }

        /// <summary>
        /// Directory that contains CDK projects
        /// </summary>
        string ProjectsDirectory { get; }

        /// <summary>
        /// The file path of the CDK bootstrap template to be used
        /// </summary>
        string CDKBootstrapTemplatePath { get; }
    }

    public class DeployToolWorkspaceMetadata : IDeployToolWorkspaceMetadata
    {
        private readonly IDirectoryManager _directoryManager;
        private readonly IFileManager _fileManager;
        private readonly IEnvironmentVariableManager _environmentVariableManager;

        public string DeployToolWorkspaceDirectoryRoot
        {
            get
            {
                var workspace = Helpers.GetDeployToolWorkspaceDirectoryRoot(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _directoryManager, _environmentVariableManager);
                if (!_directoryManager.Exists(workspace))
                    _directoryManager.CreateDirectory(workspace);
                return workspace;
            }
        }

        public string ProjectsDirectory => Path.Combine(DeployToolWorkspaceDirectoryRoot, "Projects");

        public string CDKBootstrapTemplatePath
        {
            get
            {
                var bootstrapTemplate = Path.Combine(DeployToolWorkspaceDirectoryRoot, "CDKBootstrapTemplate.yaml");
                if (!_fileManager.Exists(bootstrapTemplate))
                {
                    // The CDK bootstrap template can be generated by running 'cdk bootstrap --show-template'.
                    // We need to keep the template up to date while making sure that the 'Staging Bucket' retention policies are set to 'Delete'.
                    var cdkBootstrapTemplate = typeof(CdkProjectHandler).Assembly.ReadEmbeddedFile(TemplateIdentifier);
                    using var cdkBootstrapTemplateFile = new StreamWriter(bootstrapTemplate);
                    cdkBootstrapTemplateFile.Write(cdkBootstrapTemplate);
                }
                return bootstrapTemplate;
            }
        }

        private const string TemplateIdentifier = "AWS.Deploy.Orchestration.CDK.CDKBootstrapTemplate.yaml";

        public DeployToolWorkspaceMetadata(IDirectoryManager directoryManager, IEnvironmentVariableManager environmentVariableManager, IFileManager fileManager)
        {
            _directoryManager = directoryManager;
            _environmentVariableManager = environmentVariableManager;
            _fileManager = fileManager;
        }
    }
}
