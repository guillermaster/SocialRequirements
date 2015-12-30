﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocialRequirements.CompanyService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CompanyService.CompanySoap")]
    public interface CompanySoap {
        
        // CODEGEN: Generating message contract since element name GetCompanyTypesResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCompanyTypes", ReplyAction="*")]
        SocialRequirements.CompanyService.GetCompanyTypesResponse GetCompanyTypes(SocialRequirements.CompanyService.GetCompanyTypesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCompanyTypes", ReplyAction="*")]
        System.Threading.Tasks.Task<SocialRequirements.CompanyService.GetCompanyTypesResponse> GetCompanyTypesAsync(SocialRequirements.CompanyService.GetCompanyTypesRequest request);
        
        // CODEGEN: Generating message contract since element name name from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddCompany", ReplyAction="*")]
        SocialRequirements.CompanyService.AddCompanyResponse AddCompany(SocialRequirements.CompanyService.AddCompanyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddCompany", ReplyAction="*")]
        System.Threading.Tasks.Task<SocialRequirements.CompanyService.AddCompanyResponse> AddCompanyAsync(SocialRequirements.CompanyService.AddCompanyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HaveRequirements", ReplyAction="*")]
        bool HaveRequirements(long companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HaveRequirements", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> HaveRequirementsAsync(long companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HaveProjects", ReplyAction="*")]
        bool HaveProjects(long companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HaveProjects", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> HaveProjectsAsync(long companyId);
        
        // CODEGEN: Generating message contract since element name GetAllCompaniesResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllCompanies", ReplyAction="*")]
        SocialRequirements.CompanyService.GetAllCompaniesResponse GetAllCompanies(SocialRequirements.CompanyService.GetAllCompaniesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllCompanies", ReplyAction="*")]
        System.Threading.Tasks.Task<SocialRequirements.CompanyService.GetAllCompaniesResponse> GetAllCompaniesAsync(SocialRequirements.CompanyService.GetAllCompaniesRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCompanyTypesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCompanyTypes", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.CompanyService.GetCompanyTypesRequestBody Body;
        
        public GetCompanyTypesRequest() {
        }
        
        public GetCompanyTypesRequest(SocialRequirements.CompanyService.GetCompanyTypesRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetCompanyTypesRequestBody {
        
        public GetCompanyTypesRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCompanyTypesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCompanyTypesResponse", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.CompanyService.GetCompanyTypesResponseBody Body;
        
        public GetCompanyTypesResponse() {
        }
        
        public GetCompanyTypesResponse(SocialRequirements.CompanyService.GetCompanyTypesResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetCompanyTypesResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetCompanyTypesResult;
        
        public GetCompanyTypesResponseBody() {
        }
        
        public GetCompanyTypesResponseBody(string GetCompanyTypesResult) {
            this.GetCompanyTypesResult = GetCompanyTypesResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddCompanyRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddCompany", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.CompanyService.AddCompanyRequestBody Body;
        
        public AddCompanyRequest() {
        }
        
        public AddCompanyRequest(SocialRequirements.CompanyService.AddCompanyRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AddCompanyRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string name;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int type;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string encUsername;
        
        public AddCompanyRequestBody() {
        }
        
        public AddCompanyRequestBody(string name, int type, string encUsername) {
            this.name = name;
            this.type = type;
            this.encUsername = encUsername;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddCompanyResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddCompanyResponse", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.CompanyService.AddCompanyResponseBody Body;
        
        public AddCompanyResponse() {
        }
        
        public AddCompanyResponse(SocialRequirements.CompanyService.AddCompanyResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class AddCompanyResponseBody {
        
        public AddCompanyResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAllCompaniesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAllCompanies", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.CompanyService.GetAllCompaniesRequestBody Body;
        
        public GetAllCompaniesRequest() {
        }
        
        public GetAllCompaniesRequest(SocialRequirements.CompanyService.GetAllCompaniesRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetAllCompaniesRequestBody {
        
        public GetAllCompaniesRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAllCompaniesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAllCompaniesResponse", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.CompanyService.GetAllCompaniesResponseBody Body;
        
        public GetAllCompaniesResponse() {
        }
        
        public GetAllCompaniesResponse(SocialRequirements.CompanyService.GetAllCompaniesResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAllCompaniesResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetAllCompaniesResult;
        
        public GetAllCompaniesResponseBody() {
        }
        
        public GetAllCompaniesResponseBody(string GetAllCompaniesResult) {
            this.GetAllCompaniesResult = GetAllCompaniesResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CompanySoapChannel : SocialRequirements.CompanyService.CompanySoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CompanySoapClient : System.ServiceModel.ClientBase<SocialRequirements.CompanyService.CompanySoap>, SocialRequirements.CompanyService.CompanySoap {
        
        public CompanySoapClient() {
        }
        
        public CompanySoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CompanySoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompanySoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompanySoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SocialRequirements.CompanyService.GetCompanyTypesResponse SocialRequirements.CompanyService.CompanySoap.GetCompanyTypes(SocialRequirements.CompanyService.GetCompanyTypesRequest request) {
            return base.Channel.GetCompanyTypes(request);
        }
        
        public string GetCompanyTypes() {
            SocialRequirements.CompanyService.GetCompanyTypesRequest inValue = new SocialRequirements.CompanyService.GetCompanyTypesRequest();
            inValue.Body = new SocialRequirements.CompanyService.GetCompanyTypesRequestBody();
            SocialRequirements.CompanyService.GetCompanyTypesResponse retVal = ((SocialRequirements.CompanyService.CompanySoap)(this)).GetCompanyTypes(inValue);
            return retVal.Body.GetCompanyTypesResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SocialRequirements.CompanyService.GetCompanyTypesResponse> SocialRequirements.CompanyService.CompanySoap.GetCompanyTypesAsync(SocialRequirements.CompanyService.GetCompanyTypesRequest request) {
            return base.Channel.GetCompanyTypesAsync(request);
        }
        
        public System.Threading.Tasks.Task<SocialRequirements.CompanyService.GetCompanyTypesResponse> GetCompanyTypesAsync() {
            SocialRequirements.CompanyService.GetCompanyTypesRequest inValue = new SocialRequirements.CompanyService.GetCompanyTypesRequest();
            inValue.Body = new SocialRequirements.CompanyService.GetCompanyTypesRequestBody();
            return ((SocialRequirements.CompanyService.CompanySoap)(this)).GetCompanyTypesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SocialRequirements.CompanyService.AddCompanyResponse SocialRequirements.CompanyService.CompanySoap.AddCompany(SocialRequirements.CompanyService.AddCompanyRequest request) {
            return base.Channel.AddCompany(request);
        }
        
        public void AddCompany(string name, int type, string encUsername) {
            SocialRequirements.CompanyService.AddCompanyRequest inValue = new SocialRequirements.CompanyService.AddCompanyRequest();
            inValue.Body = new SocialRequirements.CompanyService.AddCompanyRequestBody();
            inValue.Body.name = name;
            inValue.Body.type = type;
            inValue.Body.encUsername = encUsername;
            SocialRequirements.CompanyService.AddCompanyResponse retVal = ((SocialRequirements.CompanyService.CompanySoap)(this)).AddCompany(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SocialRequirements.CompanyService.AddCompanyResponse> SocialRequirements.CompanyService.CompanySoap.AddCompanyAsync(SocialRequirements.CompanyService.AddCompanyRequest request) {
            return base.Channel.AddCompanyAsync(request);
        }
        
        public System.Threading.Tasks.Task<SocialRequirements.CompanyService.AddCompanyResponse> AddCompanyAsync(string name, int type, string encUsername) {
            SocialRequirements.CompanyService.AddCompanyRequest inValue = new SocialRequirements.CompanyService.AddCompanyRequest();
            inValue.Body = new SocialRequirements.CompanyService.AddCompanyRequestBody();
            inValue.Body.name = name;
            inValue.Body.type = type;
            inValue.Body.encUsername = encUsername;
            return ((SocialRequirements.CompanyService.CompanySoap)(this)).AddCompanyAsync(inValue);
        }
        
        public bool HaveRequirements(long companyId) {
            return base.Channel.HaveRequirements(companyId);
        }
        
        public System.Threading.Tasks.Task<bool> HaveRequirementsAsync(long companyId) {
            return base.Channel.HaveRequirementsAsync(companyId);
        }
        
        public bool HaveProjects(long companyId) {
            return base.Channel.HaveProjects(companyId);
        }
        
        public System.Threading.Tasks.Task<bool> HaveProjectsAsync(long companyId) {
            return base.Channel.HaveProjectsAsync(companyId);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SocialRequirements.CompanyService.GetAllCompaniesResponse SocialRequirements.CompanyService.CompanySoap.GetAllCompanies(SocialRequirements.CompanyService.GetAllCompaniesRequest request) {
            return base.Channel.GetAllCompanies(request);
        }
        
        public string GetAllCompanies() {
            SocialRequirements.CompanyService.GetAllCompaniesRequest inValue = new SocialRequirements.CompanyService.GetAllCompaniesRequest();
            inValue.Body = new SocialRequirements.CompanyService.GetAllCompaniesRequestBody();
            SocialRequirements.CompanyService.GetAllCompaniesResponse retVal = ((SocialRequirements.CompanyService.CompanySoap)(this)).GetAllCompanies(inValue);
            return retVal.Body.GetAllCompaniesResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SocialRequirements.CompanyService.GetAllCompaniesResponse> SocialRequirements.CompanyService.CompanySoap.GetAllCompaniesAsync(SocialRequirements.CompanyService.GetAllCompaniesRequest request) {
            return base.Channel.GetAllCompaniesAsync(request);
        }
        
        public System.Threading.Tasks.Task<SocialRequirements.CompanyService.GetAllCompaniesResponse> GetAllCompaniesAsync() {
            SocialRequirements.CompanyService.GetAllCompaniesRequest inValue = new SocialRequirements.CompanyService.GetAllCompaniesRequest();
            inValue.Body = new SocialRequirements.CompanyService.GetAllCompaniesRequestBody();
            return ((SocialRequirements.CompanyService.CompanySoap)(this)).GetAllCompaniesAsync(inValue);
        }
    }
}
