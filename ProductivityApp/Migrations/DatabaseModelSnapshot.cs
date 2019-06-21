﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductivityApp.Models;

namespace ProductivityApp.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CriteriaId");

                    b.Property<string>("Text");

                    b.Property<string>("value");

                    b.HasKey("Id");

                    b.HasIndex("CriteriaId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("FormId");

                    b.Property<Guid?>("filterId");

                    b.Property<string>("inputField");

                    b.Property<string>("outputField");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.HasIndex("filterId");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("Criteria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<Guid?>("FlowId");

                    b.Property<int>("Order");

                    b.Property<string>("SelectedValue");

                    b.Property<string>("prompt");

                    b.HasKey("Id");

                    b.HasIndex("FlowId");

                    b.ToTable("Criteria");
                });

            modelBuilder.Entity("Destination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddresses");

                    b.Property<bool>("zip");

                    b.HasKey("Id");

                    b.ToTable("Destination");
                });

            modelBuilder.Entity("Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Kind");

                    b.Property<int>("Order");

                    b.Property<Guid?>("SurveyId");

                    b.Property<string>("answer");

                    b.Property<Guid?>("filterId");

                    b.Property<string>("prompt");

                    b.Property<bool>("remember");

                    b.Property<string>("tag");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.HasIndex("filterId");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("Filter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.Property<string>("value");

                    b.HasKey("Id");

                    b.ToTable("Filter");
                });

            modelBuilder.Entity("Flow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsATemplate");

                    b.Property<Guid?>("destinationId");

                    b.Property<Guid?>("inputSurveyId");

                    b.Property<string>("name");

                    b.HasKey("Id");

                    b.HasIndex("destinationId");

                    b.HasIndex("inputSurveyId");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("Form", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("FlowId");

                    b.Property<string>("fileName");

                    b.Property<string>("kind");

                    b.Property<string>("name");

                    b.HasKey("Id");

                    b.HasIndex("FlowId");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("Survey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("comment");

                    b.Property<DateTime>("timeCreated");

                    b.Property<string>("user");

                    b.HasKey("Id");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("Answer", b =>
                {
                    b.HasOne("Criteria")
                        .WithMany("answers")
                        .HasForeignKey("CriteriaId");
                });

            modelBuilder.Entity("Assignment", b =>
                {
                    b.HasOne("Form")
                        .WithMany("assignments")
                        .HasForeignKey("FormId");

                    b.HasOne("Filter", "filter")
                        .WithMany()
                        .HasForeignKey("filterId");
                });

            modelBuilder.Entity("Criteria", b =>
                {
                    b.HasOne("Flow")
                        .WithMany("criteria")
                        .HasForeignKey("FlowId");
                });

            modelBuilder.Entity("Field", b =>
                {
                    b.HasOne("Survey")
                        .WithMany("fields")
                        .HasForeignKey("SurveyId");

                    b.HasOne("Filter", "filter")
                        .WithMany()
                        .HasForeignKey("filterId");
                });

            modelBuilder.Entity("Flow", b =>
                {
                    b.HasOne("Destination", "destination")
                        .WithMany()
                        .HasForeignKey("destinationId");

                    b.HasOne("Survey", "inputSurvey")
                        .WithMany()
                        .HasForeignKey("inputSurveyId");
                });

            modelBuilder.Entity("Form", b =>
                {
                    b.HasOne("Flow")
                        .WithMany("forms")
                        .HasForeignKey("FlowId");
                });
#pragma warning restore 612, 618
        }
    }
}
