﻿// <auto-generated />
using System;
using GymAppBackend.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GymAppBackend.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(GymAppDbContext))]
    partial class GymAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GymAppBackend.Core.ExerciseCategories.Entities.ExerciseCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExerciseCategories");
                });

            modelBuilder.Entity("GymAppBackend.Core.ExerciseSets.Entities.ExerciseSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("Reps")
                        .HasColumnType("integer");

                    b.Property<int>("SetNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("ExerciseSets");
                });

            modelBuilder.Entity("GymAppBackend.Core.ExerciseTypes.Entities.ExerciseType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExerciseCategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseCategoryId");

                    b.ToTable("ExerciseTypes");
                });

            modelBuilder.Entity("GymAppBackend.Core.Exercises.Entities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ExerciseNumber")
                        .HasColumnType("integer");

                    b.Property<Guid>("ExerciseTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseTypeId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("GymAppBackend.Core.PasswordResetTokens.Entities.PasswordResetToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PasswordResetTokens");
                });

            modelBuilder.Entity("GymAppBackend.Core.Users.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GymAppBackend.Core.Workouts.Entities.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("GymAppBackend.Core.ExerciseSets.Entities.ExerciseSet", b =>
                {
                    b.HasOne("GymAppBackend.Core.Exercises.Entities.Exercise", "Exercise")
                        .WithMany("ExerciseSets")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("GymAppBackend.Core.ExerciseTypes.Entities.ExerciseType", b =>
                {
                    b.HasOne("GymAppBackend.Core.ExerciseCategories.Entities.ExerciseCategory", "ExerciseCategory")
                        .WithMany("ExerciseTypes")
                        .HasForeignKey("ExerciseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseCategory");
                });

            modelBuilder.Entity("GymAppBackend.Core.Exercises.Entities.Exercise", b =>
                {
                    b.HasOne("GymAppBackend.Core.ExerciseTypes.Entities.ExerciseType", "ExerciseType")
                        .WithMany()
                        .HasForeignKey("ExerciseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymAppBackend.Core.Workouts.Entities.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseType");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("GymAppBackend.Core.PasswordResetTokens.Entities.PasswordResetToken", b =>
                {
                    b.HasOne("GymAppBackend.Core.Users.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GymAppBackend.Core.Workouts.Entities.Workout", b =>
                {
                    b.HasOne("GymAppBackend.Core.Users.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GymAppBackend.Core.ExerciseCategories.Entities.ExerciseCategory", b =>
                {
                    b.Navigation("ExerciseTypes");
                });

            modelBuilder.Entity("GymAppBackend.Core.Exercises.Entities.Exercise", b =>
                {
                    b.Navigation("ExerciseSets");
                });

            modelBuilder.Entity("GymAppBackend.Core.Workouts.Entities.Workout", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
