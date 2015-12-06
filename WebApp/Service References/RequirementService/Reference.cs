﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace SocialRequirements.RequirementService {
    
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="RequirementService.RequirementSoap")]
    public interface RequirementSoap {
        
        // CODEGEN: Generating message contract since element name title from namespace http://tempuri.org/ is not marked nillable
        [OperationContract(Action="http://tempuri.org/AddRequirement", ReplyAction="*")]
        AddRequirementResponse AddRequirement(AddRequirementRequest request);
        
        [OperationContract(Action="http://tempuri.org/AddRequirement", ReplyAction="*")]
        Task<AddRequirementResponse> AddRequirementAsync(AddRequirementRequest request);
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped=false)]
    public partial class AddRequirementRequest {
        
        [MessageBodyMember(Name="AddRequirement", Namespace="http://tempuri.org/", Order=0)]
        public AddRequirementRequestBody Body;
        
        public AddRequirementRequest() {
        }
        
        public AddRequirementRequest(AddRequirementRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DataContract(Namespace="http://tempuri.org/")]
    public partial class AddRequirementRequestBody {
        
        [DataMember(EmitDefaultValue=false, Order=0)]
        public string title;
        
        [DataMember(EmitDefaultValue=false, Order=1)]
        public string description;
        
        [DataMember(Order=2)]
        public long companyId;
        
        [DataMember(Order=3)]
        public long projectId;
        
        [DataMember(EmitDefaultValue=false, Order=4)]
        public string encUsername;
        
        public AddRequirementRequestBody() {
        }
        
        public AddRequirementRequestBody(string title, string description, long companyId, long projectId, string encUsername) {
            this.title = title;
            this.description = description;
            this.companyId = companyId;
            this.projectId = projectId;
            this.encUsername = encUsername;
        }
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped=false)]
    public partial class AddRequirementResponse {
        
        [MessageBodyMember(Name="AddRequirementResponse", Namespace="http://tempuri.org/", Order=0)]
        public AddRequirementResponseBody Body;
        
        public AddRequirementResponse() {
        }
        
        public AddRequirementResponse(AddRequirementResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DataContract()]
    public partial class AddRequirementResponseBody {
        
        public AddRequirementResponseBody() {
        }
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface RequirementSoapChannel : RequirementSoap, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class RequirementSoapClient : ClientBase<RequirementSoap>, RequirementSoap {
        
        public RequirementSoapClient() {
        }
        
        public RequirementSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RequirementSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RequirementSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RequirementSoapClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        AddRequirementResponse RequirementSoap.AddRequirement(AddRequirementRequest request) {
            return base.Channel.AddRequirement(request);
        }
        
        public void AddRequirement(string title, string description, long companyId, long projectId, string encUsername) {
            AddRequirementRequest inValue = new AddRequirementRequest();
            inValue.Body = new AddRequirementRequestBody();
            inValue.Body.title = title;
            inValue.Body.description = description;
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.encUsername = encUsername;
            AddRequirementResponse retVal = ((RequirementSoap)(this)).AddRequirement(inValue);
        }
        
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        Task<AddRequirementResponse> RequirementSoap.AddRequirementAsync(AddRequirementRequest request) {
            return base.Channel.AddRequirementAsync(request);
        }
        
        public Task<AddRequirementResponse> AddRequirementAsync(string title, string description, long companyId, long projectId, string encUsername) {
            AddRequirementRequest inValue = new AddRequirementRequest();
            inValue.Body = new AddRequirementRequestBody();
            inValue.Body.title = title;
            inValue.Body.description = description;
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.encUsername = encUsername;
            return ((RequirementSoap)(this)).AddRequirementAsync(inValue);
        }
    }
}
