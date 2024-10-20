﻿using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Core.Workouts.Entities;
using GymAppBackend.Infrastructure.DAL.Exercises.Queries;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Queries;

public static class Extensions
{
    public static WorkoutsDto AsDto(this Workout workout)
    {
        return new()
        {
            Id = workout.Id,
            Date = workout.Date,
        };
    }

    public static WorkoutDetailsDto AsDetailsDto(this Workout workout)
    {
        return new()
        {
            Id = workout.Id,
            Date = workout.Date,
            Exercises = workout.Exercises.Select(exercise => exercise.AsDto()),
        };
    }
}