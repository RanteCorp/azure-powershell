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

using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.ServerCommunicationLink.Model;
using Microsoft.Azure.Commands.Sql.ServerCommunicationLink.Services;

namespace Microsoft.Azure.Commands.Sql.ServerCommunicationLink.Cmdlet
{
    public abstract class AzureSqlServerCommunicationLinkCmdletBase : AzureSqlCmdletBase<IEnumerable<AzureSqlServerCommunicationLinkModel>, AzureSqlServerCommunicationLinkAdapter>
    {
        /// <summary>
        /// Gets or sets the name of the server to use.
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The name of the Azure SQL Server.")]
        [ValidateNotNullOrEmpty]
        public string ServerName { get; set; }

        /// <summary>
        /// Initializes the adapter
        /// </summary>
        /// <param name="subscription">The subscription</param>
        /// <returns>Link adapter for ServerCommunicationLink</returns>
        protected override AzureSqlServerCommunicationLinkAdapter InitModelAdapter(Azure.Common.Authentication.Models.AzureSubscription subscription)
        {
            return new AzureSqlServerCommunicationLinkAdapter(DefaultProfile.Context);
        }
    }
}