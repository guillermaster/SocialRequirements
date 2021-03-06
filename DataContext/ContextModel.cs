using System.Data.Entity;
using SocialRequirements.Context.Entities;

namespace SocialRequirements.Context
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=cnxSocialRequirementsDB")
        {
        }

        public virtual DbSet<ActivityFeed> ActivityFeed { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyPerson> CompanyPerson { get; set; }
        public virtual DbSet<CompanyPersonRole> CompanyPersonRole { get; set; }
        public virtual DbSet<CompanyProject> CompanyProject { get; set; }
        public virtual DbSet<CompanyProjectPerson> CompanyProjectPerson { get; set; }
        public virtual DbSet<CompanyProjectPersonRole> CompanyProjectPersonRole { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Requirement> Requirement { get; set; }
        public virtual DbSet<RequirementComment> RequirementComment { get; set; }
        public virtual DbSet<RequirementModification> RequirementModification { get; set; }
        public virtual DbSet<RequirementModificationComment> RequirementModificationComment { get; set; }
        public virtual DbSet<RequirementModificationVersion> RequirementModificationVersion { get; set; }
        public virtual DbSet<RequirementQuestion> RequirementQuestion { get; set; }
        public virtual DbSet<RequirementQuestionAnswer> RequirementQuestionAnswer { get; set; }
        public virtual DbSet<RequirementVersion> RequirementVersion { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<GeneralCatalogDetail> GeneralCatalogDetails { get; set; }
        public virtual DbSet<GeneralCatalogHeader> GeneralCatalogHeaders { get; set; }
        public virtual DbSet<RequirementHashtag> RequirementHashtag { get; set; }
        public virtual DbSet<RequirementModificationHashtag> RequirementModificationHashtag { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(e => e.ActivityFeed)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyPersonRole)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyProject)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyProjectPerson)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyProjectPersonRole)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Requirement)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.RequirementComment)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.RequirementModification)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.RequirementModificationComment)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.RequirementModificationVersion)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.RequirementVersion)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.RequirementQuestion)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.Companies)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.type_id);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.ActivityFeed)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.entity_id);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.ActivityFeed1)
                .WithRequired(e => e.GeneralCatalogDetail1)
                .HasForeignKey(e => e.action_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.Requirement)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.Requirement1)
                .WithRequired(e => e.GeneralCatalogDetail1)
                .HasForeignKey(e => e.priority_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.Requirement2)
                .WithRequired(e => e.GeneralCatalogDetail2)
                .HasForeignKey(e => e.developmentstatus_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementModification)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
               .HasMany(e => e.RequirementModification1)
               .WithRequired(e => e.GeneralCatalogDetail1)
               .HasForeignKey(e => e.priority_id)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementModificationVersion)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementModificationVersion1)
                .WithRequired(e => e.GeneralCatalogDetail1)
                .HasForeignKey(e => e.priority_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementQuestion)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementQuestionAnswer)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementVersion)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementVersion1)
                .WithRequired(e => e.GeneralCatalogDetail1)
                .HasForeignKey(e => e.priority_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementModificationHashtag)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.CompanyPersonRole)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.person_id);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.CompanyProjectPerson)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.person_id);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.CompanyProjectPersonRole)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.person_id);

            modelBuilder.Entity<Person>()
               .HasMany(e => e.ActivityFeed)
               .WithRequired(e => e.Person)
               .HasForeignKey(e => e.createdby_id)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Requirement)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Requirement1)
                .WithRequired(e => e.Person1)
                .HasForeignKey(e => e.modifiedby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementModification)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementModification1)
                .WithRequired(e => e.Person1)
                .HasForeignKey(e => e.modifiedby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementComment)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementModificationComment)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementQuestion)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementQuestion1)
                .WithRequired(e => e.Person1)
                .HasForeignKey(e => e.modifiedby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementQuestionAnswer)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementQuestionAnswer1)
                .WithRequired(e => e.Person1)
                .HasForeignKey(e => e.modifiedby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementHashtag)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementModificationHashtag)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementVersion)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.approvedby_id);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementVersion1)
                .WithRequired(e => e.Person1)
                .HasForeignKey(e => e.createdby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RequirementVersion2)
                .WithRequired(e => e.Person2)
                .HasForeignKey(e => e.modifiedby_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.ActivityFeed)
                .WithOptional(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.CompanyProject)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.CompanyProjectPerson)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.CompanyProjectPersonRole)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Requirement)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.RequirementComment)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.RequirementModification)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.RequirementModificationComment)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.RequirementModificationVersion)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.RequirementVersion)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.RequirementQuestion)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.project_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requirement>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Requirement>()
                .HasMany(e => e.RequirementVersion)
                .WithRequired(e => e.Requirement)
                .HasForeignKey(e => e.requirement_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requirement>()
                .HasMany(e => e.RequirementComment)
                .WithRequired(e => e.Requirement)
                .HasForeignKey(e => e.requirement_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requirement>()
                .HasMany(e => e.RequirementModificationComment)
                .WithRequired(e => e.Requirement)
                .HasForeignKey(e => e.requirement_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requirement>()
                .HasMany(e => e.RequirementQuestion)
                .WithRequired(e => e.Requirement)
                .HasForeignKey(e => e.requirement_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Requirement>()
                .HasMany(e => e.RequirementHashtag)
                .WithRequired(e => e.Requirement)
                .HasForeignKey(e => e.requirement_id);

            modelBuilder.Entity<RequirementComment>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementModification>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementModification>()
                .HasMany(e => e.RequirementVersion)
                .WithOptional(e => e.RequirementModification)
                .HasForeignKey(e => e.requirement_modification_id);

            modelBuilder.Entity<RequirementModification>()
                .HasMany(e => e.RequirementModificationComment)
                .WithRequired(e => e.RequirementModification)
                .HasForeignKey(e => e.requirement_modification_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequirementModification>()
                .HasMany(e => e.RequirementModificationHashtag)
                .WithRequired(e => e.RequirementModification)
                .HasForeignKey(e => e.requirement_modification_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequirementModificationVersion>()
                .HasMany(e => e.RequirementModificationComment)
                .WithRequired(e => e.RequirementModificationVersion)
                .HasForeignKey(e => e.requirement_modification_version_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequirementModificationComment>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementModificationVersion>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementQuestionAnswer>()
                .Property(e => e.answer)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementVersion>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<RequirementVersion>()
                .HasMany(e => e.RequirementComment)
                .WithRequired(e => e.RequirementVersion)
                .HasForeignKey(e => e.requirement_version_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequirementVersion>()
               .HasMany(e => e.RequirementQuestion)
               .WithRequired(e => e.RequirementVersion)
               .HasForeignKey(e => e.requirement_version_id)
               .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Role>()
                .HasMany(e => e.CompanyPersonRole)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.CompanyProjectPersonRole)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Role)
                .WithMany(e => e.Permission)
                .Map(m => m.ToTable("RolePermission").MapLeftKey("permission_id").MapRightKey("role_id"));
            
            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyPerson)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.CompanyPerson)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.person_id);
            
            modelBuilder.Entity<GeneralCatalogHeader>()
                .HasMany(e => e.GeneralCatalogDetails)
                .WithRequired(e => e.GeneralCatalogHeader)
                .HasForeignKey(e => e.generalcatalog_id);
        }
    }
}
