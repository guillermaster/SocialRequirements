using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.General;

namespace SocialRequirements.Utilities
{
    public class StringUtilities
    {
        public static string GetShort(string text, int maxlength)
        {
            return text.Length <= maxlength ? text : text.Substring(0, maxlength - 1) + "...";
        }

        public static string GetPersonFullName(Person person)
        {
            return person != null ? person.first_name + " " + person.last_name : string.Empty;
        }

        public static string GetEntityName(GeneralCatalog.Detail.Entity entity, bool plural = false)
        {
            var name = string.Empty;

            switch (entity)
            {
                case GeneralCatalog.Detail.Entity.Project:
                    name = "Project";
                    break;
                case GeneralCatalog.Detail.Entity.Requirement:
                    name = "Requirement";
                    break;
                case GeneralCatalog.Detail.Entity.RequirementComment:
                    name = "Requirement comment";
                    break;
                case GeneralCatalog.Detail.Entity.RequirementModification:
                    name = "Requirement modification";
                    break;
                case GeneralCatalog.Detail.Entity.RequirementModificationComment:
                    name = "Requirement modification comment";
                    break;
                case GeneralCatalog.Detail.Entity.RequirementQuestion:
                    name = "Requirement question";
                    break;
                case GeneralCatalog.Detail.Entity.RequirementQuestionAnswer:
                    name = "Requirement question answer";
                    break;
            }

            if (plural) name += "s";

            return name;
        }

        public static string GetActionOccurredDescription(GeneralCatalog.Detail.EntityActions actionType, bool plural = false)
        {
            var description = plural ? "have been " : "has been ";

            switch (actionType)
            {
                case GeneralCatalog.Detail.EntityActions.Create:
                    description += "created";
                    break;
                case GeneralCatalog.Detail.EntityActions.Modify:
                    description += "modified";
                    break;
                case GeneralCatalog.Detail.EntityActions.Approve:
                    description += "approved";
                    break;
                case GeneralCatalog.Detail.EntityActions.Dislike:
                    description += "disliked";
                    break;
                case GeneralCatalog.Detail.EntityActions.Like:
                    description += "liked";
                    break;
                case GeneralCatalog.Detail.EntityActions.Reject:
                    description += "rejected";
                    break;
                case GeneralCatalog.Detail.EntityActions.Remove:
                    description += "removed";
                    break;
                case GeneralCatalog.Detail.EntityActions.SubmitForApproval:
                    description += "submitted for approval";
                    break;
            }

            return description;
        }
    }
}
