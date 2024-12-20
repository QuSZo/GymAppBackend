﻿using GymAppBackend.Application.ExerciseSets.Queries.DTO;

namespace GymAppBackend.Application.Exercises.Queries.DTO;

public class ExerciseDto
{
    public Guid Id { get; set; }
    public int ExerciseNumber { get; set; }
    public string ExerciseTypeName { get; set; }
    public IEnumerable<ExerciseSetDto> ExerciseSets { get; set; }
}