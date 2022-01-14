using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingApp.UI.Models.Validator
{
    public class SeasonSerieModelValidator : AbstractValidator<SeasonsSerie>
    {
        public SeasonSerieModelValidator()
        {
            RuleFor(x => x.Season.Name).Length(2, 50).WithMessage("The length of the field season name should be between 2 and 25 characters long.");
        }
    }
}
