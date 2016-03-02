using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using cw_itkpi.Models;

namespace cwitkpi.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20160302151003_Initial migration")]
    partial class Initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("cw_itkpi.Models.UserInfo", b =>
                {
                    b.Property<string>("username");

                    b.Property<string>("clan");

                    b.Property<int>("honor");

                    b.Property<string>("vkLink");

                    b.Property<int>("weeklyPoints");

                    b.HasKey("username");
                });
        }
    }
}
