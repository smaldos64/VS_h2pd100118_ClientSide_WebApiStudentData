namespace WebApiStudentData.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DatabaseContext : DbContext
    {
        // Your context has been configured to use a 'DatabaseContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApiStudentData.Models.DatabaseContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DatabaseContext' 
        // connection string in the application configuration file.
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User_Education_Time_Collection>()
            //.HasRequired(e => e.EducationLine)  
            //.WithMany(u => u.User_Education_Time_Collections)  
            //.WillCascadeOnDelete(false); 

            modelBuilder.Entity<User_Education_Character_Course_Collection>()
                .HasOptional(c => c.WhichCharacterScale)
                .WithMany(u => u.User_Education_Character_Course_Collections)
                .HasForeignKey(c => c.WhichCharacterScaleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_Education_Time_Collection>()
                .HasOptional(c => c.WhichCharacterScale)
                .WithMany(u => u.User_Education_Time_Collections)
                .HasForeignKey(c => c.WhichCharacterScaleID)
                .WillCascadeOnDelete(false);


            //modelBuilder.Entity<EducationLine>()
            //    .HasRequired<Education>(e => e.Education)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);


            //modelBuilder.Entity<User_Education_Time_Collection>()
            //    .HasOptional<EducationLine>(e => e.EducationLine)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User_Education_Time_Collection>()
            //    .HasRequired<WhichCharacterScale>(e => e.WhichCharacterScale)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<User_Education_Time_Collection>()
                .HasRequired<UserInfo>(e => e.UserInfo)
                .WithMany()
                .WillCascadeOnDelete(false);


            //modelBuilder.Entity<User_Education_Character_Course_Collection>()
            //    .HasRequired<Course>(e => e.Course)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User_Education_Character_Course_Collection>()
            //    .HasRequired<WhichCharacterScale>(e => e.WhichCharacterScale)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User_Education_Character_Course_Collection>()
            //    .HasRequired<User_Education_Time_Collection>(e => e.User_Education_Time_Collection)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ContactForm>()
            //   .HasRequired<UserInfo>(e => e.UserInfo)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);

            //modelBuilder.Entity<UserFile>()
            //   .HasRequired<UserInfo>(e => e.UserInfo)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        //public virtual DbSet<Absence> Absences { get; set; }
        public virtual DbSet<Character7Scale> Character7Scales { get; set; }
        public virtual DbSet<Character13Scale> Character13Scales { get; set; }
        public virtual DbSet<ContactForm> ContactForms { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EducationLine> EducationLines { get; set; }
        public virtual DbSet<User_Education_Character_Course_Collection> User_Education_Character_Course_Collections { get; set; }
        public virtual DbSet<User_Education_Time_Collection> User_Education_Time_Collections { get; set; }
        public virtual DbSet<UserFile> UserFiles { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<WhichCharacterScale> WhichCharacterScales { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}