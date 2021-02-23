// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Deploy.Recipes.CDK.Common
{
    public static class CloudFormationIdentifierConstants
    {
        /// <summary>
        /// The CDK context parameter name used to pass in the location of the AWS Deploy Tool's settings file.
        /// </summary>
        public const string SETTINGS_PATH_CDK_CONTEXT_PARAMETER = "aws-deploy-tool-setting";

        /// <summary>
        /// The name of the identifier tag applied to CloudFormation stacks deployed by the AWS Deploy tool. The value of the
        /// tag is the recipe id used by the AWS Deploy Tool.
        /// </summary>
        public const string STACK_TAG = "aws-dotnet-deploy";

        /// <summary>
        /// AWS Deploy Tool CloudFormation stacks will prefix the description with this value to help identify stacks that are created by the AWS Deploy Tool.
        /// </summary>
        public const string STACK_DESCRIPTION_PREFIX = "AWSDotnetDeployCDKStack";

        /// <summary>
        /// The CloudFormation template metadata key used to hold the last used settings to deploy the application.
        /// </summary>
        public const string STACK_METADATA_SETTINGS = "aws-dotnet-deploy-settings";

        /// <summary>
        /// The CloudFormation template metadata key for storing the id of the AWS Deploy Tool recipe.
        /// </summary>
        public const string STACK_METADATA_RECIPE_ID = "aws-dotnet-deploy-recipe-id";

        /// <summary>
        /// The CloudFormation template metadata key for storing the version of the AWS Deploy Tool recipe.
        /// </summary>
        public const string STACK_METADATA_RECIPE_VERSION = "aws-dotnet-deploy-recipe-version";
    }
}
