﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocialRequirements.RequirementQuestionService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RequirementQuestionService.RequirementQuestionSoap")]
    public interface RequirementQuestionSoap {
        
        // CODEGEN: Generating message contract since element name question from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddQuestion", ReplyAction="*")]
        SocialRequirements.RequirementQuestionService.AddQuestionResponse AddQuestion(SocialRequirements.RequirementQuestionService.AddQuestionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddQuestion", ReplyAction="*")]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.AddQuestionResponse> AddQuestionAsync(SocialRequirements.RequirementQuestionService.AddQuestionRequest request);
        
        // CODEGEN: Generating message contract since element name GetQuestionResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetQuestion", ReplyAction="*")]
        SocialRequirements.RequirementQuestionService.GetQuestionResponse GetQuestion(SocialRequirements.RequirementQuestionService.GetQuestionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetQuestion", ReplyAction="*")]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetQuestionResponse> GetQuestionAsync(SocialRequirements.RequirementQuestionService.GetQuestionRequest request);
        
        // CODEGEN: Generating message contract since element name encUsername from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllQuestions", ReplyAction="*")]
        SocialRequirements.RequirementQuestionService.GetAllQuestionsResponse GetAllQuestions(SocialRequirements.RequirementQuestionService.GetAllQuestionsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllQuestions", ReplyAction="*")]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetAllQuestionsResponse> GetAllQuestionsAsync(SocialRequirements.RequirementQuestionService.GetAllQuestionsRequest request);
        
        // CODEGEN: Generating message contract since element name answer from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddAnswer", ReplyAction="*")]
        SocialRequirements.RequirementQuestionService.AddAnswerResponse AddAnswer(SocialRequirements.RequirementQuestionService.AddAnswerRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddAnswer", ReplyAction="*")]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.AddAnswerResponse> AddAnswerAsync(SocialRequirements.RequirementQuestionService.AddAnswerRequest request);
        
        // CODEGEN: Generating message contract since element name GetAnswersResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAnswers", ReplyAction="*")]
        SocialRequirements.RequirementQuestionService.GetAnswersResponse GetAnswers(SocialRequirements.RequirementQuestionService.GetAnswersRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAnswers", ReplyAction="*")]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetAnswersResponse> GetAnswersAsync(SocialRequirements.RequirementQuestionService.GetAnswersRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddQuestionRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddQuestion", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.AddQuestionRequestBody Body;
        
        public AddQuestionRequest() {
        }
        
        public AddQuestionRequest(SocialRequirements.RequirementQuestionService.AddQuestionRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AddQuestionRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long companyId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public long projectId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public long requirementId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string question;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string encUsername;
        
        public AddQuestionRequestBody() {
        }
        
        public AddQuestionRequestBody(long companyId, long projectId, long requirementId, string question, string encUsername) {
            this.companyId = companyId;
            this.projectId = projectId;
            this.requirementId = requirementId;
            this.question = question;
            this.encUsername = encUsername;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddQuestionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddQuestionResponse", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.AddQuestionResponseBody Body;
        
        public AddQuestionResponse() {
        }
        
        public AddQuestionResponse(SocialRequirements.RequirementQuestionService.AddQuestionResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class AddQuestionResponseBody {
        
        public AddQuestionResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetQuestionRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetQuestion", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.GetQuestionRequestBody Body;
        
        public GetQuestionRequest() {
        }
        
        public GetQuestionRequest(SocialRequirements.RequirementQuestionService.GetQuestionRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetQuestionRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long companyId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public long projectId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public long requirementId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long requirementVersionId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long requirementQuestionId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public bool getAnswers;
        
        public GetQuestionRequestBody() {
        }
        
        public GetQuestionRequestBody(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId, bool getAnswers) {
            this.companyId = companyId;
            this.projectId = projectId;
            this.requirementId = requirementId;
            this.requirementVersionId = requirementVersionId;
            this.requirementQuestionId = requirementQuestionId;
            this.getAnswers = getAnswers;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetQuestionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetQuestionResponse", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.GetQuestionResponseBody Body;
        
        public GetQuestionResponse() {
        }
        
        public GetQuestionResponse(SocialRequirements.RequirementQuestionService.GetQuestionResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetQuestionResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetQuestionResult;
        
        public GetQuestionResponseBody() {
        }
        
        public GetQuestionResponseBody(string GetQuestionResult) {
            this.GetQuestionResult = GetQuestionResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAllQuestionsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAllQuestions", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.GetAllQuestionsRequestBody Body;
        
        public GetAllQuestionsRequest() {
        }
        
        public GetAllQuestionsRequest(SocialRequirements.RequirementQuestionService.GetAllQuestionsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAllQuestionsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string encUsername;
        
        public GetAllQuestionsRequestBody() {
        }
        
        public GetAllQuestionsRequestBody(string encUsername) {
            this.encUsername = encUsername;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAllQuestionsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAllQuestionsResponse", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.GetAllQuestionsResponseBody Body;
        
        public GetAllQuestionsResponse() {
        }
        
        public GetAllQuestionsResponse(SocialRequirements.RequirementQuestionService.GetAllQuestionsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAllQuestionsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetAllQuestionsResult;
        
        public GetAllQuestionsResponseBody() {
        }
        
        public GetAllQuestionsResponseBody(string GetAllQuestionsResult) {
            this.GetAllQuestionsResult = GetAllQuestionsResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddAnswerRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddAnswer", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.AddAnswerRequestBody Body;
        
        public AddAnswerRequest() {
        }
        
        public AddAnswerRequest(SocialRequirements.RequirementQuestionService.AddAnswerRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AddAnswerRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long companyId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public long projectId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public long requirementId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long requirementVersionId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long requirementQuestionId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string answer;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string encUsername;
        
        public AddAnswerRequestBody() {
        }
        
        public AddAnswerRequestBody(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId, string answer, string encUsername) {
            this.companyId = companyId;
            this.projectId = projectId;
            this.requirementId = requirementId;
            this.requirementVersionId = requirementVersionId;
            this.requirementQuestionId = requirementQuestionId;
            this.answer = answer;
            this.encUsername = encUsername;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddAnswerResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddAnswerResponse", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.AddAnswerResponseBody Body;
        
        public AddAnswerResponse() {
        }
        
        public AddAnswerResponse(SocialRequirements.RequirementQuestionService.AddAnswerResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class AddAnswerResponseBody {
        
        public AddAnswerResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAnswersRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAnswers", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.GetAnswersRequestBody Body;
        
        public GetAnswersRequest() {
        }
        
        public GetAnswersRequest(SocialRequirements.RequirementQuestionService.GetAnswersRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAnswersRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long companyId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public long projectId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public long requirementId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long requirementVersionId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long requirementQuestionId;
        
        public GetAnswersRequestBody() {
        }
        
        public GetAnswersRequestBody(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId) {
            this.companyId = companyId;
            this.projectId = projectId;
            this.requirementId = requirementId;
            this.requirementVersionId = requirementVersionId;
            this.requirementQuestionId = requirementQuestionId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAnswersResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAnswersResponse", Namespace="http://tempuri.org/", Order=0)]
        public SocialRequirements.RequirementQuestionService.GetAnswersResponseBody Body;
        
        public GetAnswersResponse() {
        }
        
        public GetAnswersResponse(SocialRequirements.RequirementQuestionService.GetAnswersResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAnswersResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetAnswersResult;
        
        public GetAnswersResponseBody() {
        }
        
        public GetAnswersResponseBody(string GetAnswersResult) {
            this.GetAnswersResult = GetAnswersResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RequirementQuestionSoapChannel : SocialRequirements.RequirementQuestionService.RequirementQuestionSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RequirementQuestionSoapClient : System.ServiceModel.ClientBase<SocialRequirements.RequirementQuestionService.RequirementQuestionSoap>, SocialRequirements.RequirementQuestionService.RequirementQuestionSoap {
        
        public RequirementQuestionSoapClient() {
        }
        
        public RequirementQuestionSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RequirementQuestionSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RequirementQuestionSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RequirementQuestionSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SocialRequirements.RequirementQuestionService.AddQuestionResponse SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.AddQuestion(SocialRequirements.RequirementQuestionService.AddQuestionRequest request) {
            return base.Channel.AddQuestion(request);
        }
        
        public void AddQuestion(long companyId, long projectId, long requirementId, string question, string encUsername) {
            SocialRequirements.RequirementQuestionService.AddQuestionRequest inValue = new SocialRequirements.RequirementQuestionService.AddQuestionRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.AddQuestionRequestBody();
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.requirementId = requirementId;
            inValue.Body.question = question;
            inValue.Body.encUsername = encUsername;
            SocialRequirements.RequirementQuestionService.AddQuestionResponse retVal = ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).AddQuestion(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.AddQuestionResponse> SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.AddQuestionAsync(SocialRequirements.RequirementQuestionService.AddQuestionRequest request) {
            return base.Channel.AddQuestionAsync(request);
        }
        
        public System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.AddQuestionResponse> AddQuestionAsync(long companyId, long projectId, long requirementId, string question, string encUsername) {
            SocialRequirements.RequirementQuestionService.AddQuestionRequest inValue = new SocialRequirements.RequirementQuestionService.AddQuestionRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.AddQuestionRequestBody();
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.requirementId = requirementId;
            inValue.Body.question = question;
            inValue.Body.encUsername = encUsername;
            return ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).AddQuestionAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SocialRequirements.RequirementQuestionService.GetQuestionResponse SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.GetQuestion(SocialRequirements.RequirementQuestionService.GetQuestionRequest request) {
            return base.Channel.GetQuestion(request);
        }
        
        public string GetQuestion(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId, bool getAnswers) {
            SocialRequirements.RequirementQuestionService.GetQuestionRequest inValue = new SocialRequirements.RequirementQuestionService.GetQuestionRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.GetQuestionRequestBody();
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.requirementId = requirementId;
            inValue.Body.requirementVersionId = requirementVersionId;
            inValue.Body.requirementQuestionId = requirementQuestionId;
            inValue.Body.getAnswers = getAnswers;
            SocialRequirements.RequirementQuestionService.GetQuestionResponse retVal = ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).GetQuestion(inValue);
            return retVal.Body.GetQuestionResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetQuestionResponse> SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.GetQuestionAsync(SocialRequirements.RequirementQuestionService.GetQuestionRequest request) {
            return base.Channel.GetQuestionAsync(request);
        }
        
        public System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetQuestionResponse> GetQuestionAsync(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId, bool getAnswers) {
            SocialRequirements.RequirementQuestionService.GetQuestionRequest inValue = new SocialRequirements.RequirementQuestionService.GetQuestionRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.GetQuestionRequestBody();
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.requirementId = requirementId;
            inValue.Body.requirementVersionId = requirementVersionId;
            inValue.Body.requirementQuestionId = requirementQuestionId;
            inValue.Body.getAnswers = getAnswers;
            return ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).GetQuestionAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SocialRequirements.RequirementQuestionService.GetAllQuestionsResponse SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.GetAllQuestions(SocialRequirements.RequirementQuestionService.GetAllQuestionsRequest request) {
            return base.Channel.GetAllQuestions(request);
        }
        
        public string GetAllQuestions(string encUsername) {
            SocialRequirements.RequirementQuestionService.GetAllQuestionsRequest inValue = new SocialRequirements.RequirementQuestionService.GetAllQuestionsRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.GetAllQuestionsRequestBody();
            inValue.Body.encUsername = encUsername;
            SocialRequirements.RequirementQuestionService.GetAllQuestionsResponse retVal = ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).GetAllQuestions(inValue);
            return retVal.Body.GetAllQuestionsResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetAllQuestionsResponse> SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.GetAllQuestionsAsync(SocialRequirements.RequirementQuestionService.GetAllQuestionsRequest request) {
            return base.Channel.GetAllQuestionsAsync(request);
        }
        
        public System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetAllQuestionsResponse> GetAllQuestionsAsync(string encUsername) {
            SocialRequirements.RequirementQuestionService.GetAllQuestionsRequest inValue = new SocialRequirements.RequirementQuestionService.GetAllQuestionsRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.GetAllQuestionsRequestBody();
            inValue.Body.encUsername = encUsername;
            return ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).GetAllQuestionsAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SocialRequirements.RequirementQuestionService.AddAnswerResponse SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.AddAnswer(SocialRequirements.RequirementQuestionService.AddAnswerRequest request) {
            return base.Channel.AddAnswer(request);
        }
        
        public void AddAnswer(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId, string answer, string encUsername) {
            SocialRequirements.RequirementQuestionService.AddAnswerRequest inValue = new SocialRequirements.RequirementQuestionService.AddAnswerRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.AddAnswerRequestBody();
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.requirementId = requirementId;
            inValue.Body.requirementVersionId = requirementVersionId;
            inValue.Body.requirementQuestionId = requirementQuestionId;
            inValue.Body.answer = answer;
            inValue.Body.encUsername = encUsername;
            SocialRequirements.RequirementQuestionService.AddAnswerResponse retVal = ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).AddAnswer(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.AddAnswerResponse> SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.AddAnswerAsync(SocialRequirements.RequirementQuestionService.AddAnswerRequest request) {
            return base.Channel.AddAnswerAsync(request);
        }
        
        public System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.AddAnswerResponse> AddAnswerAsync(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId, string answer, string encUsername) {
            SocialRequirements.RequirementQuestionService.AddAnswerRequest inValue = new SocialRequirements.RequirementQuestionService.AddAnswerRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.AddAnswerRequestBody();
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.requirementId = requirementId;
            inValue.Body.requirementVersionId = requirementVersionId;
            inValue.Body.requirementQuestionId = requirementQuestionId;
            inValue.Body.answer = answer;
            inValue.Body.encUsername = encUsername;
            return ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).AddAnswerAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SocialRequirements.RequirementQuestionService.GetAnswersResponse SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.GetAnswers(SocialRequirements.RequirementQuestionService.GetAnswersRequest request) {
            return base.Channel.GetAnswers(request);
        }
        
        public string GetAnswers(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId) {
            SocialRequirements.RequirementQuestionService.GetAnswersRequest inValue = new SocialRequirements.RequirementQuestionService.GetAnswersRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.GetAnswersRequestBody();
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.requirementId = requirementId;
            inValue.Body.requirementVersionId = requirementVersionId;
            inValue.Body.requirementQuestionId = requirementQuestionId;
            SocialRequirements.RequirementQuestionService.GetAnswersResponse retVal = ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).GetAnswers(inValue);
            return retVal.Body.GetAnswersResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetAnswersResponse> SocialRequirements.RequirementQuestionService.RequirementQuestionSoap.GetAnswersAsync(SocialRequirements.RequirementQuestionService.GetAnswersRequest request) {
            return base.Channel.GetAnswersAsync(request);
        }
        
        public System.Threading.Tasks.Task<SocialRequirements.RequirementQuestionService.GetAnswersResponse> GetAnswersAsync(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId) {
            SocialRequirements.RequirementQuestionService.GetAnswersRequest inValue = new SocialRequirements.RequirementQuestionService.GetAnswersRequest();
            inValue.Body = new SocialRequirements.RequirementQuestionService.GetAnswersRequestBody();
            inValue.Body.companyId = companyId;
            inValue.Body.projectId = projectId;
            inValue.Body.requirementId = requirementId;
            inValue.Body.requirementVersionId = requirementVersionId;
            inValue.Body.requirementQuestionId = requirementQuestionId;
            return ((SocialRequirements.RequirementQuestionService.RequirementQuestionSoap)(this)).GetAnswersAsync(inValue);
        }
    }
}
