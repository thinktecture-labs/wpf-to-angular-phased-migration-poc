using System;
using Backend.DataAccess;
using Light.Validation;
using Light.Validation.Checks;
using Light.Validation.Tools;
using Range = Light.Validation.Tools.Range;

namespace Backend.ComponentSamples;

public static class Validation
{
    private static Range<TimeSpan> ValidMigrationTimeRange { get; } =
        Range.FromInclusive(TimeSpan.FromSeconds(3))
             .ToInclusive(TimeSpan.FromHours(1));

    private static Range<decimal> ValidPeakAreRange { get; } =
        Range.FromInclusive(0m).ToInclusive(10_000_000m);

    public static void ValidateComponentSampleProperties<T>(this ValidationContext context, T dto)
        where T : IComponentSampleProperties
    {
        dto.ComponentName = context.Check(dto.ComponentName).HasLengthIn(Range.FromInclusive(1).ToInclusive(200));
        dto.MigrationTime = context.Check(dto.MigrationTime).IsIn(ValidMigrationTimeRange);
        dto.PeakArea = context.Check(dto.PeakArea).IsIn(ValidPeakAreRange);
    }
}