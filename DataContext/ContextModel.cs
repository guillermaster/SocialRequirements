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
        public virtual DbSet<GeneralCatalogDetail> GeneralCatalogDetails { get; set; }
        public virtual DbSet<GeneralCatalogHeader> GeneralCatalogHeaders { get; set; }

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

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.Requirement)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementModification)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.RequirementModificationVersion)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.status_id)
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

            modelBuilder.Entity<Requirement>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Requirement>()
                .HasMany(e => e.RequirementVersion)
                .WithRequired(e => e.Requirement)
                .HasForeignKey(e => e.requirement_id)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<Role>()
                .HasMany(e => e.CompanyPersonRole)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.CompanyProjectPersonRole)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.role_id);
            
            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyPerson)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.company_id);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.CompanyPerson)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.person_id);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.Companies)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.type_id);

            modelBuilder.Entity<GeneralCatalogDetail>()
                .HasMany(e => e.ActivityFeed)
                .WithRequired(e => e.GeneralCatalogDetail)
                .HasForeignKey(e => e.entity_id);

            modelBuilder.Entity<GeneralCatalogHeader>()
                .HasMany(e => e.GeneralCatalogDetails)
                .WithRequired(e => e.GeneralCatalogHeader)
                .HasForeignKey(e => e.generalcatalog_id);
        }
    }
}
