﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Management.Automation;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ManagementGroups.Common;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;

namespace Microsoft.Azure.Commands.ManagementGroups.Cmdlets
{
    /// <summary>
    /// Remove-AzureRmManagementGroupSubscription Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRmManagementGroupSubscription",
         DefaultParameterSetName = Constants.ParameterSetNames.GroupOperationsParameterSet,
         SupportsShouldProcess = true), OutputType(typeof(bool))]
    public class RemoveAzureRmManagementGroupSubscription : AzureManagementGroupAutoRegisterCmdletBase
    {
        [Parameter(ParameterSetName = Constants.ParameterSetNames.GroupOperationsParameterSet, Mandatory = true,
            HelpMessage = Constants.HelpMessages.GroupName, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string GroupName { get; set; } = null;

        [Parameter(ParameterSetName = Constants.ParameterSetNames.GroupOperationsParameterSet, Mandatory = true,
            HelpMessage = Constants.HelpMessages.SubscriptionId, Position = 1)]
        [ValidateNotNullOrEmpty]
        public Guid SubscriptionId { get; set; }

        [Parameter(ParameterSetName = Constants.ParameterSetNames.GroupOperationsParameterSet, Mandatory = false)]
        public SwitchParameter PassThru { get; set; }


        public override void ExecuteCmdlet()
        {
            try
            {
                IAzureContext context;
                if (TryGetDefaultContext(out context)
                    && context.Account != null
                    && context.Subscription != null)
                {
                    if (context.Subscription.Id != SubscriptionId.ToString())
                    {
                        Utility.AzureManagementGroupAutoRegisterSubscription(SubscriptionId.ToString(), context);
                    }
                }
                if (ShouldProcess(
                        string.Format(Resource.RemoveManagementGroupSubShouldProcessTarget, SubscriptionId,GroupName),
                        string.Format(Resource.RemoveManagementGroupSubShouldProcessAction, SubscriptionId, GroupName)))
                {
                    ManagementGroupsApiClient.ManagementGroupSubscriptions.Delete(GroupName, SubscriptionId.ToString());

                    if (PassThru.IsPresent)
                    {
                        WriteObject(true);
                    }
                }
            }
            catch (ErrorResponseException ex)
            {
                Utility.HandleErrorResponseException(ex);
            }
        }
    }
}
