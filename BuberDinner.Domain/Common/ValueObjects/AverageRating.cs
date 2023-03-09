﻿using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Common.ValueObjects
{
    public sealed class AverageRating : ValueObject
    {
        public AverageRating(double value, int numRating)
        {
            Value = value;
            NumRating = numRating;
        }

        public double Value { get; private set; }
        public int NumRating { get; private set; }

        public static AverageRating CreateNew(double rating, int numRating)
        {
            return new AverageRating(rating, numRating);
        }
        public void AddNewRating(Rating rating)
        {
            Value = ((Value * NumRating) + rating.Value) / ++NumRating;
        }
        public void RemoveRating(Rating rating)
        {
            Value = ((Value * NumRating) - rating.Value) / --NumRating;
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
