﻿using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Application.Workouts.Queries.DTO;

public class WorkoutsDto
{
    public Guid Id { get; set; }
    public Date Date { get; set; }
}